// ReSharper disable All

#nullable enable

#pragma warning disable CA1822
#pragma warning disable CA2211
#pragma warning disable CS0414 // Field is assigned but its value is never used

namespace ReflectionExtensions.Tests.Classes
{
    public class TestClass_Public
    {
        private const int Result = 777;
        public static string? ChangedBy;

        public int Field = Result;
        public static int StaticField = Result;

        public int Property { get; set; } = Result;
        public static int StaticProperty { get; set; } = Result;

        public int Method() => Result;
        public int Method(int m) => Result * m;
        public int Method(int m, int a) => Result * m + a;

        public static int StaticMethod() => Result;
        public static int StaticMethod(int m) => Result * m;
        public static int StaticMethod(int m, int a) => Result * m + a;

        public void VoidMethod() => ChangedBy = nameof(VoidMethod);

        public static void StaticVoidMethod() => ChangedBy = nameof(StaticVoidMethod);

        public static void Reset()
        {
            StaticField = Result;
            StaticProperty = Result;
            ChangedBy = null;
        }
    }

    public class TestClass_Private
    {
        private const int Result = 777;
        public static string? ChangedBy;

        private int Field = Result;
        private static int StaticField = Result;

        private int Property { get; set; } = Result;
        private static int StaticProperty { get; set; } = Result;

        private int Method() => Result;
        private int Method(int m) => Result * m;
        private int Method(int m, int a) => Result * m + a;

        private static int StaticMethod() => Result;
        private static int StaticMethod(int m) => Result * m;
        private static int StaticMethod(int m, int a) => Result * m + a;

        private void VoidMethod() => ChangedBy = nameof(VoidMethod);

        private static void StaticVoidMethod() => ChangedBy = nameof(StaticVoidMethod);

        public int FieldPublic => Field;
        public int PropertyPublic => Property;
        public static int StaticFieldPublic => StaticField;
        public static int StaticPropertyPublic => StaticProperty;

        public static void Reset()
        {
            StaticField = Result;
            StaticProperty = Result;
            ChangedBy = null;
        }
    }
}