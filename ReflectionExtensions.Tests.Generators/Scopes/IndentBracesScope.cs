using System;

namespace ReflectionExtensions.Tests.Generators.Scopes;

public readonly struct IndentBracesScope : IDisposable
{
    private readonly GeneratorBase _generatorBase;
    private readonly IndentScope _indentScope;

    public IndentBracesScope(GeneratorBase generatorBase, string? title = null)
    {
        _generatorBase = generatorBase;

        if (title != null)
        {
            _generatorBase.AppendLine(title);
        }

        _generatorBase.AppendLine('{');

        _indentScope = new IndentScope(_generatorBase);
    }

    public void Dispose()
    {
        _indentScope.Dispose();
        _generatorBase.AppendLine('}');
    }
}