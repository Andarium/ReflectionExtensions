// ReSharper disable InconsistentNaming

using System;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        //////////////////////////////////////////////////////
        //////////     Instance Procedures T/R/X    //////////
        //////////////////////////////////////////////////////

        public static InstanceProcedureT<TInstance> CreateInstanceProcedureT<TInstance>(string methodName, params Type[] argTypes)
        {
            return GetInstanceMethodInfo<TInstance>(methodName, argTypes).CreateInstanceProcedureT<TInstance>();
        }

        public static InstanceProcedureX CreateInstanceProcedureX(this Type instanceType, string methodName, params Type[] argTypes)
        {
            return instanceType.GetInstanceMethodInfo(methodName, argTypes).CreateInstanceProcedureX();
        }

        ////////////////////////////////////////////////////////////
        //////////     Const Instance Procedures T/R/X    //////////
        ////////////////////////////////////////////////////////////

        public static ConstProcedureX CreateConstInstanceProcedure(this object constInstance, string methodName, params Type[] argTypes)
        {
            AssertInstance(constInstance, out var instanceType, methodName, MemberType.Method);
            return instanceType.GetInstanceMethodInfo(methodName, argTypes).CreateConstInstanceProcedureX(constInstance);
        }

        ////////////////////////////////////////////////////
        //////////     Static Procedures T/R/X    //////////
        ////////////////////////////////////////////////////

        public static ConstProcedureX CreateStaticProcedureT<TTarget>(string methodName, params Type[] argTypes)
        {
            return GetInstanceMethodInfo<TTarget>(methodName, argTypes).CreateStaticProcedureX(argTypes);
        }

        public static ConstProcedureX CreateStaticProcedureX(this Type targetType, string methodName, params Type[] argTypes)
        {
            return targetType.GetInstanceMethodInfo(methodName, argTypes).CreateStaticProcedureX(argTypes);
        }

        /////////////////////////////////////////////////////
        //////////     Instance Functions T/R/X    //////////
        /////////////////////////////////////////////////////

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

        ///////////////////////////////////////////////////////////
        //////////     Const Instance Functions T/R/X    //////////
        ///////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////
        //////////     Static Functions T/R/X    ///////////
        ////////////////////////////////////////////////////

        public static ConstFunctionX CreateStaticFunctionT<TTarget>(string methodName, params Type[] argTypes)
        {
            return GetInstanceMethodInfo<TTarget>(methodName, argTypes).CreateStaticFunctionX();
        }

        public static ConstFunctionR<TResult> CreateStaticFunctionR<TResult>(this Type targetType, string methodName, params Type[] argTypes)
        {
            return targetType.GetInstanceMethodInfo(methodName, argTypes).CreateStaticFunctionR<TResult>();
        }

        public static ConstFunctionR<TResult> CreateStaticFunctionTR<TTarget, TResult>(string methodName, params Type[] argTypes)
        {
            return GetInstanceMethodInfo<TTarget>(methodName, argTypes).CreateStaticFunctionR<TResult>();
        }

        public static ConstFunctionX CreateStaticFunctionX(this Type targetType, string methodName, params Type[] argTypes)
        {
            return targetType.GetInstanceMethodInfo(methodName, argTypes).CreateStaticFunctionX();
        }
    }
}