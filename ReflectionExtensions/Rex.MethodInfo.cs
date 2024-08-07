﻿// ReSharper disable CoVariantArrayConversion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static void CreateArguments<TTarget>(
            out ParameterExpression target,
            out List<ParameterExpression> arguments,
            params Type[] argumentTypes
        )
        {
            target = Expression.Parameter(typeof(TTarget), "target");
            CreateArguments(out arguments, argumentTypes);
        }

        private static void CreateArguments(
            out List<ParameterExpression> arguments,
            params Type[] argumentTypes
        )
        {
            arguments = argumentTypes
                .Select((type, i) => Expression.Parameter(type, $"arg{i}"))
                .ToList();
        }

        private static List<Expression> CreateArgumentsX(out ParameterExpression targetExp, out ParameterExpression arrayArgsExp, params Type[] argTypes)
        {
            targetExp = Expression.Parameter(typeof(object), "target");
            return CreateArgumentsX(out arrayArgsExp, argTypes);
        }

        private static List<Expression> CreateArgumentsX(out ParameterExpression lambdaArgs, params Type[] argTypes)
        {
            lambdaArgs = Expression.Parameter(typeof(object[]), "args");
            var callArgs = new List<Expression>();

            for (var i = 0; i < argTypes.Length; i++)
            {
                var arg = Expression.ArrayIndex(lambdaArgs, Expression.Constant(i));
                var argCast = arg.Cast(argTypes[i]);
                callArgs.Add(argCast);
            }

            return callArgs;
        }

        // ReSharper disable once InconsistentNaming
        // call site instance obj => target method instance DeclaringType
        private static void CreateArgumentsIA(MethodInfo methodInfo, out Expression callTargetExp, out IEnumerable<Expression> callArgs, out IEnumerable<ParameterExpression> lambdaArgs)
        {
            var lambdaTargetExp = Expression.Parameter(typeof(object), "target");
            callTargetExp = lambdaTargetExp.Cast(methodInfo.DeclaringType!);
            var args = CreateArgumentsA(methodInfo);
            lambdaArgs = args.Prepend(lambdaTargetExp);
            callArgs = args;
        }

        // ReSharper disable once InconsistentNaming
        // call site instance DeclaringType => target method instance DeclaringType
        private static void CreateArgumentsITA(MethodInfo methodInfo, out ParameterExpression callTargetExp, out IEnumerable<Expression> callArgs, out IEnumerable<ParameterExpression> lambdaArgs)
        {
            callTargetExp = Expression.Parameter(methodInfo.DeclaringType!, "target");
            var args = CreateArgumentsA(methodInfo);
            lambdaArgs = args.Prepend(callTargetExp);
            callArgs = args;
        }

        private static void CreateArgumentsA(ConstructorInfo constructorInfo, out List<ParameterExpression> argList)
        {
            argList = CreateArgumentsA(constructorInfo);
        }

        private static void CreateArgumentsX(ConstructorInfo constructorInfo, out List<Expression> callArgs, out ParameterExpression lambdaArgs)
        {
            var argTypes = constructorInfo.GetArgs();

            lambdaArgs = Expression.Parameter(typeof(object[]), "args");
            callArgs = new List<Expression>();

            for (var i = 0; i < argTypes.Length; i++)
            {
                var arg = Expression.ArrayIndex(lambdaArgs, Expression.Constant(i));
                var argCast = arg.Cast(argTypes[i]);
                callArgs.Add(argCast);
            }
        }

        private static List<ParameterExpression> CreateArgumentsA(MethodBase info)
        {
            return info.GetArgs().Select((x, i) => Expression.Parameter(x, $"arg{i}")).ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Expression BoxCall(Expression? instance, MethodInfo info, IEnumerable<Expression> args)
        {
            Expression call = Expression.Call(instance, info, arguments: args);
            return call.Box();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Expression Box(this Expression exp)
        {
            if (exp.Type.IsValueType)
            {
                exp = Expression.TypeAs(exp, typeof(object));
            }

            return exp;
        }

        /////////////////////////////////////////////////
        //////////     Instance Procedures     //////////
        /////////////////////////////////////////////////

        #region Instance Procedures

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance> CreateInstanceProcedure<TInstance>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments);
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceProcedure<TInstance>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0> CreateInstanceProcedure<TInstance, T0>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceProcedure<TInstance, T0>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0, T1> CreateInstanceProcedure<TInstance, T0, T1>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceProcedure<TInstance, T0, T1>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0, T1, T2> CreateInstanceProcedure<TInstance, T0, T1, T2>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceProcedure<TInstance, T0, T1, T2>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0, T1, T2, T3> CreateInstanceProcedure<TInstance, T0, T1, T2, T3>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceProcedure<TInstance, T0, T1, T2, T3>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0, T1, T2, T3, T4> CreateInstanceProcedure<TInstance, T0, T1, T2, T3, T4>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceProcedure<TInstance, T0, T1, T2, T3, T4>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        #endregion

        ///////////////////////////////////////////////////////
        //////////     Const Instance Procedures     //////////
        ///////////////////////////////////////////////////////

        #region Const Instance Procedures

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure CreateConstInstanceProcedure(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments);
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0> CreateConstInstanceProcedure<T0>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1> CreateConstInstanceProcedure<T0, T1>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2> CreateConstInstanceProcedure<T0, T1, T2>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2, T3> CreateConstInstanceProcedure<T0, T1, T2, T3>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2, T3>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2, T3, T4> CreateConstInstanceProcedure<T0, T1, T2, T3, T4>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2, T3, T4>>(callExp, arguments).LogAndCompile();
        }

        #endregion

        ///////////////////////////////////////////////
        //////////     Static Procedures     //////////
        ///////////////////////////////////////////////

        #region Static Procedures

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure CreateStaticProcedure(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments);
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0> CreateStaticProcedure<T0>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1> CreateStaticProcedure<T0, T1>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2> CreateStaticProcedure<T0, T1, T2>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2, T3> CreateStaticProcedure<T0, T1, T2, T3>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2, T3>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2, T3, T4> CreateStaticProcedure<T0, T1, T2, T3, T4>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2, T3, T4>>(callExp, arguments).LogAndCompile();
        }

        #endregion

        ////////////////////////////////////////////////
        //////////     Instance Functions     //////////
        ////////////////////////////////////////////////

        #region Instance Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, TResult> CreateInstanceFunction<TInstance, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments);
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceFunction<TInstance, TResult>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, TResult> CreateInstanceFunction<TInstance, T0, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceFunction<TInstance, T0, TResult>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, T1, TResult> CreateInstanceFunction<TInstance, T0, T1, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceFunction<TInstance, T0, T1, TResult>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, T1, T2, TResult> CreateInstanceFunction<TInstance, T0, T1, T2, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceFunction<TInstance, T0, T1, T2, TResult>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, T1, T2, T3, TResult> CreateInstanceFunction<TInstance, T0, T1, T2, T3, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceFunction<TInstance, T0, T1, T2, T3, TResult>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult> CreateInstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<InstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult>>(callExp, arguments.Prepend(targetExp)).LogAndCompile();
        }

        #endregion

        //////////////////////////////////////////////////////
        //////////     Const Instance Functions     //////////
        //////////////////////////////////////////////////////

        #region Const Instance Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<TResult> CreateConstInstanceFunction<TResult>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments);
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstFunction<TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, TResult> CreateConstInstanceFunction<T0, TResult>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, TResult> CreateConstInstanceFunction<T0, T1, TResult>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, T1, TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, TResult> CreateConstInstanceFunction<T0, T1, T2, TResult>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, T1, T2, TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, T3, TResult> CreateConstInstanceFunction<T0, T1, T2, T3, TResult>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, T1, T2, T3, TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, T3, T4, TResult> CreateConstInstanceFunction<T0, T1, T2, T3, T4, TResult>(this MethodInfo methodInfo, object constInstance)
        {
            AssertInstance(constInstance, methodInfo.Name, MemberType.Method);
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(Expression.Constant(constInstance), methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, T1, T2, T3, T4, TResult>>(callExp, arguments).LogAndCompile();
        }

        #endregion

        //////////////////////////////////////////////
        //////////     Static Functions     //////////
        //////////////////////////////////////////////

        #region Static Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<TResult> CreateStaticFunction<TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments);
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstFunction<TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, TResult> CreateStaticFunction<T0, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, TResult> CreateStaticFunction<T0, T1, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, T1, TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, TResult> CreateStaticFunction<T0, T1, T2, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, T1, T2, TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, T3, TResult> CreateStaticFunction<T0, T1, T2, T3, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, T1, T2, T3, TResult>>(callExp, arguments).LogAndCompile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, T3, T4, TResult> CreateStaticFunction<T0, T1, T2, T3, T4, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstFunction<T0, T1, T2, T3, T4, TResult>>(callExp, arguments).LogAndCompile();
        }

        #endregion
    }
}