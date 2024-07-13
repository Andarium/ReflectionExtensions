using System.Linq.Expressions;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static Expression BoxNew(ConstructorInfo info, IEnumerable<Expression> args)
        {
            Expression call = Expression.New(info, arguments: args);
            if (info.DeclaringType!.IsValueType)
            {
                call = Expression.TypeAs(call, typeof(object));
            }

            return call;
        }

        public static ConstructorT<TTarget> CreateConstructorT<TTarget>(params Type[] argTypes)
        {
            return typeof(TTarget).GetConstructorInfo(argTypes).CreateConstructorT<TTarget>();
        }

        public static Constructor CreateConstructorX(this Type targetType, params Type[] argTypes)
        {
            return targetType.GetConstructorInfo(argTypes).CreateConstructorX();
        }

        public static Constructor<TTarget> CreateConstructor<TTarget>()
        {
            var info = typeof(TTarget).GetConstructorInfo();
            return info.CreateConstructor<TTarget>();
        }

        public static Constructor<TTarget, T0> CreateConstructor<TTarget, T0>()
        {
            var info = typeof(TTarget).GetConstructorInfo(typeof(T0));
            return info.CreateConstructor<TTarget, T0>();
        }

        public static Constructor<TTarget, T0, T1> CreateConstructor<TTarget, T0, T1>()
        {
            var info = typeof(TTarget).GetConstructorInfo(typeof(T0), typeof(T1));
            return info.CreateConstructor<TTarget, T0, T1>();
        }

        public static Constructor<TTarget, T0, T1, T2> CreateConstructor<TTarget, T0, T1, T2>()
        {
            var info = typeof(TTarget).GetConstructorInfo(typeof(T0), typeof(T1), typeof(T2));
            return info.CreateConstructor<TTarget, T0, T1, T2>();
        }

        public static Constructor<TTarget, T0, T1, T2, T3> CreateConstructor<TTarget, T0, T1, T2, T3>()
        {
            var info = typeof(TTarget).GetConstructorInfo(typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateConstructor<TTarget, T0, T1, T2, T3>();
        }

        public static Constructor<TTarget, T0, T1, T2, T3, T4> CreateConstructor<TTarget, T0, T1, T2, T3, T4>()
        {
            var info = typeof(TTarget).GetConstructorInfo(typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateConstructor<TTarget, T0, T1, T2, T3, T4>();
        }

        public static ConstructorA CreateConstructorA(this Type targetType)
        {
            var info = targetType.GetConstructorInfo();
            return info.CreateConstructorA();
        }

        public static ConstructorA<T0> CreateConstructorA<T0>(this Type targetType)
        {
            var info = targetType.GetConstructorInfo(typeof(T0));
            return info.CreateConstructorA<T0>();
        }

        public static ConstructorA<T0, T1> CreateConstructorA<T0, T1>(this Type targetType)
        {
            var info = targetType.GetConstructorInfo(typeof(T0), typeof(T1));
            return info.CreateConstructorA<T0, T1>();
        }

        public static ConstructorA<T0, T1, T2> CreateConstructorA<T0, T1, T2>(this Type targetType)
        {
            var info = targetType.GetConstructorInfo(typeof(T0), typeof(T1), typeof(T2));
            return info.CreateConstructorA<T0, T1, T2>();
        }

        public static ConstructorA<T0, T1, T2, T3> CreateConstructorA<T0, T1, T2, T3>(this Type targetType)
        {
            var info = targetType.GetConstructorInfo(typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateConstructorA<T0, T1, T2, T3>();
        }

        public static ConstructorA<T0, T1, T2, T3, T4> CreateConstructorA<T0, T1, T2, T3, T4>(this Type targetType)
        {
            var info = targetType.GetConstructorInfo(typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateConstructorA<T0, T1, T2, T3, T4>();
        }
    }
}