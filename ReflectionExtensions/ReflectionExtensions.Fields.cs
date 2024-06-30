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
        private static readonly Dictionary<Type, FieldInfo[]> FieldMap = new();
        private static readonly Map<Type, string, FieldInfo?> FieldNameMap = new();

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

        private static FieldInfo? GetFieldInfoInternalOrNull([NotNull] this Type? type, string fieldName, bool isStatic)
        {
            type.AssertType();
            if (FieldNameMap.TryGetValue(type, fieldName, out var field))
            {
                return field;
            }

            field = GetAllFields(type).FirstOrDefault(x => x.Name == fieldName && x.IsStatic == isStatic);
            FieldNameMap[type, fieldName] = field;
            return field;
        }

        private static FieldInfo GetFieldInfoInternal([NotNull] this Type? type, string fieldName, bool isStatic)
        {
            var field = GetFieldInfoInternalOrNull(type, fieldName, isStatic);
            return field ?? throw new InvalidOperationException(NotFoundMessage(type, fieldName, isStatic, MemberType.Field));
        }

        #region Field Info

        public static FieldInfo? GetInstanceFieldInfoOrNull<T>(string fieldName) => GetInstanceFieldInfoOrNull(typeof(T), fieldName);
        public static FieldInfo GetInstanceFieldInfo<T>(string fieldName) => GetInstanceFieldInfo(typeof(T), fieldName);

        public static FieldInfo? GetInstanceFieldInfoOrNull(this Type type, string fieldName) => GetFieldInfoInternalOrNull(type, fieldName, false);
        public static FieldInfo GetInstanceFieldInfo(this Type type, string fieldName) => GetFieldInfoInternal(type, fieldName, false);

        public static FieldInfo? GetStaticFieldInfoOrNull<T>(string fieldName) => GetStaticFieldInfoOrNull(typeof(T), fieldName);
        public static FieldInfo GetStaticFieldInfo<T>(string fieldName) => GetStaticFieldInfo(typeof(T), fieldName);

        public static FieldInfo? GetStaticFieldInfoOrNull(this Type type, string fieldName) => GetFieldInfoInternalOrNull(type, fieldName, true);
        public static FieldInfo GetStaticFieldInfo(this Type type, string fieldName) => GetFieldInfoInternal(type, fieldName, true);

        #endregion

        #region Instance Field Value

        public static TR GetInstanceField<T, TR>(this T instance, string fieldName) => GetInstanceField<TR>(instance, fieldName, typeof(T));

        public static TR GetInstanceField<TR>(this object? instance, string fieldName, Type? instanceType = null)
        {
            AssertInstance(instance, ref instanceType, fieldName, MemberType.Field);
            var field = GetInstanceFieldInfo(instanceType, fieldName);
            return (TR) field.GetValue(instance);
        }

        public static void SetInstanceField<T, TR>(this T? instance, string fieldName, TR? value)
        {
            var instanceType = typeof(T);
            AssertInstance(instance, ref instanceType, fieldName, MemberType.Field);
            var field = GetInstanceFieldInfo(instanceType, fieldName);
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