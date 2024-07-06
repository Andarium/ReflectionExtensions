// ReSharper disable CoVariantArrayConversion

using System;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        /////////////////////////////////////////////////
        //////////     Instance Procedures     //////////
        /////////////////////////////////////////////////

        #region Instance Procedures

        public static InstanceProcedure<TInstance> CreateInstanceProcedure<TInstance>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName);
            return info.CreateInstanceProcedure<TInstance>();
        }

        public static InstanceProcedure<TInstance, T0> CreateInstanceProcedure<TInstance, T0>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0));
            return info.CreateInstanceProcedure<TInstance, T0>();
        }

        public static InstanceProcedure<TInstance, T0, T1> CreateInstanceProcedure<TInstance, T0, T1>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1));
            return info.CreateInstanceProcedure<TInstance, T0, T1>();
        }

        public static InstanceProcedure<TInstance, T0, T1, T2> CreateInstanceProcedure<TInstance, T0, T1, T2>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateInstanceProcedure<TInstance, T0, T1, T2>();
        }

        public static InstanceProcedure<TInstance, T0, T1, T2, T3> CreateInstanceProcedure<TInstance, T0, T1, T2, T3>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateInstanceProcedure<TInstance, T0, T1, T2, T3>();
        }

        public static InstanceProcedure<TInstance, T0, T1, T2, T3, T4> CreateInstanceProcedure<TInstance, T0, T1, T2, T3, T4>(string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateInstanceProcedure<TInstance, T0, T1, T2, T3, T4>();
        }

        ////////// Instance Procedure X //////////
        public static InstanceProcedureX CreateInstanceProcedureX(this Type instanceType, string methodName, params Type[] argTypes)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, argTypes);
            return info.CreateInstanceProcedureX(instanceType);
        }

        #endregion

        ///////////////////////////////////////////////////////
        //////////     Const Instance Procedures     //////////
        ///////////////////////////////////////////////////////

        #region Const Instance Procedures

        public static ConstProcedure CreateConstInstanceProcedure<TInstance>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName);
            return info.CreateConstInstanceProcedure<TInstance>(constInstance);
        }

        public static ConstProcedure<T0> CreateConstInstanceProcedure<TInstance, T0>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0));
            return info.CreateConstInstanceProcedure<TInstance, T0>(constInstance);
        }

        public static ConstProcedure<T0, T1> CreateConstInstanceProcedure<TInstance, T0, T1>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1));
            return info.CreateConstInstanceProcedure<TInstance, T0, T1>(constInstance);
        }

        public static ConstProcedure<T0, T1, T2> CreateConstInstanceProcedure<TInstance, T0, T1, T2>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateConstInstanceProcedure<TInstance, T0, T1, T2>(constInstance);
        }

        public static ConstProcedure<T0, T1, T2, T3> CreateConstInstanceProcedure<TInstance, T0, T1, T2, T3>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateConstInstanceProcedure<TInstance, T0, T1, T2, T3>(constInstance);
        }

        public static ConstProcedure<T0, T1, T2, T3, T4> CreateConstInstanceProcedure<TInstance, T0, T1, T2, T3, T4>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateConstInstanceProcedure<TInstance, T0, T1, T2, T3, T4>(constInstance);
        }

        ////////// Const Instance Procedure X //////////
        public static ConstProcedureX CreateConstInstanceProcedureX(this object constInstance, string methodName, params Type[] argTypes)
        {
            AssertInstance(constInstance, out var instanceType, methodName, MemberType.Method);
            var info = instanceType.GetInstanceMethodInfo(methodName, argTypes);
            return info.CreateConstInstanceProcedureX(constInstance);
        }

        #endregion

        ///////////////////////////////////////////////
        //////////     Static Procedures     //////////
        ///////////////////////////////////////////////

        #region Static Procedures

        public static ConstProcedure CreateStaticProcedure(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName);
            return info.CreateStaticProcedure();
        }

        public static ConstProcedure<T0> CreateStaticProcedure<T0>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0));
            return info.CreateStaticProcedure<T0>();
        }

        public static ConstProcedure<T0, T1> CreateStaticProcedure<T0, T1>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1));
            return info.CreateStaticProcedure<T0, T1>();
        }

        public static ConstProcedure<T0, T1, T2> CreateStaticProcedure<T0, T1, T2>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateStaticProcedure<T0, T1, T2>();
        }

        public static ConstProcedure<T0, T1, T2, T3> CreateStaticProcedure<T0, T1, T2, T3>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateStaticProcedure<T0, T1, T2, T3>();
        }

        public static ConstProcedure<T0, T1, T2, T3, T4> CreateStaticProcedure<T0, T1, T2, T3, T4>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateStaticProcedure<T0, T1, T2, T3, T4>();
        }

        public static ConstProcedure CreateStaticProcedure<TTarget>(string methodName) => typeof(TTarget).CreateStaticProcedure(methodName);

        public static ConstProcedure<T0> CreateStaticProcedure<TTarget, T0>(string methodName) => typeof(TTarget).CreateStaticProcedure<T0>(methodName);

        public static ConstProcedure<T0, T1> CreateStaticProcedure<TTarget, T0, T1>(string methodName) => typeof(TTarget).CreateStaticProcedure<T0, T1>(methodName);

        public static ConstProcedure<T0, T1, T2> CreateStaticProcedure<TTarget, T0, T1, T2>(string methodName) => typeof(TTarget).CreateStaticProcedure<T0, T1, T2>(methodName);

        public static ConstProcedure<T0, T1, T2, T3> CreateStaticProcedure<TTarget, T0, T1, T2, T3>(string methodName) => typeof(TTarget).CreateStaticProcedure<T0, T1, T2, T3>(methodName);

        public static ConstProcedure<T0, T1, T2, T3, T4> CreateStaticProcedure<TTarget, T0, T1, T2, T3, T4>(string methodName) => typeof(TTarget).CreateStaticProcedure<T0, T1, T2, T3, T4>(methodName);

        ////////// Instance Procedure X //////////
        public static ConstProcedureX CreateStaticProcedureX(this Type targetType, string methodName, params Type[] argTypes)
        {
            var info = targetType.GetInstanceMethodInfo(methodName, argTypes);
            return info.CreateStaticProcedureX(argTypes);
        }

        #endregion

        ////////////////////////////////////////////////
        //////////     Instance Functions     //////////
        ////////////////////////////////////////////////

        #region Instance Functions

        public static InstanceFunction<TTarget, TR> CreateInstanceFunction<TTarget, TR>(string methodName)
        {
            var info = GetInstanceMethodInfo<TTarget>(methodName);
            return info.CreateInstanceFunction<TTarget, TR>();
        }

        public static InstanceFunction<TTarget, T0, TR> CreateInstanceFunction<TTarget, T0, TR>(string methodName)
        {
            var info = GetInstanceMethodInfo<TTarget>(methodName, typeof(T0));
            return info.CreateInstanceFunction<TTarget, T0, TR>();
        }

        public static InstanceFunction<TTarget, T0, T1, TR> CreateInstanceFunction<TTarget, T0, T1, TR>(string methodName)
        {
            var info = GetInstanceMethodInfo<TTarget>(methodName, typeof(T0), typeof(T1));
            return info.CreateInstanceFunction<TTarget, T0, T1, TR>();
        }

        public static InstanceFunction<TTarget, T0, T1, T2, TR> CreateInstanceFunction<TTarget, T0, T1, T2, TR>(string methodName)
        {
            var info = GetInstanceMethodInfo<TTarget>(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateInstanceFunction<TTarget, T0, T1, T2, TR>();
        }

        public static InstanceFunction<TTarget, T0, T1, T2, T3, TR> CreateInstanceFunction<TTarget, T0, T1, T2, T3, TR>(string methodName)
        {
            var info = GetInstanceMethodInfo<TTarget>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateInstanceFunction<TTarget, T0, T1, T2, T3, TR>();
        }

        public static InstanceFunction<TTarget, T0, T1, T2, T3, T4, TR> CreateInstanceFunction<TTarget, T0, T1, T2, T3, T4, TR>(string methodName)
        {
            var info = GetInstanceMethodInfo<TTarget>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateInstanceFunction<TTarget, T0, T1, T2, T3, T4, TR>();
        }

        ////////// Instance Function X //////////
        public static InstanceFunctionX CreateInstanceFunctionX(this Type instanceType, string methodName, params Type[] argTypes)
        {
            var info = instanceType.GetInstanceMethodInfo(methodName, argTypes);
            return info.CreateInstanceFunctionX(instanceType);
        }

        #endregion

        //////////////////////////////////////////////////////
        //////////     Const Instance Functions     //////////
        //////////////////////////////////////////////////////

        #region Const Instance Functions

        public static ConstFunction<TR> CreateConstInstanceFunction<TInstance, TR>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName);
            return info.CreateConstInstanceFunction<TInstance, TR>(constInstance);
        }

        public static ConstFunction<T0, TR> CreateConstInstanceFunction<TInstance, T0, TR>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0));
            return info.CreateConstInstanceFunction<TInstance, T0, TR>(constInstance);
        }

        public static ConstFunction<T0, T1, TR> CreateConstInstanceFunction<TInstance, T0, T1, TR>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1));
            return info.CreateConstInstanceFunction<TInstance, T0, T1, TR>(constInstance);
        }

        public static ConstFunction<T0, T1, T2, TR> CreateConstInstanceFunction<TInstance, T0, T1, T2, TR>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateConstInstanceFunction<TInstance, T0, T1, T2, TR>(constInstance);
        }

        public static ConstFunction<T0, T1, T2, T3, TR> CreateConstInstanceFunction<TInstance, T0, T1, T2, T3, TR>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateConstInstanceFunction<TInstance, T0, T1, T2, T3, TR>(constInstance);
        }

        public static ConstFunction<T0, T1, T2, T3, T4, TR> CreateConstInstanceFunction<TInstance, T0, T1, T2, T3, T4, TR>(this TInstance constInstance, string methodName)
        {
            var info = GetInstanceMethodInfo<TInstance>(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateConstInstanceFunction<TInstance, T0, T1, T2, T3, T4, TR>(constInstance);
        }

        ////////// Const Instance Function X //////////
        public static ConstFunctionX CreateConstInstanceFunctionX(this object constInstance, string methodName, params Type[] argTypes)
        {
            AssertInstance(constInstance, out var instanceType, methodName, MemberType.Method);
            var info = instanceType.GetInstanceMethodInfo(methodName, argTypes);
            return info.CreateConstInstanceFunctionX(constInstance);
        }

        #endregion

        //////////////////////////////////////////////
        //////////     Static Functions     //////////
        //////////////////////////////////////////////

        #region Static Functions

        public static ConstFunction<TR> CreateStaticFunction<TR>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName);
            return info.CreateStaticFunction<TR>();
        }

        public static ConstFunction<T0, TR> CreateStaticFunction<T0, TR>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0));
            return info.CreateStaticFunction<T0, TR>();
        }

        public static ConstFunction<T0, T1, TR> CreateStaticFunction<T0, T1, TR>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1));
            return info.CreateStaticFunction<T0, T1, TR>();
        }

        public static ConstFunction<T0, T1, T2, TR> CreateStaticFunction<T0, T1, T2, TR>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2));
            return info.CreateStaticFunction<T0, T1, T2, TR>();
        }

        public static ConstFunction<T0, T1, T2, T3, TR> CreateStaticFunction<T0, T1, T2, T3, TR>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3));
            return info.CreateStaticFunction<T0, T1, T2, T3, TR>();
        }

        public static ConstFunction<T0, T1, T2, T3, T4, TR> CreateStaticFunction<T0, T1, T2, T3, T4, TR>(this Type targetType, string methodName)
        {
            var info = targetType.GetStaticMethodInfo(methodName, typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return info.CreateStaticFunction<T0, T1, T2, T3, T4, TR>();
        }

        public static ConstFunction<TR> CreateStaticFunction<TTarget, TR>(string methodName) => typeof(TTarget).CreateStaticFunction<TR>(methodName);

        public static ConstFunction<T0, TR> CreateStaticFunction<TTarget, T0, TR>(string methodName) => typeof(TTarget).CreateStaticFunction<T0, TR>(methodName);

        public static ConstFunction<T0, T1, TR> CreateStaticFunction<TTarget, T0, T1, TR>(string methodName) => typeof(TTarget).CreateStaticFunction<T0, T1, TR>(methodName);

        public static ConstFunction<T0, T1, T2, TR> CreateStaticFunction<TTarget, T0, T1, T2, TR>(string methodName) => typeof(TTarget).CreateStaticFunction<T0, T1, T2, TR>(methodName);

        public static ConstFunction<T0, T1, T2, T3, TR> CreateStaticFunction<TTarget, T0, T1, T2, T3, TR>(string methodName) => typeof(TTarget).CreateStaticFunction<T0, T1, T2, T3, TR>(methodName);

        public static ConstFunction<T0, T1, T2, T3, T4, TR> CreateStaticFunction<TTarget, T0, T1, T2, T3, T4, TR>(string methodName) => typeof(TTarget).CreateStaticFunction<T0, T1, T2, T3, T4, TR>(methodName);

        ////////// Static Function X //////////
        public static ConstFunctionX CreateStaticFunctionX(this Type targetType, string methodName, params Type[] argTypes)
        {
            var info = targetType.GetInstanceMethodInfo(methodName, argTypes);
            return info.CreateStaticFunctionX();
        }

        #endregion
    }
}