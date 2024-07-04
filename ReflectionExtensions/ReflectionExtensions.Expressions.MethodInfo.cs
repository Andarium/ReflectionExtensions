// ReSharper disable CoVariantArrayConversion

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
        private static void CreateArguments(
            out ParameterExpression target,
            out ParameterExpression[] arguments,
            Type targetType,
            params Type[] argumentTypes
        )
        {
            target = Expression.Parameter(targetType, "target");
            CreateArguments(out arguments, argumentTypes);
        }

        private static void CreateArguments<TTarget>(
            out ParameterExpression target,
            out ParameterExpression[] arguments,
            params Type[] argumentTypes
        )
        {
            target = Expression.Parameter(typeof(TTarget), "target");
            CreateArguments(out arguments, argumentTypes);
        }

        private static void CreateArguments(
            out ParameterExpression[] arguments,
            params Type[] argumentTypes
        )
        {
            arguments = argumentTypes
                .Select((type, i) => Expression.Parameter(type, $"arg{i}"))
                .ToArray();
        }

        private static List<Expression> CreateArgumentsX(out ParameterExpression targetExp, out ParameterExpression arrayArgsExp, params Type[] argTypes)
        {
            targetExp = Expression.Parameter(typeof(object), "target");
            return CreateArgumentsX(out arrayArgsExp, argTypes);
        }

        private static List<Expression> CreateArgumentsX(out ParameterExpression arrayArgsExp, params Type[] argTypes)
        {
            arrayArgsExp = Expression.Parameter(typeof(object[]), "args");
            var argList = new List<Expression>();

            for (var i = 0; i < argTypes.Length; i++)
            {
                var arg = Expression.ArrayIndex(arrayArgsExp, Expression.Constant(i));
                var argCast = arg.Cast(argTypes[i]);
                argList.Add(argCast);
            }

            return argList;
        }

        private static Expression BoxCall(Expression? instance, MethodInfo info, IEnumerable<Expression> args)
        {
            Expression call = Expression.Call(instance, info, arguments: args);
            if (info.ReturnType.IsValueType)
            {
                call = Expression.TypeAs(call, typeof(object));
            }

            return call;
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
            return Expression.Lambda<Action<TInstance>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0> CreateInstanceProcedure<TInstance, T0>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Action<TInstance, T0>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0, T1> CreateInstanceProcedure<TInstance, T0, T1>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Action<TInstance, T0, T1>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0, T1, T2> CreateInstanceProcedure<TInstance, T0, T1, T2>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Action<TInstance, T0, T1, T2>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0, T1, T2, T3> CreateInstanceProcedure<TInstance, T0, T1, T2, T3>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Action<TInstance, T0, T1, T2, T3>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedure<TInstance, T0, T1, T2, T3, T4> CreateInstanceProcedure<TInstance, T0, T1, T2, T3, T4>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Action<TInstance, T0, T1, T2, T3, T4>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        ////////// Instance Procedure X //////////
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceProcedureX CreateInstanceProcedureX(this MethodInfo methodInfo, Type instanceType, params Type[] argTypes)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, argTypes);
            var instExp = targetExp.Cast(instanceType);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<Action<object, object[]>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        #endregion

        ///////////////////////////////////////////////////////
        //////////     Const Instance Procedures     //////////
        ///////////////////////////////////////////////////////

        #region Const Instance Procedures

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure CreateConstInstanceProcedure<TInstance>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments);
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<Action>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0> CreateConstInstanceProcedure<TInstance, T0>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1> CreateConstInstanceProcedure<TInstance, T0, T1>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2> CreateConstInstanceProcedure<TInstance, T0, T1, T2>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2, T3> CreateConstInstanceProcedure<TInstance, T0, T1, T2, T3>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2, T3>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2, T3, T4> CreateConstInstanceProcedure<TInstance, T0, T1, T2, T3, T4>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2, T3, T4>>(callExp, arguments).Compile();
        }

        ////////// Const Instance Procedure X //////////
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedureX CreateConstInstanceProcedureX(this MethodInfo methodInfo, object constInstance, Type instanceType, params Type[] argTypes)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, argTypes);
            var instExp = Expression.Constant(constInstance).Cast(instanceType);
            var callExp = Expression.Call(instExp, methodInfo, argList);
            return Expression.Lambda<Action<object[]>>(callExp, arrayArgsExp).LogAndCompile();
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
            return Expression.Lambda<Action>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0> CreateStaticProcedure<T0>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1> CreateStaticProcedure<T0, T1>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2> CreateStaticProcedure<T0, T1, T2>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2, T3> CreateStaticProcedure<T0, T1, T2, T3>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2, T3>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedure<T0, T1, T2, T3, T4> CreateStaticProcedure<T0, T1, T2, T3, T4>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<ConstProcedure<T0, T1, T2, T3, T4>>(callExp, arguments).Compile();
        }

        ////////// Static Procedure X //////////
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstProcedureX CreateStaticProcedureX(this MethodInfo methodInfo, params Type[] argTypes)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, argTypes);
            var callExp = Expression.Call(null, methodInfo, argList);
            return Expression.Lambda<Action<object[]>>(callExp, arrayArgsExp).LogAndCompile();
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
            return Expression.Lambda<Func<TInstance, TResult>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, TResult> CreateInstanceFunction<TInstance, T0, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Func<TInstance, T0, TResult>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, T1, TResult> CreateInstanceFunction<TInstance, T0, T1, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Func<TInstance, T0, T1, TResult>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, T1, T2, TResult> CreateInstanceFunction<TInstance, T0, T1, T2, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Func<TInstance, T0, T1, T2, TResult>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, T1, T2, T3, TResult> CreateInstanceFunction<TInstance, T0, T1, T2, T3, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Func<TInstance, T0, T1, T2, T3, TResult>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult> CreateInstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments<TInstance>(out var targetExp, out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(targetExp, methodInfo, arguments);
            return Expression.Lambda<Func<TInstance, T0, T1, T2, T3, T4, TResult>>(callExp, arguments.Prepend(targetExp)).Compile();
        }

        ////////// Instance Function X //////////
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceFunctionX CreateInstanceFunctionX(this MethodInfo methodInfo, Type instanceType, params Type[] argTypes)
        {
            var argList = CreateArgumentsX(out var targetExp, out var arrayArgsExp, argTypes);
            var instExp = targetExp.Cast(instanceType);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<Func<object, object[], object>>(callExp, targetExp, arrayArgsExp).LogAndCompile();
        }

        #endregion

        //////////////////////////////////////////////////////
        //////////     Const Instance Functions     //////////
        //////////////////////////////////////////////////////

        #region Const Instance Functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<TResult> CreateConstInstanceFunction<TInstance, TResult>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments);
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<Func<TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, TResult> CreateConstInstanceFunction<TInstance, T0, TResult>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<Func<T0, TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, TResult> CreateConstInstanceFunction<TInstance, T0, T1, TResult>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<Func<T0, T1, TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, TResult> CreateConstInstanceFunction<TInstance, T0, T1, T2, TResult>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, T3, TResult> CreateConstInstanceFunction<TInstance, T0, T1, T2, T3, TResult>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, T3, TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, T3, T4, TResult> CreateConstInstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult>(this MethodInfo methodInfo, TInstance instance)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(Expression.Constant(instance), methodInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, T3, T4, TResult>>(callExp, arguments).Compile();
        }

        ////////// Const Instance Function X //////////
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionX CreateConstInstanceFunctionX(this MethodInfo methodInfo, object constInstance, Type instanceType, params Type[] argTypes)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, argTypes);
            var instExp = Expression.Constant(constInstance).Cast(instanceType);
            var callExp = BoxCall(instExp, methodInfo, argList);
            return Expression.Lambda<Func<object[], object>>(callExp, arrayArgsExp).LogAndCompile();
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
            return Expression.Lambda<Func<TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, TResult> CreateStaticFunction<T0, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<Func<T0, TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, TResult> CreateStaticFunction<T0, T1, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<Func<T0, T1, TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, TResult> CreateStaticFunction<T0, T1, T2, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, T3, TResult> CreateStaticFunction<T0, T1, T2, T3, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, T3, TResult>>(callExp, arguments).Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunction<T0, T1, T2, T3, T4, TResult> CreateStaticFunction<T0, T1, T2, T3, T4, TResult>(this MethodInfo methodInfo)
        {
            CreateArguments(out var arguments, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            var callExp = Expression.Call(null, methodInfo, arguments);
            return Expression.Lambda<Func<T0, T1, T2, T3, T4, TResult>>(callExp, arguments).Compile();
        }

        ////////// Static Function X //////////
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstFunctionX CreateStaticFunctionX(this MethodInfo methodInfo, params Type[] argTypes)
        {
            var argList = CreateArgumentsX(out var arrayArgsExp, argTypes);
            var callExp = BoxCall(null, methodInfo, argList);
            return Expression.Lambda<Func<object[], object>>(callExp, arrayArgsExp).LogAndCompile();
        }

        #endregion
    }
}