using System;

namespace ReflectionExtensions.Tests.Generators.Scopes;

internal readonly struct AttributeMethodScope : IDisposable
{
    private readonly IndentBracesScope _indentScope;

    public AttributeMethodScope(GeneratorBase generator, string methodName, string attribute = "Test")
    {
        generator.AppendLine($"[{attribute}]");
        generator.AppendLine($"public void {methodName}()");

        _indentScope = new IndentBracesScope(generator);
    }

    public void Dispose()
    {
        _indentScope.Dispose();
    }
}