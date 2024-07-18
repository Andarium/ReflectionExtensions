using System;

namespace ReflectionExtensions.Tests.Generators;

public sealed class StubMethodsGenerator(bool generateProcedures) : GeneratorBase
{
    protected override string TypeName => generateProcedures ? "StubProcedures" : "StubFunctions";

    protected override void GenerateInternal()
    {
        using (WithStubFile(TypeName))
        {
            if (generateProcedures)
            {
                AppendLine("public static object Result;");
                AppendLine();
                AppendLine("public static void Reset() => Result = null;");
                AppendLine();
            }

            PermutationUtil.PermutateCall<int, Type, bool, bool>(GenerateSumFunction, AppendLine);
        }
    }

    private void GenerateSumFunction(int args, Type type, bool isStatic, bool isPublic)
    {
        var pTypeName = type.Name;

        Append(isPublic ? "public " : "private ");
        Append(isStatic ? "static " : "");
        Append(generateProcedures ? "void" : pTypeName);
        Append(' ');
        Append(isPublic ? "Public_" : "Private_");
        Append(isStatic ? "Static_" : "Instance_");
        Append("Sum");
        Append(args);
        Append("_");
        Append(pTypeName);
        Append('(');

        AppendSequence(args, i => $"{pTypeName} arg{i}", AppendType.Comma);

        AppendLine(")");

        using (WithIndentBraces())
        {
            Append(generateProcedures ? "Result = " : "return ");

            if (args is 0)
            {
                Append($"default({pTypeName})");
            }
            else
            {
                AppendSequence(args, i => $"arg{i}", AppendType.Plus);
            }

            AppendLine(';');
        }
    }
}