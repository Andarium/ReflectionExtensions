using System;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestFieldAccessorsGenerator : GeneratorBase
{
    protected override string TypeName => "TestFieldAccessors";

    private const string StubClass = "StubFields";

    protected override void GenerateInternal()
    {
        using (WithTestFile(TypeName))
        {
            AppendSetupReset(StubClass);

            AppendCommentBlock("Instance");
            PermutationUtil.Permutate<bool, bool, Type, InstanceAccessorType>(GenerateInstanceTestCase, new PermutationUtil.Params(AppendLine, skipLastIteration: true));

            AppendCommentBlock("Const Instance");
            PermutationUtil.Permutate<bool, bool, Type, bool>(GenerateConstInstanceTestCase, new PermutationUtil.Params(AppendLine, skipLastIteration: true));

            AppendCommentBlock("Static");
            PermutationUtil.Permutate<bool, bool, Type, StaticAccessorType>(GenerateStaticTestCase, new PermutationUtil.Params(AppendLine, skipLastIteration: true));
        }
    }

    private void GenerateInstanceTestCase(bool isPublic, bool isReadonly, Type type, InstanceAccessorType accessorType)
    {
        var fieldName = StubFieldsGenerator.GenerateFieldName(isPublic, false, isReadonly, type);
        var methodName = $"{fieldName}_{accessorType}";

        using (WithTestMethodScope(methodName))
        {
            AppendLine($"var instance = new {StubClass}();");

            if (isReadonly)
            {
                using (WithThrowsScope<ArgumentException>())
                {
                    AppendCreateInstanceAccessor(fieldName, type, accessorType);
                }

                return;
            }

            Append("var accessor = ");
            AppendCreateInstanceAccessor(fieldName, type, accessorType);

            var encapsulated = $"instance.{StubFieldsGenerator.GenerateGetFieldName(isPublic, false, isReadonly, type)}";
            AppendAssert(encapsulated, type, 66, true);
        }
    }

    private void GenerateConstInstanceTestCase(bool isPublic, bool isReadonly, Type type, bool x)
    {
        var fieldName = StubFieldsGenerator.GenerateFieldName(isPublic, false, isReadonly, type);
        var methodName = $"{fieldName}{(x ? "_X" : "_Generic")}_Const";

        using (WithTestMethodScope(methodName))
        {
            AppendLine($"var instance = new {StubClass}();");

            if (isReadonly)
            {
                using (WithThrowsScope<ArgumentException>())
                {
                    AppendCreateConstInstanceAccessor(fieldName, type, x);
                }

                return;
            }

            Append("var accessor = ");
            AppendCreateConstInstanceAccessor(fieldName, type, x);

            var encapsulated = $"instance.{StubFieldsGenerator.GenerateGetFieldName(isPublic, false, isReadonly, type)}";
            AppendAssert(encapsulated, type, 555);
        }
    }

    private void GenerateStaticTestCase(bool isPublic, bool isReadonly, Type type, StaticAccessorType accessorType)
    {
        var fieldName = StubFieldsGenerator.GenerateFieldName(isPublic, true, isReadonly, type);
        var methodName = $"{fieldName}_{accessorType}";

        using (WithTestMethodScope(methodName))
        {
            if (isReadonly)
            {
                using (WithThrowsScope<ArgumentException>())
                {
                    AppendCreateStaticAccessor(fieldName, type, accessorType);
                }

                return;
            }

            Append("var accessor = ");
            AppendCreateStaticAccessor(fieldName, type, accessorType);

            var encapsulated = $"{StubClass}.{StubFieldsGenerator.GenerateGetFieldName(isPublic, true, isReadonly, type)}";
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

    private void AppendCreateInstanceAccessor(string fieldName, Type type, InstanceAccessorType accessorType)
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

        AppendWrap(fieldName, "(\"", "\");");
        AppendLine();
    }

    private void AppendCreateConstInstanceAccessor(string fieldName, Type type, bool x)
    {
        const string extensionName = nameof(ReflectionExtensions.CreateConstInstanceAccessor);

        Append("instance.");
        Append(extensionName);
        if (!x)
        {
            AppendGenerics(type);
        }

        AppendWrap(fieldName, "(\"", "\");");
        AppendLine();
    }

    private void AppendCreateStaticAccessor(string fieldName, Type type, StaticAccessorType accessorType)
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

        AppendWrap(fieldName, "(\"", "\");");
        AppendLine();
    }
}