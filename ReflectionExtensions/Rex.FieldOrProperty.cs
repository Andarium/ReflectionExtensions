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
        private static TDelegate InstanceGetter<TTarget, TResult, TDelegate>(this FieldOrProp fieldOrProperty)
            where TDelegate : Delegate
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");

            // cast target to declaring type in case of accessing as 'object'
            var castTargetExp = targetExp.Cast(fieldOrProperty.DeclaringType);

            var memberExp = Expression.MakeMemberAccess(castTargetExp, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TResult>();

            return Expression.Lambda<TDelegate>(castResultExp, targetExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate InstanceSetter<TTarget, TValue, TDelegate>(this FieldOrProp fieldOrProperty) where TDelegate : Delegate
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");
            var valueExp = Expression.Parameter(typeof(TValue), "value");

            // cast target to declaring type in case of accessing as 'object'
            var castTargetExp = targetExp.Cast(fieldOrProperty.DeclaringType);

            // cast value in case of getting parameter as 'object'
            var castValueExp = valueExp.Cast(fieldOrProperty.Type);
            var memberExp = Expression.MakeMemberAccess(castTargetExp, fieldOrProperty);
            var assignExp = Expression.Assign(memberExp, castValueExp);
            return Expression.Lambda<TDelegate>(assignExp, targetExp, valueExp).LogAndCompile();
        }

        public static InstanceGetter<TTarget, TResult> CreateInstanceGetter<TTarget, TResult>(string memberName)
        {
            return typeof(TTarget).GetInstanceFieldOrPropertyInfo(memberName).InstanceGetter<TTarget, TResult, InstanceGetter<TTarget, TResult>>();
        }

        public static InstanceGetter<TResult> CreateInstanceGetter<TResult>(this Type instanceType, string memberName)
        {
            return instanceType.GetInstanceFieldOrPropertyInfo(memberName).InstanceGetter<object, TResult, InstanceGetter<TResult>>();
        }

        public static InstanceGetter CreateInstanceGetter(this Type instanceType, string memberName)
        {
            return instanceType.GetInstanceFieldOrPropertyInfo(memberName).InstanceGetter<object, object, InstanceGetter>();
        }

        public static InstanceSetter<TTarget, TValue> CreateInstanceSetter<TTarget, TValue>(string memberName)
        {
            return typeof(TTarget).GetInstanceFieldOrPropertyInfo(memberName).InstanceSetter<TTarget, TValue, InstanceSetter<TTarget, TValue>>();
        }

        public static InstanceSetter<TValue> CreateInstanceSetter<TValue>(this Type instanceType, string memberName)
        {
            return instanceType.GetInstanceFieldOrPropertyInfo(memberName).InstanceSetter<object, TValue, InstanceSetter<TValue>>();
        }

        public static InstanceSetter CreateInstanceSetter(this Type instanceType, string memberName)
        {
            return instanceType.GetInstanceFieldOrPropertyInfo(memberName).InstanceSetter<object, object, InstanceSetter>();
        }

        ////////////////////////////////////////
        //////////   Const Instance   //////////
        ////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate ConstInstanceGetter<TValue, TDelegate>(this FieldOrProp fieldOrProperty, object constInstance) where TDelegate : Delegate
        {
            AssertInstance(constInstance, fieldOrProperty.Name, MemberType.FieldOrProperty);

            var targetExp = Expression.Constant(constInstance);
            var memberExp = Expression.MakeMemberAccess(targetExp, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TValue>();
            return Expression.Lambda<TDelegate>(castResultExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate ConstInstanceSetter<TValue, TDelegate>(this FieldOrProp fieldOrProperty, object constInstance) where TDelegate : Delegate
        {
            AssertInstance(constInstance, fieldOrProperty.Name, MemberType.FieldOrProperty);

            var targetExp = Expression.Constant(constInstance);
            var valueExp = Expression.Parameter(typeof(TValue), "value");

            // cast value in case of getting parameter as 'object'
            var castValueExp = valueExp.Cast(fieldOrProperty.Type);

            var memberExp = Expression.MakeMemberAccess(targetExp, fieldOrProperty);
            var assignExp = Expression.Assign(memberExp, castValueExp);
            return Expression.Lambda<TDelegate>(assignExp, valueExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate CreateConstInstanceGetter<TValue, TDelegate>(this object constInstance, string memberName) where TDelegate : Delegate
        {
            AssertInstance(constInstance, out var instanceType, memberName, MemberType.FieldOrProperty);
            var info = instanceType.GetInstanceFieldOrPropertyInfo(memberName);
            return ConstInstanceGetter<TValue, TDelegate>(info, constInstance);
        }

        public static ConstGetter CreateConstInstanceGetter(this object constInstance, string memberName)
        {
            return constInstance.CreateConstInstanceGetter<object, ConstGetter>(memberName);
        }

        public static ConstGetter<TValue> CreateConstInstanceGetter<TValue>(this object constInstance, string memberName)
        {
            return constInstance.CreateConstInstanceGetter<TValue, ConstGetter<TValue>>(memberName);
        }

        private static TDelegate CreateConstInstanceSetter<TValue, TDelegate>(this object constInstance, string memberName) where TDelegate : Delegate
        {
            AssertInstance(constInstance, out var instanceType, memberName, MemberType.FieldOrProperty);
            var info = instanceType.GetInstanceFieldOrPropertyInfo(memberName);
            return ConstInstanceSetter<TValue, TDelegate>(info, constInstance);
        }

        public static ConstSetter CreateConstInstanceSetter(this object constInstance, string memberName)
        {
            return CreateConstInstanceSetter<object, ConstSetter>(constInstance, memberName);
        }

        public static ConstSetter<TValue> CreateConstInstanceSetter<TValue>(this object constInstance, string memberName)
        {
            return CreateConstInstanceSetter<TValue, ConstSetter<TValue>>(constInstance, memberName);
        }

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate StaticGetter<TResult, TDelegate>(this FieldOrProp fieldOrProperty) where TDelegate : Delegate
        {
            var memberExp = Expression.MakeMemberAccess(null, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TResult>();
            return Expression.Lambda<TDelegate>(castResultExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate StaticSetter<TValue, TDelegate>(this FieldOrProp fieldOrProperty) where TDelegate : Delegate
        {
            var valueExp = Expression.Parameter(typeof(TValue), "value");

            // cast value in case of getting parameter as 'object'
            var castValueExp = valueExp.Cast(fieldOrProperty.Type);

            var memberExp = Expression.MakeMemberAccess(null, fieldOrProperty);
            var assignExp = Expression.Assign(memberExp, castValueExp);
            return Expression.Lambda<TDelegate>(assignExp, valueExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstGetter<TResult> CreateStaticGetter<TTarget, TResult>(string memberName)
        {
            return typeof(TTarget).GetStaticFieldOrPropertyInfo(memberName).StaticGetter<TResult, ConstGetter<TResult>>();
        }

        public static ConstGetter CreateStaticGetter<TTarget>(string memberName)
        {
            return typeof(TTarget).GetStaticFieldOrPropertyInfo(memberName).StaticGetter<object, ConstGetter>();
        }

        public static ConstGetter<TResult> CreateStaticGetter<TResult>(this Type targetType, string memberName)
        {
            return targetType.GetStaticFieldOrPropertyInfo(memberName).StaticGetter<TResult, ConstGetter<TResult>>();
        }

        public static ConstGetter CreateStaticGetter(this Type targetType, string memberName)
        {
            return targetType.GetStaticFieldOrPropertyInfo(memberName).StaticGetter<object, ConstGetter>();
        }

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
                    return _info switch
                    {
                        FieldInfo f => f.FieldType,
                        PropertyInfo p => p.PropertyType,
                        _ => throw new InvalidOperationException()
                    };
                }
            }

            public string Name => _info.Name;

            public Type DeclaringType => _info.DeclaringType!;

            public object GetValue(object? instance)
            {
                return _info switch
                {
                    FieldInfo f => f.GetValue(instance),
                    PropertyInfo p => p.GetValue(instance),
                    _ => throw new InvalidOperationException()
                };
            }

            public void SetValue(object? instance, object? value)
            {
                switch (_info)
                {
                    case FieldInfo f:
                        f.SetValue(instance, value);
                        return;
                    case PropertyInfo p:
                        p.SetValue(instance, value);
                        return;
                    default:
                        throw new InvalidOperationException();
                }
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