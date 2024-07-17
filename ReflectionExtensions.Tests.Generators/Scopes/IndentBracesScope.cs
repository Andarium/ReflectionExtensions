using System;

namespace ReflectionExtensions.Tests.Generators.Scopes;

public readonly struct IndentBracesScope : IDisposable
{
    private readonly GeneratorBase _generatorBase;
    private readonly IndentScope _indentScope;
    private readonly string _closing;

    public IndentBracesScope(GeneratorBase generatorBase, string? title = null, string opening = "{",  string closing = "}")
    {
        _closing = closing;
        _generatorBase = generatorBase;

        if (title is not null)
        {
            _generatorBase.AppendLine(title);
        }

        _generatorBase.AppendLine(opening);

        _indentScope = new IndentScope(_generatorBase);
    }

    public void Dispose()
    {
        _indentScope.Dispose();
        _generatorBase.AppendLine(_closing);
    }
}