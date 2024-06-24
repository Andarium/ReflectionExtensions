// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantNullableDirective

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertyMap = new();
        private static readonly Map<Type, string, PropertyInfo> PropertyNameMap = new();

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

        private static PropertyInfo GetPropertyInfoInternal(this Type type, string propName, bool isStatic)
        {
            if (PropertyNameMap.TryGetValue(type, propName, out var prop))
            {
                return prop;
            }

            prop = GetAllProperties(type).FirstOrDefault(x => x.Name == propName && x.IsStatic() == isStatic);
            PropertyNameMap[type, propName] = prop ?? throw new InvalidOperationException(NotFoundMessage(type, propName, isStatic, MemberType.Property));
            return prop;
        }

        private static bool IsStatic(this PropertyInfo p)
        {
            return p.GetMethod?.IsStatic ?? p.SetMethod?.IsStatic ?? true;
        }

        #region Property Info

        public static PropertyInfo GetInstancePropertyInfo<T>(string propName) => GetInstancePropertyInfo(typeof(T), propName);

        public static PropertyInfo GetInstancePropertyInfo(this Type type, string propName)
        {
            AssertType(type);
            return GetPropertyInfoInternal(type, propName, false);
        }

        public static PropertyInfo GetStaticPropertyInfo<T>(string propName) => GetStaticPropertyInfo(typeof(T), propName);

        public static PropertyInfo GetStaticPropertyInfo(this Type type, string propName)
        {
            AssertType(type);
            return GetPropertyInfoInternal(type, propName, true);
        }

        #endregion

        #region Instance Property Value

        public static TR GetInstanceProperty<T, TR>(this T instance, string propName) => GetInstanceProperty<TR>(instance, propName, typeof(T));

        public static TR GetInstanceProperty<TR>(this object? instance, string propName, Type? instanceType = null)
        {
            AssertInstance(instance, instanceType, propName, MemberType.Property);
            instanceType ??= instance!.GetType();
            var prop = GetInstancePropertyInfo(instanceType, propName);
            return (TR) prop.GetValue(instance);
        }

        public static void SetInstanceProperty<T, TR>(this T? instance, string propName, TR? value)
        {
            AssertInstance<T>(instance, propName, MemberType.Property);
            var prop = GetInstancePropertyInfo(typeof(T), propName);
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