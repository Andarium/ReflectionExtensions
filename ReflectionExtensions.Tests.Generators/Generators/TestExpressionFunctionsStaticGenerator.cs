using System.Linq;
using static ReflectionExtensions.ReflectionExtensions;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestExpressionFunctionsStaticGenerator : GeneratorBase
{
    protected override string TypeName => "TestExpressionFunctionsStatic";

    protected override void GenerateInternal()
    {
        using (WithTestFile(TypeName))
        {
            AppendMethods<int>(5, true);
            AppendLine();
            AppendMethods<string>(5, true);
            AppendLine();
            AppendMethods<int>(5, false);
            AppendLine();
            AppendMethods<string>(5, false);
        }
    }

    private void AppendMethods<T>(int upToArgs, bool isPublic)
    {
        InvokeSequence(upToArgs + 1, i => AppendStatic<T>(i, isPublic), AppendType.NewLine);
    }

    private void AppendFunName<T>(int args, bool isPublic) => AppendMethodName<T>(args, true, isPublic, true);

    private void AppendStatic<T>(int args, bool isPublic)
    {
        const string targetClass = "StubFunctions";
        const string extensionName = nameof(CreateStaticFunction);

        var testMethodNameBase = "Test_" + GenerateMethodName<T>(args, true, isPublic);

        using (WithTestMethodScope(testMethodNameBase + "_Generic"))
        {
            // full generics
            Append($"var f = {extensionName}");
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
            Append($"var f = typeof({targetClass}).{extensionName}A");
            AppendGenerics<T>(args);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        using (WithTestMethodScope(testMethodNameBase + "_AR"))
        {
            // AR
            Append($"var f = typeof({targetClass}).{extensionName}");
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
            Append($"var f = {extensionName}T");
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
            Append($"var f = typeof({targetClass}).{extensionName}R");
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
            Append($"var f = {extensionName}TR");
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
            Append($"var f = typeof({targetClass}).{extensionName}X");
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
        Append("var actual = f(");
        AppendParameterValues<T>(args);
        AppendLine(");");

        // Assert
        AppendSumAssert<T>(args);
    }
}