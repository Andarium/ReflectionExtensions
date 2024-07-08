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

        #region Constructor Info

        public static Constructor<TTarget> CreateConstructor<TTarget>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArguments(out var arguments);
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Func<TTarget>>(newExp, arguments).LogAndCompile().Invoke;
        }

        public static Constructor<TTarget, T0> CreateConstructor<TTarget, T0>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArguments(out var arguments, typeof(T0));
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Func<T0, TTarget>>(newExp, arguments).LogAndCompile().Invoke;
        }

        public static Constructor<TTarget, T0, T1> CreateConstructor<TTarget, T0, T1>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Func<T0, T1, TTarget>>(newExp, arguments).LogAndCompile().Invoke;
        }

        public static Constructor<TTarget, T0, T1, T2> CreateConstructor<TTarget, T0, T1, T2>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, TTarget>>(newExp, arguments).LogAndCompile().Invoke;
        }

        public static Constructor<TTarget, T0, T1, T2, T3> CreateConstructor<TTarget, T0, T1, T2, T3>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, T3, TTarget>>(newExp, arguments).LogAndCompile().Invoke;
        }

        public static Constructor<TTarget, T0, T1, T2, T3, T4> CreateConstructor<TTarget, T0, T1, T2, T3, T4>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, T3, T4, TTarget>>(newExp, arguments).LogAndCompile().Invoke;
        }

        public static ConstructorT<TTarget> CreateConstructorX<TTarget>(this ConstructorInfo constructorInfo, params Type[] argTypes)
        {
            constructorInfo.AssertConstructor<TTarget>();
            var arguments = CreateArgumentsX(out var arrayArgsExp, argTypes);
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Func<object[], TTarget>>(newExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        public static ConstructorX CreateConstructorX(this ConstructorInfo constructorInfo, params Type[] argTypes)
        {
            var arguments = CreateArgumentsX(out var arrayArgsExp, argTypes);
            var newExp = BoxNew(constructorInfo, arguments);
            return Expression.Lambda<Func<object[], object>>(newExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        #endregion

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

        public static ConstructorT<TTarget> CreateConstructorX<TTarget>(params Type[] argTypes)
        {
            var info = typeof(TTarget).GetConstructorInfo(argTypes);
            return info.CreateConstructorX<TTarget>(argTypes);
        }

        public static ConstructorX CreateConstructorX(this Type targetType, params Type[] argTypes)
        {
            var info = targetType.GetConstructorInfo(argTypes);
            return info.CreateConstructorX(argTypes);
        }
    }
}