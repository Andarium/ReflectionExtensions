using System.Linq;
using static ReflectionExtensions.ReflectionExtensions;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestExpressionProceduresStaticGenerator : GeneratorBase
{
    protected override string TypeName => "TestExpressionProceduresStatic";

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
        InvokeSequence(upToArgs + 1, i => AppendConstInstance<T>(i, isPublic), AppendType.NewLine);
    }

    private void AppendFunName<T>(int args, bool isPublic) => AppendMethodName<T>(args, true, isPublic, true);

    private void AppendConstInstance<T>(int args, bool isPublic)
    {
        const string targetClass = "StubProcedures";
        const string extensionName = nameof(CreateStaticProcedure);

        var testMethodNameBase = "Test_" + GenerateMethodName<T>(args, true, isPublic);

        using (WithTestMethodScope(testMethodNameBase + "_Generic"))
        {
            // full generics
            Append($"var f = {extensionName}");
            AppendGenerics<T>(args, targetClass);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_A"))
        {
            // A
            Append($"var f = typeof({targetClass}).{extensionName}");
            AppendGenerics<T>(args);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        using (WithTestMethodScope(testMethodNameBase + "_T"))
        {
            // T
            Append($"var f = {extensionName}");
            AppendGenerics<T>(0, targetClass);
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
            Append($"var f = typeof({targetClass}).{extensionName}");
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
        Append("f(");
        AppendParameterValues<T>(args);
        AppendLine(");");
        AppendLine("var actual = StubProcedures.Result;");

        // Assert
        AppendSumAssert<T>(args);
    }
}