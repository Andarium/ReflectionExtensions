using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TTarget, TValue> CreateInstanceGetter<TTarget, TValue>(this PropertyInfo prop)
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");
            var castTargetExp = targetExp.Cast(prop.DeclaringType!);
            var propExp = Expression.Property(castTargetExp, prop);
            return Expression.Lambda<Func<TTarget, TValue>>(propExp, targetExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateConstInstanceGetter<TTarget, TValue>(this PropertyInfo prop, TTarget constInstance)
        {
            if (constInstance == null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var propExp = Expression.Property(Expression.Constant(constInstance), prop);
            return Expression.Lambda<Func<TValue>>(propExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateConstInstanceGetter<TValue>(this PropertyInfo prop, object constInstance)
        {
            if (constInstance == null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var propExp = Expression.Property(Expression.Constant(constInstance), prop);
            return Expression.Lambda<Func<TValue>>(propExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateStaticGetter<TValue>(this PropertyInfo prop)
        {
            var propExp = Expression.Property(null, prop);
            return Expression.Lambda<Func<TValue>>(propExp).Compile();
        }

        // Setters

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TTarget, TValue> CreateInstanceSetter<TTarget, TValue>(this PropertyInfo prop)
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");
            var valueExp = Expression.Parameter(typeof(TValue), "value");

            var castTargetExp = targetExp.Cast(prop.DeclaringType!);
            var castValueExp = Expression.Convert(valueExp, prop.PropertyType);

            var propExp = Expression.Property(castTargetExp, prop);
            var assignExp = Expression.Assign(propExp, castValueExp);
            return Expression.Lambda<Action<TTarget, TValue>>(assignExp, targetExp, valueExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TValue> CreateConstInstanceSetter<TTarget, TValue>(this PropertyInfo prop, TTarget constInstance)
        {
            if (constInstance == null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var fieldExp = Expression.Property(Expression.Constant(constInstance), prop);
            Expression castValueExp = Expression.Convert(valueExp, prop.PropertyType);
            var assignExp = Expression.Assign(fieldExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TValue> CreateConstInstanceSetter<TValue>(this PropertyInfo prop, object constInstance)
        {
            if (constInstance == null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var fieldExp = Expression.Property(Expression.Constant(constInstance), prop);
            Expression castValueExp = Expression.Convert(valueExp, prop.PropertyType);
            var assignExp = Expression.Assign(fieldExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TValue> CreateStaticSetter<TValue>(this PropertyInfo prop)
        {
            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var fieldExp = Expression.Property(null, prop);
            Expression castValueExp = Expression.Convert(valueExp, prop.PropertyType);
            var assignExp = Expression.Assign(fieldExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).Compile();
        }
    }
}