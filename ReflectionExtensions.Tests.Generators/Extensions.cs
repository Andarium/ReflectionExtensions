namespace ReflectionExtensions.Tests.Generators;

public static class Extensions
{
    public static string Wrap<T>(this T value, char c = '"') => Wrap(value, true, c, c);
    public static string Wrap<T>(this T value, string c = "\"") => Wrap(value, true, c, c);
    public static string Wrap<T>(this T value, bool wrap, char start, char end) => Wrap(value, wrap, start.ToString(), end.ToString());
    public static string Wrap<T>(this T value, string start, string end) => Wrap(value, true, start, end);
    public static string Wrap<T>(this T value, bool wrap, string start = "\"", string end = "\"")
    {
        return wrap ? $"{start}{value}{end}" : value!.ToString();
    }
}