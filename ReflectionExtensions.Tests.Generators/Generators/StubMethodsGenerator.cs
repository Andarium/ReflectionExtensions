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
                AppendLine("public static void Clear() => Result = null;");
            }

            GenerateSumFunctions<int>(5, true, false);
            AppendLine();
            GenerateSumFunctions<int>(5, false, false);
            AppendLine();
            GenerateSumFunctions<int>(5, true, true);
            AppendLine();
            GenerateSumFunctions<int>(5, false, true);
            AppendLine();

            GenerateSumFunctions<string>(5, true, false);
            AppendLine();
            GenerateSumFunctions<string>(5, false, false);
            AppendLine();
            GenerateSumFunctions<string>(5, true, true);
            AppendLine();
            GenerateSumFunctions<string>(5, false, true);
        }
    }

    private void GenerateSumFunctions<T>(int upToArgs, bool isPublic, bool isStatic)
    {
        for (var args = 0; args <= upToArgs; args++)
        {
            GenerateSumFunction<T>(args, isPublic, isStatic);

            if (args < upToArgs)
            {
                AppendLine();
            }
        }
    }

    private void GenerateSumFunction<T>(int args, bool isPublic, bool isStatic)
    {
        var pTypeName = typeof(T).Name;

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