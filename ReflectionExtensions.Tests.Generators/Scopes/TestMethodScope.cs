using System;

namespace ReflectionExtensions.Tests.Generators.Scopes;

internal readonly struct TestMethodScope : IDisposable
{
    private readonly IndentBracesScope _indentScope;

    public TestMethodScope(GeneratorBase generator, string methodName)
    {
        generator.AppendLine("[Test]");
        generator.AppendLine($"public void {methodName}()");

        _indentScope = new IndentBracesScope(generator);
    }

    public void Dispose()
    {
        _indentScope.Dispose();
    }
}