using System.Collections.Generic;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ReflectionExtensions
{
    /// <summary>
    /// Static constructors ignored
    /// </summary>
    public static partial class ReflectionExtensions
    {
        private static readonly Dictionary<Type, ConstructorInfo[]> ConstructorMap = new();
        private static readonly Map<Type, int, ConstructorInfo?> ConstructorHashMap = new();

#if NETSTANDARD2_1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHashCode(Type[] argTypes)
        {
            var a = argTypes;
            switch (a.Length)
            {
                case 0: return 0;
                case 1: return HashCode.Combine(a[0]);
                case 2: return HashCode.Combine(a[0], a[1]);
            }

            var hash = new HashCode();
            for (var i = 0; i < a.Length; i++)
            {
                hash.Add(a[i]);
            }

            return hash.ToHashCode();
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetHashCode(params Type[] argTypes)
        {
            if (argTypes.Length == 0)
            {
                return 0;
            }

            unchecked
            {
                var hash = 17;
                for (var i = 0; i < argTypes.Length; i++)
                {
                    hash = hash * 31 + argTypes[i].GetHashCode();
                }

                return hash;
            }
        }
#endif

        private static IReadOnlyList<ConstructorInfo> GetAllConstructors(this Type type)
        {
            if (ConstructorMap.TryGetValue(type, out var value))
            {
                return value;
            }

            value = FetchUpToRootBase(type, t => t.GetConstructors(FetchAllDeclared));
            ConstructorMap.Add(type, value);
            return value;
        }

        private static ConstructorInfo? GetConstructorInfoInternalOrNull([NotNull] this Type? type, params Type[] argTypes)
        {
            type.AssertType();
            var hash = GetHashCode(argTypes);
            if (ConstructorHashMap.TryGetValue(type, hash, out var constructor))
            {
                return constructor;
            }

            constructor = GetAllConstructors(type).Where(x => !x.IsStatic).FirstOrDefault(x => IsMatchArguments(x, argTypes));
            ConstructorHashMap[type, hash] = constructor;
            return constructor;
        }

        private static ConstructorInfo GetConstructorInfoInternal(this Type type, params Type[] argTypes)
        {
            var constructor = GetConstructorInfoInternalOrNull(type, argTypes);
            return constructor ?? throw new InvalidOperationException(NotFoundMethodMessage(type, ".ctor", false, MemberType.Constructor, argTypes));
        }

        #region Constructor Info

        public static ConstructorInfo? GetConstructorInfoOrNull<T>(params Type[] argTypes) => GetConstructorInfoOrNull(typeof(T), argTypes);
        public static ConstructorInfo GetConstructorInfo<T>(params Type[] argTypes) => GetConstructorInfo(typeof(T), argTypes);
        public static ConstructorInfo? GetConstructorInfoOrNull(this Type type, params Type[] argTypes) => GetConstructorInfoInternalOrNull(type, argTypes);
        public static ConstructorInfo GetConstructorInfo(this Type type, params Type[] argTypes) => GetConstructorInfoInternal(type, argTypes);

        #endregion

        public static T CallConstructor<T>(Args args = default) => (T) CallConstructor(typeof(T), args);

        public static object CallConstructor(this Type targetType, Args args = default)
        {
            AssertType(targetType);
            var method = GetConstructorInfo(targetType, args.Types);
            return method.Invoke(args.Values);
        }
    }
}