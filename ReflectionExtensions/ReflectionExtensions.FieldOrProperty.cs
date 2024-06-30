using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static MemberInfo? GetFieldOrPropertyInfoInternalOrNull([NotNull] this Type? type, string propName, bool isStatic)
        {
            type.AssertType();
            return GetFieldInfoInternalOrNull(type, propName, isStatic) ??
                   GetPropertyInfoInternalOrNull(type, propName, isStatic) as MemberInfo;
        }

        private static MemberInfo GetFieldOrPropertyInfoInternal(this Type type, string propName, bool isStatic)
        {
            var info = GetFieldInfoInternalOrNull(type, propName, isStatic);
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

        public static TR GetInstanceFieldOrProperty<T, TR>(this T instance, string propName) => GetInstanceFieldOrProperty<TR>(instance, propName, typeof(T));

        public static TR GetInstanceFieldOrProperty<TR>([NotNull] this object? instance, string propName, Type? instanceType = null)
        {
            AssertInstance(instance, ref instanceType, propName, MemberType.FieldOrProperty);
            var info = GetInstanceFieldOrPropertyInfo(instanceType, propName);
            return info switch
            {
                FieldInfo field => (TR) field.GetValue(instance),
                PropertyInfo prop => (TR) prop.GetValue(instance),
                _ => throw new InvalidOperationException()
            };
        }

        public static void SetInstanceFieldOrProperty<T, TR>(this T? instance, string propName, TR? value)
        {
            var instanceType = typeof(T);
            AssertInstance(instance, ref instanceType, propName, MemberType.FieldOrProperty);
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

        public static TR GetStaticFieldOrProperty<T, TR>(string propName) => GetStaticFieldOrProperty<TR>(typeof(T), propName);

        public static TR GetStaticFieldOrProperty<TR>(this Type type, string propName)
        {
            var info = GetStaticFieldOrPropertyInfo(type, propName);
            return info switch
            {
                FieldInfo field => (TR) field.GetValue(null),
                PropertyInfo prop => (TR) prop.GetValue(null),
                _ => throw new InvalidOperationException()
            };
        }

        public static void SetStaticFieldOrProperty<T>(string propName, T? value) => SetStaticFieldOrProperty(typeof(T), propName, value);

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