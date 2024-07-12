namespace ReflectionExtensions.Tests.Generators;

public sealed class StubConstructorsGenerator(bool isPublic) : GeneratorBase
{
    protected override string TypeName => isPublic ? "StubPublicConstructors" : "StubPrivateConstructors";

    protected override void GenerateInternal()
    {
        using (WithStubFile(TypeName))
        {
            AppendLine("public object Result;");
            AppendLine();

            GenerateConstructors<int>(5, false);
            AppendLine();
            GenerateConstructors<string>(5, true);
        }
    }

    private void GenerateConstructors<T>(int upToArgs, bool skipFirst)
    {
        var start = skipFirst ? 1 : 0;
        for (var args = start; args <= upToArgs; args++)
        {
            GenerateConstructor<T>(args);

            if (args < upToArgs)
            {
                AppendLine();
            }
        }
    }

    private void GenerateConstructor<T>(int args)
    {
        var pTypeName = typeof(T).Name;

        Append(isPublic ? "public " : "private ");
        Append(TypeName);
        Append('(');

        AppendSequence(args, i => $"{pTypeName} arg{i}", AppendType.Comma);
        AppendLine(")");

        using (WithIndentBraces())
        {
            Append("Result = ");
            AppendNewArray<T>(args, i => $"arg{i}");
            AppendLine(';');
        }
    }
}