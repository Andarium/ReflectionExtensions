using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TTarget, TValue> CreateInstanceGetter<TTarget, TValue>(this FieldInfo field)
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");
            var castTargetExp = targetExp.Cast(field.DeclaringType!);
            var fieldExp = Expression.Field(castTargetExp, field);
            return Expression.Lambda<Func<TTarget, TValue>>(fieldExp, targetExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateConstInstanceGetter<TTarget, TValue>(this FieldInfo field, TTarget constInstance)
        {
            if (constInstance is null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var propExp = Expression.Field(Expression.Constant(constInstance), field);
            return Expression.Lambda<Func<TValue>>(propExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateConstInstanceGetter<TValue>(this FieldInfo field, object constInstance)
        {
            if (constInstance is null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var propExp = Expression.Field(Expression.Constant(constInstance), field);
            return Expression.Lambda<Func<TValue>>(propExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateStaticGetter<TValue>(this FieldInfo field)
        {
            var propExp = Expression.Field(null, field);
            return Expression.Lambda<Func<TValue>>(propExp).Compile();
        }

        // Setters

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TTarget, TValue> CreateInstanceSetter<TTarget, TValue>(this FieldInfo field)
        {
            var targetExp = Expression.Parameter(typeof(TTarget), "target");
            var valueExp = Expression.Parameter(typeof(TValue), "value");

            var castTargetExp = targetExp.Cast(field.DeclaringType!);
            var castValueExp = valueExp.Cast(field.FieldType);

            var fieldExp = Expression.Field(castTargetExp, field);
            var assignExp = Expression.Assign(fieldExp, castValueExp);
            return Expression.Lambda<Action<TTarget, TValue>>(assignExp, targetExp, valueExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TValue> CreateConstInstanceSetter<TTarget, TValue>(this FieldInfo field, TTarget constInstance)
        {
            if (constInstance == null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var fieldExp = Expression.Field(Expression.Constant(constInstance), field);
            var castValueExp = valueExp.Cast(field.FieldType);
            var assignExp = Expression.Assign(fieldExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TValue> CreateConstInstanceSetter<TValue>(this FieldInfo field, object constInstance)
        {
            if (constInstance == null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var fieldExp = Expression.Field(Expression.Constant(constInstance), field);
            var castValueExp = valueExp.Cast(field.FieldType);
            var assignExp = Expression.Assign(fieldExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TValue> CreateStaticSetter<TValue>(this FieldInfo field)
        {
            var valueExp = Expression.Parameter(typeof(TValue), "value");
            var fieldExp = Expression.Field(null, field);
            var castValueExp = valueExp.Cast(field.FieldType);
            var assignExp = Expression.Assign(fieldExp, castValueExp);
            return Expression.Lambda<Action<TValue>>(assignExp, valueExp).Compile();
        }
    }
}