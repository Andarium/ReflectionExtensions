namespace ReflectionExtensions.Tests.Generators;

public readonly struct SourceFile(string fileName, string source)
{
    public readonly string FileName = fileName;
    public readonly string Source = source;
}