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
        private static readonly Dictionary<Type, FieldInfo[]> FieldMap = new();
        private static readonly Map<Type, string, FieldInfo?> FieldNameMap = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReadOnlyList<FieldInfo> GetAllFields(Type type)
        {
            if (FieldMap.TryGetValue(type, out var value))
            {
                return value;
            }

            value = FetchUpToRootBase(type, t => t.GetFields(FetchAllDeclared));
            FieldMap.Add(type, value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public static TResult GetInstanceField<TResult>(this object? instance, string fieldName)
        {
            AssertInstance(instance, out var instanceType, fieldName, MemberType.Field);
            return (TResult) GetInstanceFieldInfo(instanceType, fieldName).GetValue(instance);
        }

        public static void SetInstanceField(this object? instance, string fieldName, object? value)
        {
            AssertInstance(instance, out var instanceType, fieldName, MemberType.Field);
            GetInstanceFieldInfo(instanceType, fieldName).SetValue(instance, value);
        }

        #endregion

        #region Static Field Value

        public static TResult GetStaticField<TTarget, TResult>(string fieldName) => GetStaticField<TResult>(typeof(TTarget), fieldName);

        public static TResult GetStaticField<TResult>(this Type type, string fieldName) => (TResult) GetStaticField(type, fieldName);

        public static object GetStaticField(this Type type, string fieldName) => GetStaticFieldInfo(type, fieldName).GetValue(null);

        public static void SetStaticField<TTarget>(string fieldName, object? value) => SetStaticField(typeof(TTarget), fieldName, value);

        public static void SetStaticField(this Type type, string fieldName, object? value)
        {
            GetStaticFieldInfo(type, fieldName).SetValue(null, value);
        }

        #endregion
    }
}