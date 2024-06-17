// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantNullableDirective

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly Type[] EmptyTypes = Array.Empty<Type>();
        private static readonly object[] EmptyArgs = Array.Empty<object>();

        private static readonly Dictionary<Type, MethodInfo[]> MethodMap = new();
        private static readonly Map<Type, int, MethodInfo> MethodHashMap = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHashCode(string methodName, Type[] argTypes)
        {
            var a = argTypes;
            return a.Length switch
            {
                0 => HashCode.Combine(methodName),
                1 => HashCode.Combine(methodName, a[0]),
                2 => HashCode.Combine(methodName, a[0], a[1]),
                3 => HashCode.Combine(methodName, a[0], a[1], a[2]),
                4 => HashCode.Combine(methodName, a[0], a[1], a[2], a[3]),
                5 => HashCode.Combine(methodName, a[0], a[1], a[2], a[3], a[4]),
                6 => HashCode.Combine(methodName, a[0], a[1], a[2], a[3], a[4], a[5]),
                7 => HashCode.Combine(methodName, a[0], a[1], a[2], a[3], a[4], a[5], a[6]),
                8 => HashCode.Combine(GetHashCode(methodName, a.Take(7).ToArray()), a[7]),
                _ => HashCode.Combine(GetHashCode(methodName, a.Take(7).ToArray()), HashCode.Combine(a[7], a.Skip(8).ToArray()))
            };
        }

        private static IReadOnlyList<MethodInfo> GetAllMethods(this Type type)
        {
            if (MethodMap.TryGetValue(type, out var value))
            {
                return value;
            }

            value = FetchUpToRootBase(type, t => t.GetMethods(FetchAllDeclared));
            MethodMap.Add(type, value);
            return value;
        }

        private static bool IsMatchArguments(this MethodInfo method, params Type[] argTypes)
        {
            var parameters = method.GetParameters();

            if (parameters.Length != argTypes.Length)
            {
                return false;
            }

            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType != argTypes[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static MethodInfo GetMethodInfoInternal(this Type type, string methodName, bool isStatic, params Type[] argTypes)
        {
            var hash = GetHashCode(methodName, argTypes);
            if (MethodHashMap.TryGetValue(type, hash, out var method))
            {
                return method;
            }

            method = GetAllMethods(type).Where(x => x.Name == methodName && x.IsStatic == isStatic).FirstOrDefault(x => IsMatchArguments(x, argTypes));

            MethodHashMap[type, hash] = method ?? throw new InvalidOperationException(NotFoundMethodMessage(type, methodName, isStatic, MemberType.Method, argTypes));
            return method;
        }

        #region Method Info

        public static MethodInfo GetInstanceMethodInfo<T>(string methodName, params Type[] argTypes) => GetInstanceMethodInfo(typeof(T), methodName, argTypes);

        public static MethodInfo GetInstanceMethodInfo(this Type type, string methodName, params Type[] argTypes)
        {
            AssertType(type);
            return GetMethodInfoInternal(type, methodName, false, argTypes);
        }

        public static MethodInfo GetStaticMethodInfo<T>(string methodName, params Type[] argTypes) => GetStaticMethodInfo(typeof(T), methodName, argTypes);

        public static MethodInfo GetStaticMethodInfo(this Type type, string methodName, params Type[] argTypes)
        {
            AssertType(type);
            return GetMethodInfoInternal(type, methodName, true, argTypes);
        }

        #endregion

        public static object CallInstanceMethod<T>(this T instance, string methodName)
        {
            return CallInstanceMethodInternal(instance, methodName, EmptyArgs, EmptyTypes);
        }

        public static object CallInstanceMethod<T>(this T instance, string methodName, params object[] args)
        {
            return CallInstanceMethodInternal(instance, methodName, args, args.Select(x => x.GetType()).ToArray());
        }

        public static void CallInstanceMethod<T, TR>(this T instance, out TR result, string methodName, params object[] args)
        {
            result = (TR) CallInstanceMethodInternal(instance, methodName, args, args.Select(x => x.GetType()).ToArray());
        }

        public static TR CallInstanceMethod<T, TR>(this T instance, string methodName, params object[] args)
        {
            return (TR) CallInstanceMethodInternal(instance, methodName, args, args.Select(x => x.GetType()).ToArray());
        }

        public static object CallInstanceMethod<T>(this T instance, string methodName, object[] args, Type[] argTypes)
        {
            return CallInstanceMethodInternal(instance, methodName, args, argTypes);
        }

        public static void CallInstanceMethod<T, TR>(this T instance, out TR result, string methodName, object[] args, Type[] argTypes)
        {
            result = (TR) CallInstanceMethodInternal(instance, methodName, args, argTypes);
        }

        public static TR CallInstanceMethod<T, TR>(this T instance, string methodName, object[] args, Type[] argTypes)
        {
            return (TR) CallInstanceMethodInternal(instance, methodName, args, argTypes);
        }

        private static object CallInstanceMethodInternal<T>(this T? instance, string methodName, object[] args, Type[] argTypes)
        {
            var type = typeof(T);
            AssertInstance(instance, type, methodName, MemberType.Method);
            var method = GetInstanceMethodInfo(type, methodName, argTypes);
            return method.Invoke(instance, args);
        }

        public static object CallStaticMethod(this Type type, string methodName, params object[] args)
        {
            return CallStaticMethodInternal(type, methodName, args, args.Select(x => x.GetType()).ToArray());
        }

        public static void CallStaticMethod<TR>(this Type type, out TR result, string methodName, params object[] args)
        {
            result = (TR) CallStaticMethodInternal(type, methodName, args, args.Select(x => x.GetType()).ToArray());
        }

        public static TR CallStaticMethod<TR>(this Type type, string methodName, params object[] args)
        {
            return (TR) CallStaticMethodInternal(type, methodName, args, args.Select(x => x.GetType()).ToArray());
        }

        public static object CallStaticMethod(this Type type, string methodName, object[] args, Type[] argTypes)
        {
            return CallStaticMethodInternal(type, methodName, args, argTypes);
        }

        public static void CallStaticMethod<TR>(this Type type, out TR result, string methodName, object[] args, Type[] argTypes)
        {
            result = (TR) CallStaticMethodInternal(type, methodName, args, argTypes);
        }

        public static TR CallStaticMethod<TR>(this Type type, string methodName, object[] args, Type[] argTypes)
        {
            return (TR) CallStaticMethodInternal(type, methodName, args, argTypes);
        }

        private static object CallStaticMethodInternal(this Type type, string methodName, object[] args, Type[] argTypes)
        {
            var method = GetStaticMethodInfo(type, methodName, argTypes);
            return method.Invoke(null, args);
        }
    }
}