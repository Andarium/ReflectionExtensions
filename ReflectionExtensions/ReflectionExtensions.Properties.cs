// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantNullableDirective

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertyMap = new();
        private static readonly Map<Type, string, PropertyInfo?> PropertyNameMap = new();

        private static IReadOnlyList<PropertyInfo> GetAllProperties(this Type type)
        {
            if (PropertyMap.TryGetValue(type, out var value))
            {
                return value;
            }

            value = type.FetchUpToRootBase(t => t.GetProperties(FetchAllDeclared));
            PropertyMap.Add(type, value);
            return value;
        }

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

        private static PropertyInfo GetPropertyInfoInternal([NotNull] this Type? type, string propName, bool isStatic)
        {
            var prop = GetPropertyInfoInternalOrNull(type, propName, isStatic);
            return prop ?? throw new InvalidOperationException(NotFoundMessage(type, propName, isStatic, MemberType.Property));
        }

        private static bool IsStatic(this PropertyInfo p)
        {
            return p.GetMethod?.IsStatic ?? p.SetMethod?.IsStatic ?? true;
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

        public static TR GetInstanceProperty<T, TR>(this T instance, string propName) => GetInstanceProperty<TR>(instance, propName, typeof(T));

        public static TR GetInstanceProperty<TR>([NotNull] this object? instance, string propName, Type? instanceType = null)
        {
            AssertInstanceAndType(instance, ref instanceType, propName, MemberType.Property);
            var prop = GetInstancePropertyInfo(instanceType, propName);
            return (TR) prop.GetValue(instance);
        }

        public static void SetInstanceProperty<T, TR>([NotNull] this T? instance, string propName, TR? value)
        {
            var instanceType = typeof(T);
            AssertInstanceAndType(instance, ref instanceType, propName, MemberType.Property);
            var prop = GetInstancePropertyInfo(instanceType, propName);
            prop.SetValue(instance, value);
        }

        #endregion

        #region Static Property Value

        public static TR GetStaticProperty<T, TR>(string propName) => GetStaticProperty<TR>(typeof(T), propName);

        public static TR GetStaticProperty<TR>(this Type type, string propName)
        {
            var prop = GetStaticPropertyInfo(type, propName);
            return (TR) prop.GetValue(null);
        }

        public static void SetStaticProperty<T>(string propName, T? value) => SetStaticProperty(typeof(T), propName, value);

        public static void SetStaticProperty(this Type type, string propName, object? value)
        {
            var prop = GetStaticPropertyInfo(type, propName);
            prop.SetValue(null, value);
        }

        #endregion
    }
}