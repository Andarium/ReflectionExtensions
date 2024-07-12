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
            new StubMethodsGenerator(false),
            new StubMethodsGenerator(true),
            new StubConstructorsGenerator(true),
            new StubConstructorsGenerator(false),
            new TestStaticFunctionsGenerator(),
            new TestInstanceFunctionsGenerator(),
            new TestConstInstanceFunctionsGenerator(),
            new TestStaticProceduresGenerator(),
            new TestInstanceProceduresGenerator(),
            new TestConstInstanceProceduresGenerator(),
            new TestConstructorsGenerator(true),
            new TestConstructorsGenerator(false),
            new TestCallMethodGenerator(),
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