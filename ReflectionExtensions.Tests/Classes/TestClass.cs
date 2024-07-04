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

        public int Function() => Result;
        public int Function(int m) => Result * m;
        public string Function(int m, float a) => (Result * m + a).ToString();
        public double Function2(int m, float a) => Result * m + a;

        public static int StaticFunction() => Result;
        public static int StaticFunction(int m) => Result * m;
        public static string StaticFunction(int m, float a) => (Result * m + a).ToString();

        public void Procedure() => ChangedBy = nameof(Procedure);

        public static void StaticProcedure() => ChangedBy = nameof(StaticProcedure);

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

        private int Function() => Result;
        private int Function(int m) => Result * m;
        private string Function(int m, float a) => (Result * m + a).ToString();

        private static int StaticFunction() => Result;
        private static int StaticFunction(int m) => Result * m;
        private static string StaticFunction(int m, float a) => (Result * m + a).ToString();

        private void Procedure() => ChangedBy = nameof(Procedure);

        private static void StaticProcedure() => ChangedBy = nameof(StaticProcedure);

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

    public sealed class TestObject;
}