using System;
using System.Linq;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        public static void CallInstanceMethod<T, TR>(this T instance, out TR result, string methodName, params object[] args)
        {
            result = (TR) CallInstanceMethodInternal(instance, methodName, args, args.Select(x => x.GetType()).ToArray());
        }

        public static void CallInstanceMethod<T, TR>(this T instance, out TR result, string methodName, object[] args, Type[] argTypes)
        {
            result = (TR) CallInstanceMethodInternal(instance, methodName, args, argTypes);
        }

        public static void CallStaticMethod<TR>(this Type type, out TR result, string methodName, params object[] args)
        {
            result = (TR) CallStaticMethodInternal(type, methodName, args, args.Select(x => x.GetType()).ToArray());
        }

        public static void CallStaticMethod<TR>(this Type type, out TR result, string methodName, object[] args, Type[] argTypes)
        {
            result = (TR) CallStaticMethodInternal(type, methodName, args, argTypes);
        }
    }
}