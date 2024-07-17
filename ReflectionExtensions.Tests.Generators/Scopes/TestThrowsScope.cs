using System;

namespace ReflectionExtensions.Tests.Generators.Scopes;

internal readonly struct TestThrowsScope<T> : IDisposable
    where T : Exception
{
    private readonly IndentBracesScope _indentScope;

    public TestThrowsScope(GeneratorBase generator)
    {
        generator.AppendLine($"Assert.Throws<{typeof(T).Name}>(() =>");
        _indentScope = new IndentBracesScope(generator, closing: "});");
    }

    public void Dispose()
    {
        _indentScope.Dispose();
    }
}