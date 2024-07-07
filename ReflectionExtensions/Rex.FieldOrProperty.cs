using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        //////////////////////////////////
        //////////   Instance   //////////
        //////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<TTarget, TValue> InstanceGetter<TTarget, TValue>(FieldOrProp fieldOrProperty)
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");

            // cast target to declaring type in case of accessing as 'object'
            var castTargetExp = targetExp.Cast(fieldOrProperty.DeclaringType);

            var memberExp = Expression.MakeMemberAccess(castTargetExp, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TValue>();

            return Expression.Lambda<Func<TTarget, TValue>>(castResultExp, targetExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Action<TTarget, TValue> InstanceSetter<TTarget, TValue>(FieldOrProp fieldOrProperty)
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

        ////////////////////////////////////////
        //////////   Const Instance   //////////
        ////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<TValue> ConstInstanceGetter<TTarget, TValue>(FieldOrProp fieldOrProperty, TTarget constInstance)
        {
            if (constInstance is null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var targetExp = Expression.Constant(constInstance);
            var memberExp = Expression.MakeMemberAccess(targetExp, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TValue>();
            return Expression.Lambda<Func<TValue>>(castResultExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Action<TValue> ConstInstanceSetter<TTarget, TValue>(FieldOrProp fieldOrProperty, TTarget constInstance)
        {
            if (constInstance is null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var targetExp = Expression.Constant(constInstance);

            var valueExp = Expression.Parameter(typeof(TValue), "value");

            // cast value in case of getting parameter as 'object'
            var castValueExp = valueExp.Cast(fieldOrProperty.Type);

            var memberExp = Expression.MakeMemberAccess(targetExp, fieldOrProperty);
            var assignExp = Expression.Assign(memberExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).LogAndCompile();
        }

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<TValue> StaticGetter<TValue>(FieldOrProp fieldOrProperty)
        {
            var memberExp = Expression.MakeMemberAccess(null, fieldOrProperty);

            // box result in case of returning 'object'
            var castResultExp = memberExp.Cast<TValue>();
            return Expression.Lambda<Func<TValue>>(castResultExp).LogAndCompile();
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

            public Type DeclaringType => _info.DeclaringType!;

            public static implicit operator FieldOrProp(FieldInfo field) => new(field);
            public static implicit operator FieldOrProp(PropertyInfo prop) => new(prop);

            public static implicit operator FieldInfo(FieldOrProp f) => (FieldInfo) f._info;
            public static implicit operator PropertyInfo(FieldOrProp f) => (PropertyInfo) f._info;
            public static implicit operator MemberInfo(FieldOrProp f) => f._info;
        }
    }
}