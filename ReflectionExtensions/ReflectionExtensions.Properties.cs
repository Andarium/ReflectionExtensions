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

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertyMap = new();
        private static readonly Map<Type, string, PropertyInfo?> PropertyNameMap = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReadOnlyList<PropertyInfo> GetAllProperties(Type type)
        {
            if (PropertyMap.TryGetValue(type, out var value))
            {
                return value;
            }

            value = type.FetchUpToRootBase(t => t.GetProperties(FetchAllDeclared));
            PropertyMap.Add(type, value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static PropertyInfo? GetPropertyInfoInternalOrNull([NotNull] this Type? type, string propName, bool isStatic)
        {
            type.AssertType();
            if (PropertyNameMap.TryGetValue(type, propName, out var prop))
            {
                return prop;
            }

            prop = GetAllProperties(type).FirstOrDefault(x => x.Name == propName && x.IsStatic() == isStatic);
            PropertyNameMap[type, propName] = prop;
            return prop;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static PropertyInfo GetPropertyInfoInternal([NotNull] this Type? type, string propName, bool isStatic)
        {
            var prop = GetPropertyInfoInternalOrNull(type, propName, isStatic);
            return prop ?? throw new InvalidOperationException(NotFoundMessage(type, propName, isStatic, MemberType.Property));
        }

        #region Property Info

        public static PropertyInfo? GetInstancePropertyInfoOrNull<T>(string propName) => GetInstancePropertyInfoOrNull(typeof(T), propName);
        public static PropertyInfo GetInstancePropertyInfo<T>(string propName) => GetInstancePropertyInfo(typeof(T), propName);

        public static PropertyInfo? GetInstancePropertyInfoOrNull(this Type type, string propName) => GetPropertyInfoInternalOrNull(type, propName, false);
        public static PropertyInfo GetInstancePropertyInfo(this Type type, string propName) => GetPropertyInfoInternal(type, propName, false);

        public static PropertyInfo? GetStaticPropertyInfoOrNull<T>(string propName) => GetStaticPropertyInfoOrNull(typeof(T), propName);
        public static PropertyInfo GetStaticPropertyInfo<T>(string propName) => GetStaticPropertyInfo(typeof(T), propName);

        public static PropertyInfo? GetStaticPropertyInfoOrNull(this Type type, string propName) => GetPropertyInfoInternalOrNull(type, propName, true);
        public static PropertyInfo GetStaticPropertyInfo(this Type type, string propName) => GetPropertyInfoInternal(type, propName, true);

        #endregion

        #region Instance Property Value

        public static TResult GetInstanceProperty<TResult>([NotNull] this object? instance, string propName)
        {
            AssertInstance(instance, out var instanceType, propName, MemberType.Property);
            return (TResult) GetInstancePropertyInfo(instanceType, propName).GetValue(instance);
        }

        public static void SetInstanceProperty([NotNull] this object? instance, string propName, object? value)
        {
            AssertInstance(instance, out var instanceType, propName, MemberType.Property);
            GetInstancePropertyInfo(instanceType, propName).SetValue(instance, value);
        }

        #endregion

        #region Static Property Value

        public static TResult GetStaticProperty<TTarget, TResult>(string propName) => GetStaticProperty<TResult>(typeof(TTarget), propName);

        public static TResult GetStaticProperty<TResult>(this Type type, string propName) => (TResult) GetStaticProperty(type, propName);

        public static object GetStaticProperty(this Type type, string propName) => GetStaticPropertyInfo(type, propName).GetValue(null);

        public static void SetStaticProperty<TTarget>(string propName, object? value) => SetStaticProperty(typeof(TTarget), propName, value);

        public static void SetStaticProperty(this Type type, string propName, object? value)
        {
            GetStaticPropertyInfo(type, propName).SetValue(null, value);
        }

        #endregion
    }
}