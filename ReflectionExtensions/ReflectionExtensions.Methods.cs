﻿// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantNullableDirective

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly Dictionary<Type, MethodInfo[]> MethodMap = new();
        private static readonly Map<Type, int, MethodInfo?> MethodHashMap = new();

#if NETSTANDARD2_1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHashCode(string methodName, Type[] argTypes)
        {
            var a = argTypes;
            switch (a.Length)
            {
                case 0: return HashCode.Combine(methodName);
                case 1: return HashCode.Combine(methodName, a[0]);
                case 2: return HashCode.Combine(methodName, a[0], a[1]);
            }

            var hash = new HashCode();
            hash.Add(methodName);
            for (var i = 0; i < a.Length; i++)
            {
                hash.Add(a[i]);
            }

            return hash.ToHashCode();
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHashCode(string methodName, params Type[] argTypes)
        {
            if (argTypes.Length == 0)
            {
                return methodName.GetHashCode();
            }

            unchecked
            {
                var hash = 17 + methodName.GetHashCode();
                for (var i = 0; i < argTypes.Length; i++)
                {
                    hash = hash * 31 + argTypes[i].GetHashCode();
                }

                return hash;
            }
        }
#endif

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

        private static MethodInfo? GetMethodInfoInternalOrNull([NotNull] this Type? type, string methodName, bool isStatic, params Type[] argTypes)
        {
            type.AssertType();
            var hash = GetHashCode(methodName, argTypes);
            if (MethodHashMap.TryGetValue(type, hash, out var method))
            {
                return method;
            }

            method = GetAllMethods(type).Where(x => x.Name == methodName && x.IsStatic == isStatic).FirstOrDefault(x => IsMatchArguments(x, argTypes));
            MethodHashMap[type, hash] = method;
            return method;
        }

        private static MethodInfo GetMethodInfoInternal(this Type type, string methodName, bool isStatic, params Type[] argTypes)
        {
            var method = GetMethodInfoInternalOrNull(type, methodName, isStatic, argTypes);
            return method ?? throw new InvalidOperationException(NotFoundMethodMessage(type, methodName, isStatic, MemberType.Method, argTypes));
        }

        #region Method Info

        public static MethodInfo? GetInstanceMethodInfoOrNull<T>(string methodName, params Type[] argTypes) => GetInstanceMethodInfoOrNull(typeof(T), methodName, argTypes);
        public static MethodInfo GetInstanceMethodInfo<T>(string methodName, params Type[] argTypes) => GetInstanceMethodInfo(typeof(T), methodName, argTypes);

        public static MethodInfo? GetInstanceMethodInfoOrNull(this Type type, string methodName, params Type[] argTypes) => GetMethodInfoInternalOrNull(type, methodName, false, argTypes);
        public static MethodInfo GetInstanceMethodInfo(this Type type, string methodName, params Type[] argTypes) => GetMethodInfoInternal(type, methodName, false, argTypes);

        public static MethodInfo? GetStaticMethodInfoOrNull<T>(string methodName, params Type[] argTypes) => GetStaticMethodInfoOrNull(typeof(T), methodName, argTypes);
        public static MethodInfo GetStaticMethodInfo<T>(string methodName, params Type[] argTypes) => GetStaticMethodInfo(typeof(T), methodName, argTypes);

        public static MethodInfo? GetStaticMethodInfoOrNull(this Type type, string methodName, params Type[] argTypes) => GetMethodInfoInternalOrNull(type, methodName, true, argTypes);
        public static MethodInfo GetStaticMethodInfo(this Type type, string methodName, params Type[] argTypes) => GetMethodInfoInternal(type, methodName, true, argTypes);

        #endregion

        public static TResult CallInstanceMethod<TResult>(this object instance, string methodName, Args args = default)
        {
            return (TResult) CallInstanceMethod(instance, methodName, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object CallInstanceMethod(this object instance, string methodName, Args args = default)
        {
            AssertInstance(instance, out var instanceType, methodName, MemberType.Method);
            return GetInstanceMethodInfo(instanceType, methodName, args.Types).Invoke(instance, args.Values);
        }

        public static TResult CallStaticMethod<TResult>(this Type type, string methodName, Args args = default)
        {
            return (TResult) CallStaticMethod(type, methodName, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object CallStaticMethod(this Type type, string methodName, Args args = default)
        {
            var method = GetStaticMethodInfo(type, methodName, args.Types);
            return method.Invoke(null, args.Values);
        }
        
        public static TResult CallStaticMethod<TTarget, TResult>(string methodName, Args args = default)
        {
            return (TResult) CallStaticMethod<TTarget>(methodName, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object CallStaticMethod<TTarget>(string methodName, Args args = default)
        {
            var method = GetStaticMethodInfo(typeof(TTarget), methodName, args.Types);
            return method.Invoke(null, args.Values);
        }
    }
}