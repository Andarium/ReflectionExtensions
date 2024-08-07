﻿// ReSharper disable InconsistentNaming

using System;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        ////////////////////////////////////////////////////////
        //////////     Instance Procedures T/A/R/X    //////////
        ////////////////////////////////////////////////////////

        public static InstanceProcedureT<TInstance> CreateInstanceProcedureT<TInstance>(string methodName, params Type[] argTypes)
        {
            return GetInstanceMethodInfo<TInstance>(methodName, argTypes).CreateInstanceProcedureT<TInstance>();
        }

        public static InstanceProcedureX CreateInstanceProcedureX(this Type instanceType, string methodName, params Type[] argTypes)
        {
            return instanceType.GetInstanceMethodInfo(methodName, argTypes).CreateInstanceProcedureX();
        }

        public static InstanceProcedureA CreateInstanceProcedureA(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName);
            return info.CreateInstanceProcedureA();
        }

        public static InstanceProcedureA<T0> CreateInstanceProcedureA<T0>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0));
            return info.CreateInstanceProcedureA<T0>();
        }

        public static InstanceProcedureA<T0, T1> CreateInstanceProcedureA<T0, T1>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1));
            return info.CreateInstanceProcedureA<T0, T1>();
        }

        public static InstanceProcedureA<T0, T1, T2> CreateInstanceProcedureA<T0, T1, T2>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateInstanceProcedureA<T0, T1, T2>();
        }

        public static InstanceProcedureA<T0, T1, T2, T3> CreateInstanceProcedureA<T0, T1, T2, T3>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateInstanceProcedureA<T0, T1, T2, T3>();
        }

        public static InstanceProcedureA<T0, T1, T2, T3, T4> CreateInstanceProcedureA<T0, T1, T2, T3, T4>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateInstanceProcedureA<T0, T1, T2, T3, T4>();
        }

        //////////////////////////////////////////////////////////////
        //////////     Const Instance Procedures T/A/R/X    //////////
        //////////////////////////////////////////////////////////////

        public static ConstProcedureX CreateConstInstanceProcedure(this object constInstance, string methodName, params Type[] argTypes)
        {
            AssertInstance(constInstance, out var instanceType, methodName, MemberType.Method);
            return instanceType.GetInstanceMethodInfo(methodName, argTypes).CreateConstInstanceProcedureX(constInstance);
        }

        //////////////////////////////////////////////////////
        //////////     Static Procedures T/A/R/X    //////////
        //////////////////////////////////////////////////////

        public static ConstProcedureX CreateStaticProcedure<TTarget>(string methodName, params Type[] argTypes)
        {
            return GetStaticMethodInfo<TTarget>(methodName, argTypes).CreateStaticProcedureX(argTypes);
        }

        public static ConstProcedureX CreateStaticProcedure(this Type targetType, string methodName, params Type[] argTypes)
        {
            return targetType.GetStaticMethodInfo(methodName, argTypes).CreateStaticProcedureX(argTypes);
        }

        ///////////////////////////////////////////////////////
        //////////     Instance Functions T/A/R/X    //////////
        ///////////////////////////////////////////////////////

        public static InstanceFunctionT<TInstance> CreateInstanceFunctionT<TInstance>(string methodName, params Type[] argTypes)
        {
            return GetInstanceMethodInfo<TInstance>(methodName, argTypes).CreateInstanceFunctionT<TInstance>();
        }

        public static InstanceFunctionR<TResult> CreateInstanceFunctionR<TResult>(this Type targetType, string methodName, params Type[] argTypes)
        {
            return targetType.GetInstanceMethodInfo(methodName, argTypes).CreateInstanceFunctionR<TResult>();
        }

        public static InstanceFunctionTR<TInstance, TResult> CreateInstanceFunctionTR<TInstance, TResult>(string methodName, params Type[] argTypes)
        {
            return GetInstanceMethodInfo<TInstance>(methodName, argTypes).CreateInstanceFunctionTR<TInstance, TResult>();
        }

        public static InstanceFunctionX CreateInstanceFunctionX(this Type instanceType, string methodName, params Type[] argTypes)
        {
            return instanceType.GetInstanceMethodInfo(methodName, argTypes).CreateInstanceFunctionX();
        }

        public static InstanceFunctionA<T0> CreateInstanceFunctionA<T0>(this Type instanceType, string methodName)
        {
            return instanceType.GetInstanceMethodInfo(methodName, typeof(T0)).CreateInstanceFunctionA<T0>();
        }

        public static InstanceFunctionA CreateInstanceFunctionA(this Type instanceType, string methodName)
        {
            return instanceType.GetInstanceMethodInfo(methodName).CreateInstanceFunctionA();
        }

        public static InstanceFunctionA<T0, T1> CreateInstanceFunctionA<T0, T1>(this Type instanceType, string methodName)
        {
            return instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1)).CreateInstanceFunctionA<T0, T1>();
        }

        public static InstanceFunctionA<T0, T1, T2> CreateInstanceFunctionA<T0, T1, T2>(this Type instanceType, string methodName)
        {
            return instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2)).CreateInstanceFunctionA<T0, T1, T2>();
        }

        public static InstanceFunctionA<T0, T1, T2, T3> CreateInstanceFunctionA<T0, T1, T2, T3>(this Type instanceType, string methodName)
        {
            return instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3)).CreateInstanceFunctionA<T0, T1, T2, T3>();
        }

        public static InstanceFunctionA<T0, T1, T2, T3, T4> CreateInstanceFunctionA<T0, T1, T2, T3, T4>(this Type instanceType, string methodName)
        {
            return instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4)).CreateInstanceFunctionA<T0, T1, T2, T3, T4>();
        }

        public static InstanceFunctionAR<TR> CreateInstanceFunctionAR<TR>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName);
            return info.CreateInstanceFunctionAR<TR>();
        }

        public static InstanceFunctionAR<T0, TR> CreateInstanceFunctionAR<T0, TR>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0));
            return info.CreateInstanceFunctionAR<T0, TR>();
        }

        public static InstanceFunctionAR<T0, T1, TR> CreateInstanceFunctionAR<T0, T1, TR>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1));
            return info.CreateInstanceFunctionAR<T0, T1, TR>();
        }

        public static InstanceFunctionAR<T0, T1, T2, TR> CreateInstanceFunctionAR<T0, T1, T2, TR>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateInstanceFunctionAR<T0, T1, T2, TR>();
        }

        public static InstanceFunctionAR<T0, T1, T2, T3, TR> CreateInstanceFunctionAR<T0, T1, T2, T3, TR>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateInstanceFunctionAR<T0, T1, T2, T3, TR>();
        }

        public static InstanceFunctionAR<T0, T1, T2, T3, T4, TR> CreateInstanceFunctionAR<T0, T1, T2, T3, T4, TR>(this Type instanceType, string methodName)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateInstanceFunctionAR<T0, T1, T2, T3, T4, TR>();
        }

        public static InstanceFunctionTA<TInstance> CreateInstanceFunctionTA<TInstance>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName);
            return info.CreateInstanceFunctionTA<TInstance>();
        }

        public static InstanceFunctionTA<TInstance, T0> CreateInstanceFunctionTA<TInstance, T0>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0));
            return info.CreateInstanceFunctionTA<TInstance, T0>();
        }

        public static InstanceFunctionTA<TInstance, T0, T1> CreateInstanceFunctionTA<TInstance, T0, T1>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1));
            return info.CreateInstanceFunctionTA<TInstance, T0, T1>();
        }

        public static InstanceFunctionTA<TInstance, T0, T1, T2> CreateInstanceFunctionTA<TInstance, T0, T1, T2>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateInstanceFunctionTA<TInstance, T0, T1, T2>();
        }

        public static InstanceFunctionTA<TInstance, T0, T1, T2, T3> CreateInstanceFunctionTA<TInstance, T0, T1, T2, T3>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateInstanceFunctionTA<TInstance, T0, T1, T2, T3>();
        }

        public static InstanceFunctionTA<TInstance, T0, T1, T2, T3, T4> CreateInstanceFunctionTA<TInstance, T0, T1, T2, T3, T4>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateInstanceFunctionTA<TInstance, T0, T1, T2, T3, T4>();
        }

        /////////////////////////////////////////////////////////////
        //////////     Const Instance Functions T/A/R/X    //////////
        /////////////////////////////////////////////////////////////

        public static ConstFunctionR<TResult> CreateConstInstanceFunctionR<TResult>(this object constInstance, string methodName, params Type[] argTypes)
        {
            AssertInstance(constInstance, out var instanceType, methodName, MemberType.Method);
            return instanceType.GetInstanceMethodInfo(methodName, argTypes).CreateConstInstanceFunctionR<TResult>(constInstance);
        }

        public static ConstFunctionX CreateConstInstanceFunction(this object constInstance, string methodName, params Type[] argTypes)
        {
            AssertInstance(constInstance, out var instanceType, methodName, MemberType.Method);
            return instanceType.GetInstanceMethodInfo(methodName, argTypes).CreateConstInstanceFunctionX(constInstance);
        }

        public static ConstFunctionA CreateConstInstanceFunctionA(this object constInstance, string methodName)
        {
            var info = constInstance.GetType().GetInstanceMethodInfo(methodName);
            return info.CreateConstInstanceFunctionA(constInstance);
        }

        public static ConstFunctionA<T0> CreateConstInstanceFunctionA<T0>(this object constInstance, string methodName)
        {
            var info = constInstance.GetType().GetInstanceMethodInfo(methodName, typeof(T0));
            return info.CreateConstInstanceFunctionA<T0>(constInstance);
        }

        public static ConstFunctionA<T0, T1> CreateConstInstanceFunctionA<T0, T1>(this object constInstance, string methodName)
        {
            var info = constInstance.GetType().GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1));
            return info.CreateConstInstanceFunctionA<T0, T1>(constInstance);
        }

        public static ConstFunctionA<T0, T1, T2> CreateConstInstanceFunctionA<T0, T1, T2>(this object constInstance, string methodName)
        {
            var info = constInstance.GetType().GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateConstInstanceFunctionA<T0, T1, T2>(constInstance);
        }

        public static ConstFunctionA<T0, T1, T2, T3> CreateConstInstanceFunctionA<T0, T1, T2, T3>(this object constInstance, string methodName)
        {
            var info = constInstance.GetType().GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateConstInstanceFunctionA<T0, T1, T2, T3>(constInstance);
        }

        public static ConstFunctionA<T0, T1, T2, T3, T4> CreateConstInstanceFunctionA<T0, T1, T2, T3, T4>(this object constInstance, string methodName)
        {
            var info = constInstance.GetType().GetInstanceMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateConstInstanceFunctionA<T0, T1, T2, T3, T4>(constInstance);
        }

        //////////////////////////////////////////////////////
        //////////     Static Functions T/A/R/X    ///////////
        //////////////////////////////////////////////////////

        public static ConstFunctionX CreateStaticFunctionT<TTarget>(string methodName, params Type[] argTypes)
        {
            return GetStaticMethodInfo<TTarget>(methodName, argTypes).CreateStaticFunctionX();
        }

        public static ConstFunctionR<TResult> CreateStaticFunctionR<TResult>(this Type targetType, string methodName, params Type[] argTypes)
        {
            return targetType.GetStaticMethodInfo(methodName, argTypes).CreateStaticFunctionR<TResult>();
        }

        public static ConstFunctionR<TResult> CreateStaticFunctionTR<TTarget, TResult>(string methodName, params Type[] argTypes)
        {
            return GetStaticMethodInfo<TTarget>(methodName, argTypes).CreateStaticFunctionR<TResult>();
        }

        public static ConstFunctionX CreateStaticFunctionX(this Type targetType, string methodName, params Type[] argTypes)
        {
            return targetType.GetStaticMethodInfo(methodName, argTypes).CreateStaticFunctionX();
        }

        public static ConstFunctionA CreateStaticFunctionA(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName);
            return info.CreateStaticFunctionA();
        }

        public static ConstFunctionA<T0> CreateStaticFunctionA<T0>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0));
            return info.CreateStaticFunctionA<T0>();
        }

        public static ConstFunctionA<T0, T1> CreateStaticFunctionA<T0, T1>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1));
            return info.CreateStaticFunctionA<T0, T1>();
        }

        public static ConstFunctionA<T0, T1, T2> CreateStaticFunctionA<T0, T1, T2>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateStaticFunctionA<T0, T1, T2>();
        }

        public static ConstFunctionA<T0, T1, T2, T3> CreateStaticFunctionA<T0, T1, T2, T3>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateStaticFunctionA<T0, T1, T2, T3>();
        }

        public static ConstFunctionA<T0, T1, T2, T3, T4> CreateStaticFunctionA<T0, T1, T2, T3, T4>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateStaticFunctionA<T0, T1, T2, T3, T4>();
        }

        public static ConstFunctionA CreateStaticFunctionTA<TTarget>(string methodName)
        {
            var info = typeof(TTarget).GetStaticMethodInfo(methodName);
            return info.CreateStaticFunctionA();
        }

        public static ConstFunctionA<T0> CreateStaticFunctionTA<TTarget, T0>(string methodName)
        {
            var info = typeof(TTarget).GetStaticMethodInfo(methodName, typeof(T0));
            return info.CreateStaticFunctionA<T0>();
        }

        public static ConstFunctionA<T0, T1> CreateStaticFunctionTA<TTarget, T0, T1>(string methodName)
        {
            var info = typeof(TTarget).GetStaticMethodInfo(methodName, typeof(T0), typeof(T1));
            return info.CreateStaticFunctionA<T0, T1>();
        }

        public static ConstFunctionA<T0, T1, T2> CreateStaticFunctionTA<TTarget, T0, T1, T2>(string methodName)
        {
            var info = typeof(TTarget).GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateStaticFunctionA<T0, T1, T2>();
        }

        public static ConstFunctionA<T0, T1, T2, T3> CreateStaticFunctionTA<TTarget, T0, T1, T2, T3>(string methodName)
        {
            var info = typeof(TTarget).GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateStaticFunctionA<T0, T1, T2, T3>();
        }

        public static ConstFunctionA<T0, T1, T2, T3, T4> CreateStaticFunctionTA<TTarget, T0, T1, T2, T3, T4>(string methodName)
        {
            var info = typeof(TTarget).GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateStaticFunctionA<T0, T1, T2, T3, T4>();
        }
    }
}