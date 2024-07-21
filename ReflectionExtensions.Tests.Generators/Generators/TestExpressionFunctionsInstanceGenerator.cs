using System;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestExpressionFunctionsInstanceGenerator : GeneratorBase
{
    protected override string TypeName => "TestExpressionFunctionsInstance";

    protected override void GenerateInternal()
    {
        using (WithTestFile(TypeName))
        {
            PermutationUtil.PermutateCall<Type, int, bool, FunctionType>(AppendTestMethod, AppendLine);
        }
    }

    private void AppendTestMethod(Type type, int args, bool isPublic, FunctionType functionType)
    {
        const string targetClass = "StubFunctions";

        var methodName = "Test_" + GenerateMethodName(type, args, false, isPublic) + "_" + functionType;

        using (WithTestMethodScope(methodName))
        {
            AppendLine($"var instance = new {targetClass}();");

            var target = functionType.HasEnumFlag(FunctionType.T) ? "" : $"typeof({targetClass}).";

            Append($"var f = {target}{functionType.GetExtensionName()}");

            var genericArgs = 0;
            if (functionType.HasEnumFlag(FunctionType.A))
            {
                genericArgs += args;
            }

            if (functionType.HasEnumFlag(FunctionType.R))
            {
                genericArgs++;
            }

            var t = functionType.HasEnumFlag(FunctionType.T) ? targetClass : null;

            AppendGenerics(type, genericArgs, t);

            Append("(");
            AppendMethodName(type, args, false, isPublic, true);
            if (!functionType.HasEnumFlag(FunctionType.A))
            {
                AppendTypeOf(type, args);
            }

            AppendLine(");");
            AppendInvokeAndAssert(type, args);
        }
    }

    private void AppendInvokeAndAssert(Type type, int args)
    {
        // Invoke
        Append("var actual = f(instance");
        AppendParameterValues(type, args, true);
        AppendLine(");");

        // Assert
        AppendSumAssert(type, args);
    }
}

[Flags]
public enum FunctionType
{
    X = 0,
    T = 1,
    A = 2,
    R = 4,
    TA = T | A,
    TR = T | R,
    AR = A | R,
    Generic = T | A | R,
}