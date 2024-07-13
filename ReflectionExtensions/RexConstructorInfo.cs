using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Constructor CreateConstructorX(this ConstructorInfo constructorInfo)
        {
            CreateArgumentsX(constructorInfo, out var callArgs, out var lambdaArgs);
            var newExp = BoxNew(constructorInfo, callArgs);
            return Expression.Lambda<Constructor>(newExp, lambdaArgs).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorT<TTarget> CreateConstructorT<TTarget>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArgumentsX(constructorInfo, out var callArgs, out var lambdaArgs);
            var newExp = Expression.New(constructorInfo, callArgs);
            return Expression.Lambda<ConstructorT<TTarget>>(newExp, lambdaArgs).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Constructor<TTarget> CreateConstructor<TTarget>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Constructor<TTarget>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Constructor<TTarget, T0> CreateConstructor<TTarget, T0>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Constructor<TTarget, T0>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Constructor<TTarget, T0, T1> CreateConstructor<TTarget, T0, T1>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Constructor<TTarget, T0, T1>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Constructor<TTarget, T0, T1, T2> CreateConstructor<TTarget, T0, T1, T2>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Constructor<TTarget, T0, T1, T2>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Constructor<TTarget, T0, T1, T2, T3> CreateConstructor<TTarget, T0, T1, T2, T3>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Constructor<TTarget, T0, T1, T2, T3>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Constructor<TTarget, T0, T1, T2, T3, T4> CreateConstructor<TTarget, T0, T1, T2, T3, T4>(this ConstructorInfo constructorInfo)
        {
            constructorInfo.AssertConstructor<TTarget>();
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = Expression.New(constructorInfo, arguments);
            return Expression.Lambda<Constructor<TTarget, T0, T1, T2, T3, T4>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorA CreateConstructorA(this ConstructorInfo constructorInfo)
        {
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = BoxNew(constructorInfo, arguments);
            return Expression.Lambda<ConstructorA>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorA<T0> CreateConstructorA<T0>(this ConstructorInfo constructorInfo)
        {
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = BoxNew(constructorInfo, arguments);
            return Expression.Lambda<ConstructorA<T0>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorA<T0, T1> CreateConstructorA<T0, T1>(this ConstructorInfo constructorInfo)
        {
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = BoxNew(constructorInfo, arguments);
            return Expression.Lambda<ConstructorA<T0, T1>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorA<T0, T1, T2> CreateConstructorA<T0, T1, T2>(this ConstructorInfo constructorInfo)
        {
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = BoxNew(constructorInfo, arguments);
            return Expression.Lambda<ConstructorA<T0, T1, T2>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorA<T0, T1, T2, T3> CreateConstructorA<T0, T1, T2, T3>(this ConstructorInfo constructorInfo)
        {
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = BoxNew(constructorInfo, arguments);
            return Expression.Lambda<ConstructorA<T0, T1, T2, T3>>(newExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorA<T0, T1, T2, T3, T4> CreateConstructorA<T0, T1, T2, T3, T4>(this ConstructorInfo constructorInfo)
        {
            CreateArgumentsA(constructorInfo, out var arguments);
            var newExp = BoxNew(constructorInfo, arguments);
            return Expression.Lambda<ConstructorA<T0, T1, T2, T3, T4>>(newExp, arguments).LogAndCompile();
        }
    }
}