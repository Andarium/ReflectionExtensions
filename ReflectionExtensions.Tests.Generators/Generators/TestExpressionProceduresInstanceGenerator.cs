﻿using System.Linq;
using static ReflectionExtensions.ReflectionExtensions;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestExpressionProceduresInstanceGenerator : GeneratorBase
{
    protected override string TypeName => "TestExpressionProceduresInstance";

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

    private void AppendFunName<T>(int args, bool isPublic) => AppendMethodName<T>(args, false, isPublic, true);

    private void AppendConstInstance<T>(int args, bool isPublic)
    {
        const string targetClass = "StubProcedures";
        const string extensionName = nameof(CreateInstanceProcedure);

        var testMethodNameBase = "Test_" + GenerateMethodName<T>(args, false, isPublic);

        using (WithTestMethodScope(testMethodNameBase + "_Generic"))
        {
            // full generics == TA
            AppendLine($"var instance = new {targetClass}();");
            Append($"var f = {extensionName}");
            AppendGenerics<T>(args, targetClass); // +1 for return type
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_A"))
        {
            // A
            AppendLine($"var instance = new {targetClass}();");
            Append($"var f = typeof({targetClass}).{extensionName}A");
            AppendGenerics<T>(args);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_T"))
        {
            // T
            AppendLine($"var instance = new {targetClass}();");
            Append($"var f = {extensionName}T");
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
            AppendLine($"var instance = new {targetClass}();");
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
        Append("f(instance");
        AppendParameterValues<T>(args, true);
        AppendLine(");");
        AppendLine("var actual = StubProcedures.Result;");

        // Assert
        AppendSumAssert<T>(args);
    }
}