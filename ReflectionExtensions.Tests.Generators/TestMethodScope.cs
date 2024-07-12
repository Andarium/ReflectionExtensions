using System;

namespace ReflectionExtensions.Tests.Generators;

internal readonly struct TestMethodScope : IDisposable
{
    private readonly GeneratorBase _generator;

    public TestMethodScope(GeneratorBase generator, string methodName)
    {
        _generator = generator;

        _generator.AppendOffsetLine("[Test]");
        _generator.AppendOffset("public void ");
        _generator.Append(methodName);
        _generator.AppendLine("()");
        _generator.AppendOffsetLine("{");
    }

    public void Dispose()
    {
        _generator.AppendOffsetLine("}");
    }
}