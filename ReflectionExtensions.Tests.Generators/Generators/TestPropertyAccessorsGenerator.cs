using System;
using System.Linq;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestPropertyAccessorsGenerator : GeneratorBase
{
    protected override string TypeName => "TestPropertyAccessors";

    private const string StubClass = "StubProperties";

    protected override void GenerateInternal()
    {
        using (WithTestFile(TypeName))
        {
            AppendSetupReset(StubClass);

            AppendCommentBlock("Instance");

            PermutationUtil.PermutateCall<bool, PropertyMutability, Type, InstanceAccessorType>(GenerateInstanceTestCase, AppendLine);

            AppendCommentBlock("Const Instance");
            PermutationUtil.PermutateCall<bool, PropertyMutability, Type, bool>(GenerateConstInstanceTestCase, AppendLine);

            AppendCommentBlock("Static");

            // filter out static init combo
            var staticOptions = PermutationUtil.GeneratePermutations<bool, PropertyMutability, Type, StaticAccessorType>()
                .Where(x => x.Item2 is not PropertyMutability.Init)
                .ToList();

            PermutationUtil.Call(GenerateStaticTestCase, staticOptions, AppendLine);
        }
    }

    private void GenerateInstanceTestCase(bool isPublic, PropertyMutability mutability, Type type, InstanceAccessorType accessorType)
    {
        var propName = StubPropertiesGenerator.GeneratePropertyName(isPublic, false, mutability, type);
        var methodName = $"{propName}_{accessorType}";

        using (WithTestMethodScope(methodName))
        {
            AppendLine($"var instance = new {StubClass}();");

            if (mutability is PropertyMutability.GetOnly)
            {
                using (WithThrowsScope<ArgumentException>())
                {
                    AppendCreateInstanceAccessor(propName, type, accessorType);
                }

                return;
            }

            Append("var accessor = ");
            AppendCreateInstanceAccessor(propName, type, accessorType);

            var encapsulated = $"instance.{StubPropertiesGenerator.GenerateGetPropertyName(isPublic, false, mutability, type)}";
            AppendAssert(encapsulated, type, 66, true);
        }
    }

    private void GenerateConstInstanceTestCase(bool isPublic, PropertyMutability mutability, Type type, bool x)
    {
        var propName = StubPropertiesGenerator.GeneratePropertyName(isPublic, false, mutability, type);
        var methodName = $"{propName}{(x ? "_X" : "_Generic")}_Const";

        using (WithTestMethodScope(methodName))
        {
            AppendLine($"var instance = new {StubClass}();");

            if (mutability is PropertyMutability.GetOnly)
            {
                using (WithThrowsScope<ArgumentException>())
                {
                    AppendCreateConstInstanceAccessor(propName, type, x);
                }

                return;
            }

            Append("var accessor = ");
            AppendCreateConstInstanceAccessor(propName, type, x);

            var encapsulated = $"instance.{StubPropertiesGenerator.GenerateGetPropertyName(isPublic, false, mutability, type)}";
            AppendAssert(encapsulated, type, 555);
        }
    }

    private void GenerateStaticTestCase(bool isPublic, PropertyMutability mutability, Type type, StaticAccessorType accessorType)
    {
        var propName = StubPropertiesGenerator.GeneratePropertyName(isPublic, true, mutability, type);
        var methodName = $"{propName}_{accessorType}";

        using (WithTestMethodScope(methodName))
        {
            if (mutability is PropertyMutability.GetOnly)
            {
                using (WithThrowsScope<ArgumentException>())
                {
                    AppendCreateStaticAccessor(propName, type, accessorType);
                }

                return;
            }

            Append("var accessor = ");
            AppendCreateStaticAccessor(propName, type, accessorType);

            var encapsulated = $"{StubClass}.{StubPropertiesGenerator.GenerateGetPropertyName(isPublic, true, mutability, type)}";
            AppendAssert(encapsulated, type, 444);
        }
    }

    private void AppendAssert(string encapsulated, Type type, int expectedRaw, bool useInstance = false)
    {
        var instance = useInstance ? "instance" : "";
        AppendLine($"var actual = accessor.GetValue({instance});");
        AppendLine($"Assert.That(actual, Is.EqualTo({encapsulated}));");

        AppendLine();

        var expected = expectedRaw.Wrap(type == typeof(string));
        AppendLine($"var expected = {expected};");
        AppendLine(useInstance ? "accessor.SetValue(instance, expected);" : "accessor.SetValue(expected);");

        AppendLine($"actual = accessor.GetValue({instance});");

        AppendLine($"Assert.That(actual, Is.EqualTo({encapsulated}));");
        AppendLine("Assert.That(actual, Is.EqualTo(expected));");
    }

    private void AppendCreateInstanceAccessor(string propName, Type type, InstanceAccessorType accessorType)
    {
        const string extensionName = nameof(ReflectionExtensions.CreateInstanceAccessor);

        switch (accessorType)
        {
            case InstanceAccessorType.Generic:
                Append(extensionName);
                AppendGenerics(StubClass, type);
                break;
            case InstanceAccessorType.T:
                AppendTypeOf(StubClass);
                Append($".{extensionName}T");
                AppendGenerics(StubClass);
                break;
            case InstanceAccessorType.R:
                AppendTypeOf(StubClass);
                Append($".{extensionName}");
                AppendGenerics(type);
                break;
            case InstanceAccessorType.X:
                AppendTypeOf(StubClass);
                Append($".{extensionName}X");
                break;
        }

        AppendWrap(propName, "(\"", "\");");
        AppendLine();
    }

    private void AppendCreateConstInstanceAccessor(string propName, Type type, bool x)
    {
        const string extensionName = nameof(ReflectionExtensions.CreateConstInstanceAccessor);

        Append("instance.");
        Append(extensionName);
        if (!x)
        {
            AppendGenerics(type);
        }

        AppendWrap(propName, "(\"", "\");");
        AppendLine();
    }

    private void AppendCreateStaticAccessor(string propName, Type type, StaticAccessorType accessorType)
    {
        const string extensionName = nameof(ReflectionExtensions.CreateStaticAccessor);

        switch (accessorType)
        {
            case StaticAccessorType.Generic:
                Append(extensionName);
                AppendGenerics(StubClass, type);
                break;
            case StaticAccessorType.T:
                Append(extensionName);
                AppendGenerics(StubClass);
                break;
            case StaticAccessorType.X:
                AppendTypeOf(StubClass);
                Append('.');
                Append(extensionName);
                break;
        }

        AppendWrap(propName, "(\"", "\");");
        AppendLine();
    }
}