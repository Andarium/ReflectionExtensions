using Microsoft.CodeAnalysis;

namespace ReflectionExtensions.Tests.Generators;

[Generator]
public class TestSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(AddSources);
    }

    private static void AddSources(IncrementalGeneratorPostInitializationContext ctx)
    {
        IGenerator[] generators =
        [
            new StubMethodsGenerator("StubFunctions", false),
            new StubMethodsGenerator("StubProcedures", true),
            new TestStaticFunctionsGenerator(),
            new TestConstInstanceFunctionsGenerator(),
            new TestInstanceFunctionsGenerator()
        ];

        AddGenerators(ctx, generators);
    }

    private static void AddGenerators(IncrementalGeneratorPostInitializationContext ctx, params IGenerator[] generators)
    {
        foreach (var generator in generators)
        {
            var source = generator.Generate();
            ctx.AddSource(source.FileName, source.Source);
        }
    }
}