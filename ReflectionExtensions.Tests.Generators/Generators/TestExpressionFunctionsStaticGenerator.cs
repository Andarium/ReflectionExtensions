using System;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestExpressionFunctionsStaticGenerator : GeneratorBase
{
    protected override string TypeName => "TestExpressionFunctionsStatic";
    private const string StubClass = "StubFunctions";

    protected override void GenerateInternal()
    {
        using (WithTestFile(TypeName))
        {
            PermutationUtil.PermutateCall<Type, int, bool, StaticFunctionType>(AppendTestMethod, AppendLine);
        }
    }

    private void AppendTestMethod(Type type, int args, bool isPublic, StaticFunctionType functionType)
    {
        var methodName = "Test_" + GenerateMethodName(type, args, true, isPublic) + "_" + functionType;

        using (WithTestMethodScope(methodName))
        {
            var target = functionType.HasEnumFlag(StaticFunctionType.T) ? "" : $"typeof({StubClass}).";

            Append($"var f = {target}{functionType.GetExtensionName()}");

            var genericArgs = 0;
            if (functionType.HasEnumFlag(StaticFunctionType.A))
            {
                genericArgs += args;
            }

            if (functionType.HasEnumFlag(StaticFunctionType.R))
            {
                genericArgs++;
            }

            var t = functionType.HasEnumFlag(StaticFunctionType.T) ? StubClass : null;

            AppendGenerics(type, genericArgs, t);

            Append("(");
            AppendMethodName(type, args, false, isPublic, true);
            if (!functionType.HasEnumFlag(StaticFunctionType.A))
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
public enum StaticFunctionType
{
    X = 0,
    T = 1,
    A = 2,
    R = 4,
    TA = T | A,
    TR = T | R,
    AR = A | R,
    Generic = T | A | R
}