using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FieldOrProp? GetFieldOrPropertyInfoInternalOrNull([NotNull] this Type? type, string memberName, bool isStatic)
        {
            var info = GetFieldInfoInternalOrNull(type, memberName, isStatic) ??
                       GetPropertyInfoInternalOrNull(type, memberName, isStatic) as MemberInfo;
            if (info is null)
            {
                return null;
            }

            return (FieldOrProp) info;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FieldOrProp GetFieldOrPropertyInfoInternal(this Type type, string memberName, bool isStatic)
        {
            var info = GetFieldOrPropertyInfoInternalOrNull(type, memberName, isStatic);
            return info ?? throw new InvalidOperationException(NotFoundMessage(type, memberName, isStatic, MemberType.FieldOrProperty));
        }

        #region Property Info

        private static FieldOrProp? GetInstanceFieldOrPropertyInfoOrNull<T>(string memberName) => GetInstanceFieldOrPropertyInfoOrNull(typeof(T), memberName);
        private static FieldOrProp GetInstanceFieldOrPropertyInfo<T>(string memberName) => GetInstanceFieldOrPropertyInfo(typeof(T), memberName);

        private static FieldOrProp? GetInstanceFieldOrPropertyInfoOrNull(this Type type, string memberName) => GetFieldOrPropertyInfoInternalOrNull(type, memberName, false);
        private static FieldOrProp GetInstanceFieldOrPropertyInfo(this Type type, string memberName) => GetFieldOrPropertyInfoInternal(type, memberName, false);

        private static FieldOrProp? GetStaticFieldOrPropertyInfoOrNull<T>(string memberName) => GetStaticFieldOrPropertyInfoOrNull(typeof(T), memberName);
        private static FieldOrProp GetStaticFieldOrPropertyInfo<T>(string memberName) => GetStaticFieldOrPropertyInfo(typeof(T), memberName);

        private static FieldOrProp? GetStaticFieldOrPropertyInfoOrNull(this Type type, string memberName) => GetFieldOrPropertyInfoInternalOrNull(type, memberName, true);
        private static FieldOrProp GetStaticFieldOrPropertyInfo(this Type type, string memberName) => GetFieldOrPropertyInfoInternal(type, memberName, true);

        #endregion

        #region Instance Field or Property Value

        public static TResult GetInstanceFieldOrProperty<TResult>([NotNull] this object? instance, string memberName) => (TResult) GetInstanceFieldOrProperty(instance, memberName);

        public static object GetInstanceFieldOrProperty([NotNull] this object? instance, string memberName)
        {
            AssertInstance(instance, out var instanceType, memberName, MemberType.FieldOrProperty);
            var info = GetInstanceFieldOrPropertyInfo(instanceType, memberName);
            return info.GetValue(instance);
        }

        public static void SetInstanceFieldOrProperty(this object? instance, string memberName, object? value)
        {
            AssertInstance(instance, out var instanceType, memberName, MemberType.FieldOrProperty);
            var info = GetInstanceFieldOrPropertyInfo(instanceType, memberName);
            info.SetValue(instance, value);
        }

        #endregion

        #region Static Field or Property Value

        public static TResult GetStaticFieldOrProperty<TTarget, TResult>(string memberName) => GetStaticFieldOrProperty<TResult>(typeof(TTarget), memberName);

        public static TResult GetStaticFieldOrProperty<TResult>(this Type type, string memberName)
        {
            var info = GetStaticFieldOrPropertyInfo(type, memberName);
            return (TResult) info.GetValue(null);
        }

        public static void SetStaticFieldOrProperty<TTarget>(string memberName, object? value) => SetStaticFieldOrProperty(typeof(TTarget), memberName, value);

        public static void SetStaticFieldOrProperty(this Type type, string memberName, object? value)
        {
            var info = GetStaticFieldOrPropertyInfo(type, memberName);
            info.SetValue(null, value);
        }

        #endregion
    }
}