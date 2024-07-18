using System;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestExpressionFunctionsConstInstanceGenerator : GeneratorBase
{
    protected override string TypeName => "TestExpressionFunctionsConstInstance";

    protected override void GenerateInternal()
    {
        using (WithTestFile(TypeName))
        {
            PermutationUtil.PermutateCall<Type, int, bool, ConstFunctionType>(AppendTestMethod, AppendLine);
        }
    }

    private void AppendTestMethod(Type type, int args, bool isPublic, ConstFunctionType functionType)
    {
        const string targetClass = "StubFunctions";

        var methodName = "Test_" + GenerateMethodName(type, args, false, isPublic) + "_" + functionType;

        using (WithTestMethodScope(methodName))
        {
            AppendLine($"var instance = new {targetClass}();");

            Append($"var f = instance.{functionType.GetExtensionName()}");

            var genericArgs = 0;
            if (functionType.HasEnumFlag(ConstFunctionType.A))
            {
                genericArgs += args;
            }

            if (functionType.HasEnumFlag(ConstFunctionType.R))
            {
                genericArgs++;
            }

            AppendGenerics(type, genericArgs);

            Append("(");
            AppendMethodName(type, args, false, isPublic, true);
            if (!functionType.HasEnumFlag(ConstFunctionType.A))
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
        Append("var actual = f(");
        AppendParameterValues(type, args);
        AppendLine(");");

        // Assert
        AppendSumAssert(type, args);
    }
}

[Flags]
public enum ConstFunctionType
{
    X = 0,
    A = 1,
    R = 2,
    Generic = A | R,
}