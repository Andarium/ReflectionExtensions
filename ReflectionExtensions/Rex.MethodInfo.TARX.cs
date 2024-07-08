// ReSharper disable InconsistentNaming

using System.Linq.Expressions;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        //////////////////////////////////////////////////////
        //////////     Instance Procedures T/R/X    //////////
        //////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedureT<TInstance> CreateInstanceProcedureT<TInstance>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var targetExp = Expression.Parameter(typeof(TInstance), "target");
            var callExp = BoxCall(targetExp, methodInfo, argList);
            return Expression.Lambda<Action<TInstance, object[]>>(callExp, targetExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedureX CreateInstanceProcedureX(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<Action<object, object[]>>(callExp, targetExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        ////////////////////////////////////////////////////////////
        //////////     Const Instance Procedures T/R/X    //////////
        ////////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedureX CreateConstInstanceProcedureX(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<Action<object[]>>(callExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        ////////////////////////////////////////////////////
        //////////     Static Procedures T/R/X    //////////
        ////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedureX CreateStaticProcedureX(this MethodInfo methodInfo, params Type[] argTypes)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, argTypes);
            var callExp = Expression.Call(null, methodInfo, argList);
            return Expression.Lambda<Action<object[]>>(callExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        /////////////////////////////////////////////////////
        //////////     Instance Functions T/R/X    //////////
        /////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionT<TInstance> CreateInstanceFunctionT<TInstance>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var targetExp = Expression.Parameter(typeof(TInstance), "target");
            var callExp = BoxCall(targetExp, methodInfo, argList);
            return Expression.Lambda<Func<TInstance, object[], object>>(callExp, targetExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionR<TResult> CreateInstanceFunctionR<TResult>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<Func<object, object[], TResult>>(callExp, targetExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionTR<TInstance, TResult> CreateInstanceFunctionTR<TInstance, TResult>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var targetExp = Expression.Parameter(typeof(TInstance), "target");
            var callExp = BoxCall(targetExp, methodInfo, argList);
            return Expression.Lambda<Func<TInstance, object[], TResult>>(callExp, targetExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionX CreateInstanceFunctionX(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<Func<object, object[], object>>(callExp, targetExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        ///////////////////////////////////////////////////////////
        //////////     Const Instance Functions T/R/X    //////////
        ///////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionX CreateConstInstanceFunctionT<TInstance>(this MethodInfo methodInfo, TInstance constInstance)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<Func<object[], object>>(callExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionR<TResult> CreateConstInstanceFunctionR<TResult>(this MethodInfo methodInfo, object constInstance)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<Func<object[], TResult>>(callExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionR<TResult> CreateConstInstanceFunctionTR<TInstance, TResult>(this MethodInfo methodInfo, TInstance constInstance)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<Func<object[], TResult>>(callExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionX CreateConstInstanceFunctionX(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<Func<object[], object>>(callExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        ////////////////////////////////////////////////////
        //////////     Static Functions T/R/X    ///////////
        ////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionX CreateStaticFunctionX(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var callExp = BoxCall(null, methodInfo, argList);
            return Expression.Lambda<Func<object[], object>>(callExp, arrayArgsExp).LogAndCompile().Invoke;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionR<TResult> CreateStaticFunctionR<TResult>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var callExp = BoxCall(null, methodInfo, argList);
            return Expression.Lambda<Func<object[], TResult>>(callExp, arrayArgsExp).LogAndCompile().Invoke;
        }
    }
}