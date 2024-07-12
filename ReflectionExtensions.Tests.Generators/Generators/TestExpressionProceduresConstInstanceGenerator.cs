using System.Linq;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestExpressionProceduresConstInstanceGenerator : GeneratorBase
{
    protected override string TypeName => "TestExpressionProceduresConstInstance";

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
        const string extensionName = nameof(ReflectionExtensions.CreateConstInstanceProcedure);

        var testMethodNameBase = "Test_" + GenerateMethodName<T>(args, false, isPublic);

        using (WithTestMethodScope(testMethodNameBase + "_Generic"))
        {
            // full generics
            AppendLine($"var instance = new {targetClass}();");
            Append($"var f = instance.{extensionName}");
            AppendGenerics<T>(args);
            Append("(");
            AppendFunName<T>(args, isPublic);
            AppendLine(");");
            AppendInvokeAndAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_X"))
        {
            // X
            AppendLine($"var instance = new {targetClass}();");
            Append($"var f = instance.{extensionName}");
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

        // Assert
        if (typeof(T) == typeof(string))
        {
            if (args is 0)
            {
                AppendLine("Assert.That(StubProcedures.Result, Is.Null);");
                return;
            }

            var expected = string.Join("", Enumerable.Range(1, args));
            AppendLine($"Assert.That(StubProcedures.Result, Is.EqualTo(\"{expected}\"));");
        }
        else
        {
            var expected = args * (args + 1) / 2;
            AppendLine($"Assert.That(StubProcedures.Result, Is.EqualTo({expected}));");
        }
    }
}