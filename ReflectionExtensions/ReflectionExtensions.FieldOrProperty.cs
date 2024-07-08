using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemberInfo? GetFieldOrPropertyInfoInternalOrNull([NotNull] this Type? type, string propName, bool isStatic)
        {
            return GetFieldInfoInternalOrNull(type, propName, isStatic) ??
                   GetPropertyInfoInternalOrNull(type, propName, isStatic) as MemberInfo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemberInfo GetFieldOrPropertyInfoInternal(this Type type, string propName, bool isStatic)
        {
            var info = GetFieldOrPropertyInfoInternalOrNull(type, propName, isStatic);
            return info ?? throw new InvalidOperationException(NotFoundMessage(type, propName, isStatic, MemberType.FieldOrProperty));
        }

        #region Property Info

        public static MemberInfo? GetInstanceFieldOrPropertyInfoOrNull<T>(string propName) => GetInstanceFieldOrPropertyInfoOrNull(typeof(T), propName);
        public static MemberInfo GetInstanceFieldOrPropertyInfo<T>(string propName) => GetInstanceFieldOrPropertyInfo(typeof(T), propName);

        public static MemberInfo? GetInstanceFieldOrPropertyInfoOrNull(this Type type, string propName) => GetFieldOrPropertyInfoInternalOrNull(type, propName, false);
        public static MemberInfo GetInstanceFieldOrPropertyInfo(this Type type, string propName) => GetFieldOrPropertyInfoInternal(type, propName, false);

        public static MemberInfo? GetStaticFieldOrPropertyInfoOrNull<T>(string propName) => GetStaticFieldOrPropertyInfoOrNull(typeof(T), propName);
        public static MemberInfo GetStaticFieldOrPropertyInfo<T>(string propName) => GetStaticFieldOrPropertyInfo(typeof(T), propName);

        public static MemberInfo? GetStaticFieldOrPropertyInfoOrNull(this Type type, string propName) => GetFieldOrPropertyInfoInternalOrNull(type, propName, true);
        public static MemberInfo GetStaticFieldOrPropertyInfo(this Type type, string propName) => GetFieldOrPropertyInfoInternal(type, propName, true);

        #endregion

        #region Instance Field or Property Value

        public static TResult GetInstanceFieldOrProperty<TTarget, TResult>(this TTarget instance, string propName) => GetInstanceFieldOrProperty<TResult>(instance, propName, typeof(TTarget));

        public static TResult GetInstanceFieldOrProperty<TResult>([NotNull] this object? instance, string propName, Type? instanceType = null)
        {
            AssertInstanceAndType(instance, ref instanceType, propName, MemberType.FieldOrProperty);
            var info = GetInstanceFieldOrPropertyInfo(instanceType, propName);
            return info switch
            {
                FieldInfo field => (TResult) field.GetValue(instance),
                PropertyInfo prop => (TResult) prop.GetValue(instance),
                _ => throw new InvalidOperationException()
            };
        }

        public static void SetInstanceFieldOrProperty<T, TR>(this T? instance, string propName, TR? value)
        {
            var instanceType = typeof(T);
            AssertInstanceAndType(instance, ref instanceType, propName, MemberType.FieldOrProperty);
            var info = GetInstanceFieldOrPropertyInfo(instanceType, propName);
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

        public static TResult GetStaticFieldOrProperty<TTarget, TResult>(string propName) => GetStaticFieldOrProperty<TResult>(typeof(TTarget), propName);

        public static TResult GetStaticFieldOrProperty<TResult>(this Type type, string propName)
        {
            var info = GetStaticFieldOrPropertyInfo(type, propName);
            return info switch
            {
                FieldInfo field => (TResult) field.GetValue(null),
                PropertyInfo prop => (TResult) prop.GetValue(null),
                _ => throw new InvalidOperationException()
            };
        }

        public static void SetStaticFieldOrProperty<TTarget>(string propName, object? value) => SetStaticFieldOrProperty(typeof(TTarget), propName, value);

        public static void SetStaticFieldOrProperty(this Type type, string propName, object? value)
        {
            var info = GetStaticFieldOrPropertyInfo(type, propName);
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