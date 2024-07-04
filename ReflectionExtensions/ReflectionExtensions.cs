// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantNullableDirective

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Reflection.BindingFlags;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private const BindingFlags FetchAllDeclared = Public | NonPublic | Static | Instance | DeclaredOnly;

        private const string NotFound = "Can't find {0} {1} {2} in {3} type";
        private const string NotFoundMethod = "Can't find {0} {1} {2}({4}) in {3} type";
        private const string NullInstance = "Null instance. {0} {1}, {2} type";
        private const string CastString = "Can't assign {3} to {0}. {0} {1}, {2} type";

        public static void ClearCache()
        {
            FieldMap.Clear();
            PropertyMap.Clear();
            MethodMap.Clear();

            FieldNameMap.Clear();
            PropertyNameMap.Clear();
            MethodHashMap.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string NotFoundMessage(Type? type, string memberName, bool isStatic, MemberType memberType)
        {
            var modifier = isStatic ? "static" : "instance";
            var name = type?.AssemblyQualifiedName;
            return string.Format(NotFound, modifier, memberType, memberName, name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string NotFoundMethodMessage(Type? type, string memberName, bool isStatic, MemberType memberType, Type[] args)
        {
            var modifier = isStatic ? "static" : "instance";
            var name = type?.AssemblyQualifiedName;
            var argsText = string.Join(", ", args.Select(x => x.Name));
            return string.Format(NotFoundMethod, modifier, memberType, memberName, name, argsText);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string NullInstanceMessage(Type? type, string memberName, MemberType memberType)
        {
            var name = type?.AssemblyQualifiedName;
            return string.Format(NullInstance, memberType, memberName, name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string CastMessage(Type requestedType, string memberName, MemberType memberType, Type actualInstanceType)
        {
            var name = requestedType.AssemblyQualifiedName;
            return string.Format(CastString, memberType, memberName, name, actualInstanceType.AssemblyQualifiedName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AssertInstance([NotNull] object? instance, [NotNull] ref Type? instanceType, string memberName, MemberType memberType)
        {
            if (instance is null)
            {
                throw new NullReferenceException(NullInstanceMessage(instanceType, memberName, memberType));
            }

            instanceType ??= instance.GetType();

            if (instanceType.IsInstanceOfType(instance) is false)
            {
                throw new ArgumentException(CastMessage(instanceType, memberName, memberType, instance.GetType()));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AssertInstance([NotNull] object? instance, string memberName, MemberType memberType)
        {
            if (instance is null)
            {
                throw new NullReferenceException(NullInstanceMessage(null, memberName, memberType));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type AssertType([NotNull] this Type? type)
        {
            if (type is null)
            {
                throw new NullReferenceException("Null type.");
            }

            return type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type AssertType<T>([NotNull] this Type? type)
        {
            if (type is null)
            {
                throw new NullReferenceException("Null type.");
            }

            if (type != typeof(T))
            {
                throw new ArgumentException($"Type mismatch. {type.AssemblyQualifiedName} != {typeof(T).AssemblyQualifiedName}");
            }

            return type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type AssertConstructor<T>([NotNull] this ConstructorInfo? info)
        {
            if (info is null)
            {
                throw new NullReferenceException("Null constructor info.");
            }

            var type = info.DeclaringType;

            if (type is null)
            {
                throw new NullReferenceException($"Null constructor declaring type. T = {typeof(T).AssemblyQualifiedName}");
            }

            if (type != typeof(T))
            {
                throw new ArgumentException($"Type mismatch. {type.AssemblyQualifiedName} != {typeof(T).AssemblyQualifiedName}");
            }

            return type;
        }

        private static T[] FetchUpToRootBase<T>(this Type type, Func<Type, IEnumerable<T>> fetch)
        {
            var results = new List<T>();
            FetchUpToRootBase(type, results, fetch);
            return results.ToArray();
        }

        private static void FetchUpToRootBase<T>(this Type type, List<T> results, Func<Type, IEnumerable<T>> fetch)
        {
            while (true)
            {
                results.AddRange(fetch.Invoke(type));
                if (type.BaseType is not { } baseType)
                {
                    break;
                }

                type = baseType;
            }
        }

        private static bool IsMatchArguments(this MethodBase method, params Type[] argTypes)
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

        internal enum MemberType
        {
            Field,
            Property,
            Method,
            Constructor,
            FieldOrProperty
        }
    }
}