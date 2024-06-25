using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<T> CreateInstanceVoidMethodCall<T>(this MethodInfo methodInfo)
        {
            var targetExp = Expression.Parameter(typeof(T), "target");
            var castTargetExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = Expression.Call(castTargetExp, methodInfo);
            return Expression.Lambda<Action<T>>(callExp, targetExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action CreateConstInstanceVoidMethodCall<T>(this MethodInfo methodInfo, T constInstance)
        {
            if (constInstance is null)
            {
                throw new ArgumentNullException(nameof(constInstance));
            }

            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo);
            return Expression.Lambda<Action>(callExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action CreateConstInstanceVoidMethodCall<T>(this T constInstance, string methodName)
        {
            var info = typeof(T).GetInstanceMethodInfo(methodName);
            return info.CreateConstInstanceVoidMethodCall(constInstance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action CreateStaticVoidMethodCall(this MethodInfo methodInfo)
        {
            var callExp = Expression.Call(null, methodInfo);
            return Expression.Lambda<Action>(callExp).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<T> CreateInstanceVoidMethodCall<T>(this Type type, string methodName)
        {
            return type.GetMethodInfoInternal(methodName, false).CreateInstanceVoidMethodCall<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action CreateConstInstanceVoidMethodCall<T>(this Type type, string methodName, T instance)
        {
            return type.GetMethodInfoInternal(methodName, false).CreateConstInstanceVoidMethodCall(instance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action CreateStaticVoidMethodCall(this Type type, string methodName)
        {
            return type.GetMethodInfoInternal(methodName, true).CreateStaticVoidMethodCall();
        }
    }
}