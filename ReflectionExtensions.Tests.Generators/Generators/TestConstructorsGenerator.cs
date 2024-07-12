using static ReflectionExtensions.ReflectionExtensions;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestConstructorsGenerator(bool isPublic) : GeneratorBase
{
    protected override string TypeName => isPublic ? "TestPublicConstructors" : "TestPrivateConstructors";

    private string TargetClass => isPublic ? "StubPublicConstructors" : "StubPrivateConstructors";

    protected override void GenerateInternal()
    {
        AppendTestFileStart();

        AppendConstructors<int>(5, false);
        AppendLine();
        AppendConstructors<string>(5, true);

        Append("}");
    }

    private void AppendConstructors<T>(int upToArgs, bool skipFirst)
    {
        InvokeSequence(upToArgs + 1, AppendConstructors<T>, AppendType.NewLine, skipFirst ? 1 : 0);
    }

    private void AppendConstructors<T>(int args)
    {
        const string extensionName = nameof(CreateConstructor);

        var testMethodNameBase = "Test_" + GenerateConstructorName<T>(TargetClass, args, false);

        using (WithTestMethodScope(testMethodNameBase + "_Generic"))
        {
            // full generics
            AppendOffset2($"var f = {extensionName}");
            AppendGenerics<T>(args, TargetClass);
            Append("(");
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_A"))
        {
            // A
            AppendOffset2($"var f = typeof({TargetClass}).{extensionName}A");
            AppendGenerics<T>(args);
            Append("(");
            AppendLine(");");
            AppendInvokeAndAssert<T>(args, true);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_T"))
        {
            // T = TR
            AppendOffset2($"var f = {extensionName}T");
            AppendGenerics<T>(0, TargetClass);
            Append("(");
            AppendTypeOf<T>(args, false);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_X"))
        {
            // X
            AppendOffset2($"var f = typeof({TargetClass}).{extensionName}X");
            Append("(");
            AppendTypeOf<T>(args, false);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args, true);
        }
    }

    private void AppendInvokeAndAssert<T>(int args, bool cast = false)
    {
        // Invoke
        AppendOffset2("var instance = f(");
        AppendParameterValues<T>(args);
        Append(')');

        if (cast)
        {
            Append($" as {TargetClass}");
        }

        AppendLine(';');

        AppendOffset2Line("Assert.That(instance, Is.Not.Null);");
        AppendOffset2Line($"Assert.That(instance, Is.TypeOf<{TargetClass}>());");

        var isString = typeof(T) == typeof(string);
        AppendOffset2("var expected = ");
        AppendNewArray<T>(args, i => isString ? $"\"{i + 1}\"" : $"{i + 1}");
        AppendLine(';');

        AppendOffset2Line("var actual = instance.Result;");
        AppendOffset2Line("Assert.That(actual, Is.EqualTo(expected));");
        AppendOffset2Line("Assert.That(actual.GetType(), Is.EqualTo(expected.GetType()));");
    }
}