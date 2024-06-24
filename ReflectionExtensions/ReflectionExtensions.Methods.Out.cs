using System;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        public static void CallInstanceMethod<T, TR>(this T instance, out TR result, string methodName, Args args = default) => result = (TR) CallInstanceMethod(instance, methodName, args);

        public static void CallStaticMethod<TR>(this Type type, out TR result, string methodName, Args args = default) => result = (TR) CallStaticMethod(type, methodName, args);
    }
}