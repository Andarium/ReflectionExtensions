using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        //////////////////////////////////
        //////////   Instance   //////////
        //////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<TTarget, TResult> InstanceGetter<TTarget, TResult>(this FieldOrProp fieldOrProperty)
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");

            // cast target to declaring type in case of accessing as 'object'
            var castTargetExp = targetExp.Cast(fieldOrProperty.DeclaringType);

            var memberExp = Expression.MakeMemberAccess(castTargetExp, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TResult>();

            return Expression.Lambda<Func<TTarget, TResult>>(castResultExp, targetExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Action<TTarget, TValue> InstanceSetter<TTarget, TValue>(this FieldOrProp fieldOrProperty)
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");
            var valueExp = Expression.Parameter(typeof(TValue), "value");

            // cast target to declaring type in case of accessing as 'object'
            var castTargetExp = targetExp.Cast(fieldOrProperty.DeclaringType);

            // cast value in case of getting parameter as 'object'
            var castValueExp = valueExp.Cast(fieldOrProperty.Type);

            var memberExp = Expression.MakeMemberAccess(castTargetExp, fieldOrProperty);
            var assignExp = Expression.Assign(memberExp, castValueExp);
            return Expression.Lambda<Action<TTarget, TValue>>(assignExp, targetExp, valueExp).LogAndCompile();
        }

        public static Func<TTarget, TResult> CreateInstanceGetter<TTarget, TResult>(string memberName)
        {
            return typeof(TTarget).GetInstanceFieldOrPropertyInfo(memberName).InstanceGetter<TTarget, TResult>();
        }

        public static Func<object, TResult> CreateInstanceGetter<TResult>(this Type instanceType, string memberName)
        {
            return InstanceGetter<object, TResult>(instanceType.GetInstanceFieldOrPropertyInfo(memberName));
        }

        public static Func<object, object> CreateInstanceGetter(this Type instanceType, string memberName)
        {
            return InstanceGetter<object, object>(instanceType.GetInstanceFieldOrPropertyInfo(memberName));
        }

        public static Action<TTarget, TValue> CreateInstanceSetter<TTarget, TValue>(string memberName)
        {
            return InstanceSetter<TTarget, TValue>(typeof(TTarget).GetInstanceFieldOrPropertyInfo(memberName));
        }

        public static Action<object, TValue> CreateInstanceSetter<TValue>(this Type instanceType, string memberName)
        {
            return InstanceSetter<object, TValue>(instanceType.GetInstanceFieldOrPropertyInfo(memberName));
        }

        public static Action<object, object> CreateInstanceSetter(this Type instanceType, string memberName)
        {
            return InstanceSetter<object, object>(instanceType.GetInstanceFieldOrPropertyInfo(memberName));
        }

        ////////////////////////////////////////
        //////////   Const Instance   //////////
        ////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<TValue> ConstInstanceGetter<TValue>(this FieldOrProp fieldOrProperty, object constInstance)
        {
            AssertInstance(constInstance, fieldOrProperty.Name, MemberType.FieldOrProperty);

            var targetExp = Expression.Constant(constInstance);
            var memberExp = Expression.MakeMemberAccess(targetExp, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TValue>();
            return Expression.Lambda<Func<TValue>>(castResultExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Action<TValue> ConstInstanceSetter<TValue>(this FieldOrProp fieldOrProperty, object constInstance)
        {
            AssertInstance(constInstance, fieldOrProperty.Name, MemberType.FieldOrProperty);

            var targetExp = Expression.Constant(constInstance);
            var valueExp = Expression.Parameter(typeof(TValue), "value");

            // cast value in case of getting parameter as 'object'
            var castValueExp = valueExp.Cast(fieldOrProperty.Type);

            var memberExp = Expression.MakeMemberAccess(targetExp, fieldOrProperty);
            var assignExp = Expression.Assign(memberExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).LogAndCompile();
        }

        public static Func<object> CreateConstInstanceGetter(this object constInstance, string memberName) => CreateConstInstanceGetter<object>(constInstance, memberName);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateConstInstanceGetter<TValue>(this object constInstance, string memberName)
        {
            AssertInstance(constInstance, out var instanceType, memberName, MemberType.FieldOrProperty);
            var info = instanceType.GetInstanceFieldOrPropertyInfo(memberName);
            return ConstInstanceGetter<TValue>(info, constInstance);
        }

        public static Action<object> CreateConstInstanceSetter(this object constInstance, string memberName) => CreateConstInstanceSetter<object>(constInstance, memberName);

        public static Action<TValue> CreateConstInstanceSetter<TValue>(this object constInstance, string memberName)
        {
            AssertInstance(constInstance, out var instanceType, memberName, MemberType.FieldOrProperty);
            var info = instanceType.GetInstanceFieldOrPropertyInfo(memberName);
            return ConstInstanceSetter<TValue>(info, constInstance);
        }

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<TResult> StaticGetter<TResult>(this FieldOrProp fieldOrProperty)
        {
            var memberExp = Expression.MakeMemberAccess(null, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TResult>();
            return Expression.Lambda<Func<TResult>>(castResultExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Action<TValue> StaticSetter<TValue>(this FieldOrProp fieldOrProperty)
        {
            var valueExp = Expression.Parameter(typeof(TValue), "value");

            // cast value in case of getting parameter as 'object'
            var castValueExp = valueExp.Cast(fieldOrProperty.Type);

            var memberExp = Expression.MakeMemberAccess(null, fieldOrProperty);
            var assignExp = Expression.Assign(memberExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TResult> CreateStaticGetter<TTarget, TResult>(string memberName)
        {
            return typeof(TTarget).GetStaticFieldOrPropertyInfo(memberName).StaticGetter<TResult>();
        }

        public static Func<object> CreateStaticGetter<TTarget>(string memberName) => CreateStaticGetter<TTarget, object>(memberName);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TResult> CreateStaticGetter<TResult>(this Type targetType, string memberName)
        {
            return targetType.GetStaticFieldOrPropertyInfo(memberName).StaticGetter<TResult>();
        }

        public static Func<object> CreateStaticGetter(this Type targetType, string memberName) => CreateStaticGetter<object>(targetType, memberName);

        private readonly struct FieldOrProp
        {
            private readonly MemberInfo _info;

            private FieldOrProp(MemberInfo info)
            {
                if (info is not FieldInfo and not PropertyInfo)
                {
                    throw new ArgumentException($"{nameof(FieldInfo)} or {nameof(PropertyInfo)} expected but got {info.GetType().Name} instead");
                }

                _info = info;
            }

            public Type Type
            {
                get
                {
                    if (_info is FieldInfo f)
                    {
                        return f.FieldType;
                    }

                    if (_info is PropertyInfo p)
                    {
                        return p.PropertyType;
                    }

                    throw new InvalidOperationException();
                }
            }

            public string Name => _info.Name;

            public Type DeclaringType => _info.DeclaringType!;

            public object GetValue(object? instance)
            {
                if (_info is FieldInfo f)
                {
                    return f.GetValue(instance);
                }

                if (_info is PropertyInfo p)
                {
                    return p.GetValue(instance);
                }

                throw new InvalidOperationException();
            }

            public void SetValue(object? instance, object? value)
            {
                if (_info is FieldInfo f)
                {
                    f.SetValue(instance, value);
                    return;
                }

                if (_info is PropertyInfo p)
                {
                    p.SetValue(instance, value);
                    return;
                }

                throw new InvalidOperationException();
            }

            public static implicit operator FieldOrProp(FieldInfo field) => new(field);
            public static implicit operator FieldOrProp(PropertyInfo prop) => new(prop);
            public static explicit operator FieldOrProp(MemberInfo prop) => new(prop);

            public static implicit operator FieldInfo(FieldOrProp f) => (FieldInfo) f._info;
            public static implicit operator PropertyInfo(FieldOrProp f) => (PropertyInfo) f._info;
            public static implicit operator MemberInfo(FieldOrProp f) => f._info;
        }
    }
}