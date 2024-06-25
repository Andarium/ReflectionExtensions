namespace ReflectionExtensions.Tests.Classes;

public sealed class ExpressionTestClass
{
    public int ValueField = 55;
    public TestObject RefField = new();

    public int ValueProperty { get; set; } = 66;
    public TestObject RefProperty { get; set; } = new();

    public static int StaticValueField = 77;
    public static TestObject StaticRefField = new();
    public static int StaticValueProperty { get; set; } = 88;
    public static TestObject StaticRefProperty { get; set; } = new();
}