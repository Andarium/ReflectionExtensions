using System;

namespace ReflectionExtensions.Tests.Generators.Scopes;

public readonly struct IndentScope : IDisposable
{
    private readonly GeneratorBase _generatorBase;

    public IndentScope(GeneratorBase generatorBase)
    {
        _generatorBase = generatorBase;
        _generatorBase.AddOffset();
    }

    public void Dispose()
    {
        _generatorBase.RemoveOffset();
    }
}