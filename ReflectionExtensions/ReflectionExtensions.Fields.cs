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
        private static readonly Dictionary<Type, FieldInfo[]> FieldMap = new();
        private static readonly Map<Type, string, FieldInfo> FieldNameMap = new();

        private static IReadOnlyList<FieldInfo> GetAllFields(this Type type)
        {
            if (FieldMap.TryGetValue(type, out var value))
            {
                return value;
            }

            value = FetchUpToRootBase(type, t => t.GetFields(FetchAllDeclared));
            FieldMap.Add(type, value);
            return value;
        }

        private static FieldInfo GetFieldInfoInternal(this Type type, string fieldName, bool isStatic)
        {
            if (FieldNameMap.TryGetValue(type, fieldName, out var field))
            {
                return field;
            }

            field = GetAllFields(type).FirstOrDefault(x => x.Name == fieldName && x.IsStatic == isStatic);
            FieldNameMap[type, fieldName] = field ?? throw new InvalidOperationException(NotFoundMessage(type, fieldName, isStatic, MemberType.Field));
            return field;
        }

        #region Field Info

        public static FieldInfo GetInstanceFieldInfo<T>(string fieldName) => GetInstanceFieldInfo(typeof(T), fieldName);

        public static FieldInfo GetInstanceFieldInfo(this Type type, string fieldName)
        {
            AssertType(type);
            return GetFieldInfoInternal(type, fieldName, false);
        }

        public static FieldInfo GetStaticFieldInfo<T>(string fieldName) => GetStaticFieldInfo(typeof(T), fieldName);

        public static FieldInfo GetStaticFieldInfo(this Type type, string fieldName)
        {
            AssertType(type);
            return GetFieldInfoInternal(type, fieldName, true);
        }

        #endregion

        #region Instance Field Value

        public static TR GetInstanceField<T, TR>(this T instance, string fieldName) => GetInstanceFieldInternal<TR>(instance, fieldName, typeof(T));

        public static TR GetInstanceField<TR>(this object instance, string fieldName) => GetInstanceFieldInternal<TR>(instance, fieldName);

        private static TR GetInstanceFieldInternal<TR>(this object? instance, string fieldName, Type? type = null)
        {
            AssertInstance(instance, type, fieldName, MemberType.Field);
            type ??= instance!.GetType();
            var field = GetInstanceFieldInfo(type, fieldName);
            return (TR) field.GetValue(instance);
        }

        public static void SetInstanceField<T, TR>(this T? instance, string fieldName, TR? value)
        {
            AssertInstance<T>(instance, fieldName, MemberType.Field);
            var field = GetInstanceFieldInfo(typeof(T), fieldName);
            field.SetValue(instance, value);
        }

        #endregion

        #region Static Field Value

        public static TR GetStaticField<T, TR>(string fieldName) => GetStaticField<TR>(typeof(T), fieldName);

        public static TR GetStaticField<TR>(this Type type, string fieldName)
        {
            var field = GetStaticFieldInfo(type, fieldName);
            return (TR) field.GetValue(null);
        }

        public static void SetStaticField<T>(string fieldName, T? value) => SetStaticField(typeof(T), fieldName, value);

        public static void SetStaticField(this Type type, string fieldName, object? value)
        {
            var field = GetStaticFieldInfo(type, fieldName);
            field.SetValue(null, value);
        }

        #endregion
    }
}