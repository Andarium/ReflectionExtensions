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
            return Expression.Lambda<InstanceProcedureT<TInstance>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedureX CreateInstanceProcedureX(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<InstanceProcedureX>(callExp, targetExp, arrayArgsExp).LogAndCompile();
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
            return Expression.Lambda<ConstProcedureX>(callExp, arrayArgsExp).LogAndCompile();
        }

        ////////////////////////////////////////////////////
        //////////     Static Procedures T/R/X    //////////
        ////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedureX CreateStaticProcedureX(this MethodInfo methodInfo, params Type[] argTypes)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, argTypes);
            var callExp = Expression.Call(null, methodInfo, argList);
            return Expression.Lambda<ConstProcedureX>(callExp, arrayArgsExp).LogAndCompile();
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
            return Expression.Lambda<InstanceFunctionT<TInstance>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionR<TResult> CreateInstanceFunctionR<TResult>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<InstanceFunctionR<TResult>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionTR<TInstance, TResult> CreateInstanceFunctionTR<TInstance, TResult>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var targetExp = Expression.Parameter(typeof(TInstance), "target");
            var callExp = BoxCall(targetExp, methodInfo, argList);
            return Expression.Lambda<InstanceFunctionTR<TInstance, TResult>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionX CreateInstanceFunctionX(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<InstanceFunctionX>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionA<T0> CreateInstanceFunctionA<T0>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<InstanceFunctionA<T0>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionA<T0, T1> CreateInstanceFunctionA<T0, T1>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<InstanceFunctionA<T0, T1>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionA<T0, T1, T2> CreateInstanceFunctionA<T0, T1, T2>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<InstanceFunctionA<T0, T1, T2>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionA<T0, T1, T2, T3> CreateInstanceFunctionA<T0, T1, T2, T3>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<InstanceFunctionA<T0, T1, T2, T3>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionA<T0, T1, T2, T3, T4> CreateInstanceFunctionA<T0, T1, T2, T3, T4>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = targetExp.Cast(methodInfo.DeclaringType!);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<InstanceFunctionA<T0, T1, T2, T3, T4>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        ///////////////////////////////////////////////////////////
        //////////     Const Instance Functions T/R/X    //////////
        ///////////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionR<TResult> CreateConstInstanceFunctionR<TResult>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<ConstFunctionR<TResult>>(callExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionX CreateConstInstanceFunctionX(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<ConstFunctionX>(callExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionA<T0> CreateConstInstanceFunctionA<T0>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<ConstFunctionA<T0>>(callExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionA<T0, T1> CreateConstInstanceFunctionA<T0, T1>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<ConstFunctionA<T0, T1>>(callExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionA<T0, T1, T2> CreateConstInstanceFunctionA<T0, T1, T2>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<ConstFunctionA<T0, T1, T2>>(callExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionA<T0, T1, T2, T3> CreateConstInstanceFunctionA<T0, T1, T2, T3>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<ConstFunctionA<T0, T1, T2, T3>>(callExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionA<T0, T1, T2, T3, T4> CreateConstInstanceFunctionA<T0, T1, T2, T3, T4>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var instExp = Expression.Constant(constInstance);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<ConstFunctionA<T0, T1, T2, T3, T4>>(callExp, arrayArgsExp).LogAndCompile();
        }

        ////////////////////////////////////////////////////
        //////////     Static Functions T/R/X    ///////////
        ////////////////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionX CreateStaticFunctionX(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var callExp = BoxCall(null, methodInfo, argList);
            return Expression.Lambda<ConstFunctionX>(callExp, arrayArgsExp).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionR<TResult> CreateStaticFunctionR<TResult>(this MethodInfo methodInfo)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, methodInfo.GetArgs());
            var callExp = BoxCall(null, methodInfo, argList);
            return Expression.Lambda<ConstFunctionR<TResult>>(callExp, arrayArgsExp).LogAndCompile();
        }
    }
}