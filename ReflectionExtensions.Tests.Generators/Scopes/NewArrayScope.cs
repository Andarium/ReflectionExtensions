using System;

namespace ReflectionExtensions.Tests.Generators.Scopes;

internal readonly struct NewArrayScope<T> : IDisposable
{
    private readonly GeneratorBase _generator;

    public NewArrayScope(GeneratorBase generator)
    {
        _generator = generator;
        _generator.Append("new ");
        _generator.Append(typeof(T).Name);
        _generator.Append("[] { ");
    }

    public void Dispose()
    {
        _generator.Append(" }");
    }

    public static string GetEmptyArray()
    {
        return $"new {typeof(T).Name}[] {{ }}";
    }
}