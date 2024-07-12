using System.Linq;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestConstInstanceFunctionsGenerator : GeneratorBase
{
    protected override string TypeName => "TestConstInstanceFunctions";

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
        InvokeSequence(upToArgs + 1, i => AppendConstInstance<T>(i, isPublic), AppendType.NewLine);
    }

    private void AppendFunName<T>(int args, bool isPublic) => AppendMethodName<T>(args, false, isPublic, true);

    private void AppendConstInstance<T>(int args, bool isPublic)
    {
        const string targetClass = "StubFunctions";
        const string extensionName = nameof(ReflectionExtensions.CreateConstInstanceFunction);

        var testMethodNameBase = "Test_" + GenerateMethodName<T>(args, false, isPublic);

        using (WithTestMethodScope(testMethodNameBase + "_Generic"))
        {
            // full generics
            AppendOffset2Line($"var instance = new {targetClass}();");
            AppendOffset2($"var f = instance.{extensionName}");
            AppendGenerics<T>(args + 1); // +1 for return type
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_A"))
        {
            // A
            AppendOffset2Line($"var instance = new {targetClass}();");
            AppendOffset2($"var f = instance.{extensionName}A");
            AppendGenerics<T>(args);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_R"))
        {
            // R
            AppendOffset2Line($"var instance = new {targetClass}();");
            AppendOffset2($"var f = instance.{extensionName}R");
            AppendGenerics<T>(1);
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
            AppendOffset2Line($"var instance = new {targetClass}();");
            AppendOffset2($"var f = instance.{extensionName}");
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