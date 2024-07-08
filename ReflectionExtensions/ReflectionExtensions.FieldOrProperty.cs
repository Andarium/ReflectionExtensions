using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemberInfo? GetFieldOrPropertyInfoInternalOrNull([NotNull] this Type? type, string memberName, bool isStatic)
        {
            return GetFieldInfoInternalOrNull(type, memberName, isStatic) ??
                   GetPropertyInfoInternalOrNull(type, memberName, isStatic) as MemberInfo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemberInfo GetFieldOrPropertyInfoInternal(this Type type, string memberName, bool isStatic)
        {
            var info = GetFieldOrPropertyInfoInternalOrNull(type, memberName, isStatic);
            return info ?? throw new InvalidOperationException(NotFoundMessage(type, memberName, isStatic, MemberType.FieldOrProperty));
        }

        #region Property Info

        public static MemberInfo? GetInstanceFieldOrPropertyInfoOrNull<T>(string memberName) => GetInstanceFieldOrPropertyInfoOrNull(typeof(T), memberName);
        public static MemberInfo GetInstanceFieldOrPropertyInfo<T>(string memberName) => GetInstanceFieldOrPropertyInfo(typeof(T), memberName);

        public static MemberInfo? GetInstanceFieldOrPropertyInfoOrNull(this Type type, string memberName) => GetFieldOrPropertyInfoInternalOrNull(type, memberName, false);
        public static MemberInfo GetInstanceFieldOrPropertyInfo(this Type type, string memberName) => GetFieldOrPropertyInfoInternal(type, memberName, false);

        public static MemberInfo? GetStaticFieldOrPropertyInfoOrNull<T>(string memberName) => GetStaticFieldOrPropertyInfoOrNull(typeof(T), memberName);
        public static MemberInfo GetStaticFieldOrPropertyInfo<T>(string memberName) => GetStaticFieldOrPropertyInfo(typeof(T), memberName);

        public static MemberInfo? GetStaticFieldOrPropertyInfoOrNull(this Type type, string memberName) => GetFieldOrPropertyInfoInternalOrNull(type, memberName, true);
        public static MemberInfo GetStaticFieldOrPropertyInfo(this Type type, string memberName) => GetFieldOrPropertyInfoInternal(type, memberName, true);

        #endregion

        #region Instance Field or Property Value

        public static TResult GetInstanceFieldOrProperty<TResult>([NotNull] this object? instance, string memberName) => (TResult) GetInstanceFieldOrProperty(instance, memberName);

        public static object GetInstanceFieldOrProperty([NotNull] this object? instance, string memberName)
        {
            AssertInstance(instance, out var instanceType, memberName, MemberType.FieldOrProperty);
            var info = GetInstanceFieldOrPropertyInfo(instanceType, memberName);
            return info switch
            {
                FieldInfo field => field.GetValue(instance),
                PropertyInfo prop => prop.GetValue(instance),
                _ => throw new InvalidOperationException()
            };
        }

        public static void SetInstanceFieldOrProperty(this object? instance, string memberName, object? value)
        {
            AssertInstance(instance, out var instanceType, memberName, MemberType.FieldOrProperty);
            var info = GetInstanceFieldOrPropertyInfo(instanceType, memberName);
            switch (info)
            {
                case FieldInfo field:
                    field.SetValue(instance, value);
                    break;
                case PropertyInfo prop:
                    prop.SetValue(instance, value);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        #endregion

        #region Static Field or Property Value

        public static TResult GetStaticFieldOrProperty<TTarget, TResult>(string memberName) => GetStaticFieldOrProperty<TResult>(typeof(TTarget), memberName);

        public static TResult GetStaticFieldOrProperty<TResult>(this Type type, string memberName)
        {
            var info = GetStaticFieldOrPropertyInfo(type, memberName);
            return info switch
            {
                FieldInfo field => (TResult) field.GetValue(null),
                PropertyInfo prop => (TResult) prop.GetValue(null),
                _ => throw new InvalidOperationException()
            };
        }

        public static void SetStaticFieldOrProperty<TTarget>(string memberName, object? value) => SetStaticFieldOrProperty(typeof(TTarget), memberName, value);

        public static void SetStaticFieldOrProperty(this Type type, string memberName, object? value)
        {
            var info = GetStaticFieldOrPropertyInfo(type, memberName);
            switch (info)
            {
                case FieldInfo field:
                    field.SetValue(null, value);
                    break;
                case PropertyInfo prop:
                    prop.SetValue(null, value);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        #endregion
    }
}