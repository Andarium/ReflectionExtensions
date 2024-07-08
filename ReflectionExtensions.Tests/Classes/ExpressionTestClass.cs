namespace ReflectionExtensions.Tests.Classes;

public sealed class ExpressionTestClass
{
    public int ValueField = 55;
    public TestObject RefField = new();

    public int ValueProperty { get; set; } = 66;
    public TestObject RefProperty { get; set; } = new();

    public static int StaticValueField;
    public static TestObject StaticRefField = default!;

    public static int StaticValueProperty { get; set; }
    public static TestObject StaticRefProperty { get; set; } = default!;

    static ExpressionTestClass() => Reset();

    public static void Reset()
    {
        StaticValueField = 77;
        StaticRefField = new TestObject();
        StaticValueProperty = 88;
        StaticRefProperty = new TestObject();
    }
}