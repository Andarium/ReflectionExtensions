using System.Linq;
using static ReflectionExtensions.ReflectionExtensions;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestStaticFunctionsGenerator : GeneratorBase
{
    protected override string TypeName => "TestStaticFunctions";

    protected override void GenerateInternal()
    {
        AppendTestFileStart();

        AppendMethods<int>(5, true);
        AppendLine();
        AppendMethods<string>(5, true);
        AppendLine();
        AppendMethods<int>(5, false);
        AppendLine();
        AppendMethods<string>(5, false);

        Append("}");
    }

    private void AppendMethods<T>(int upToArgs, bool isPublic)
    {
        InvokeSequence(upToArgs + 1, i => AppendStatic<T>(i, isPublic), AppendType.NewLine);
    }

    private void AppendFunName<T>(int args, bool isPublic) => AppendFunName<T>(args, true, isPublic, true);

    private void AppendStatic<T>(int args, bool isPublic)
    {
        const string targetClass = "StubFunctions";
        const string extensionName = nameof(CreateStaticFunction);

        var testMethodNameBase = "Test_" + GenerateFunName<T>(args, true, isPublic);

        using (WithTestMethodScope(testMethodNameBase + "_Generic"))
        {
            // full generics
            AppendOffset2($"var f = {extensionName}");
            AppendGenerics<T>(args + 1, targetClass); // +1 for return type
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_A"))
        {
            // A
            AppendOffset2($"var f = typeof({targetClass}).{extensionName}A");
            AppendGenerics<T>(args);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        using (WithTestMethodScope(testMethodNameBase + "_AR"))
        {
            // AR
            AppendOffset2($"var f = typeof({targetClass}).{extensionName}");
            AppendGenerics<T>(args + 1);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_T"))
        {
            // T
            AppendOffset2($"var f = {extensionName}T");
            AppendGenerics<T>(0, targetClass);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendTypeOf<T>(args);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_R"))
        {
            // R
            AppendOffset2($"var f = typeof({targetClass}).{extensionName}R");
            AppendGenerics<T>(1);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendTypeOf<T>(args);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_TR"))
        {
            // TR
            AppendOffset2($"var f = {extensionName}TR");
            AppendGenerics<T>(1, targetClass); // +1 for return type
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendTypeOf<T>(args);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_X"))
        {
            // X
            AppendOffset2($"var f = typeof({targetClass}).{extensionName}X");
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendTypeOf<T>(args);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }
    }

    private void AppendInvokeAndAssert<T>(int args)
    {
        // Invoke
        AppendOffset2("var a = f(");
        AppendParameterValues<T>(args);
        AppendLine(");");

        // Assert
        if (typeof(T) == typeof(string))
        {
            if (args is 0)
            {
                AppendOffset2Line($"Assert.That(a, Is.Null);");
                return;
            }

            var expected = string.Join("", Enumerable.Range(1, args));
            AppendOffset2Line($"Assert.That(a, Is.EqualTo(\"{expected}\"));");
        }
        else
        {
            var expected = args * (args + 1) / 2;
            AppendOffset2Line($"Assert.That(a, Is.EqualTo({expected}));");
        }
    }
}