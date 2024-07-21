using System;

namespace ReflectionExtensions
{
    public static partial class DelegateExtensions
    {
        public static ConstProcedure WithFixedInstance<TInstance>(this InstanceProcedure<TInstance> call, Func<TInstance> instance)
        {
            return () => call.Invoke(instance.Invoke());
        }

        public static ConstProcedure<T0> WithFixedInstance<TInstance, T0>(this InstanceProcedure<TInstance, T0> call, Func<TInstance> instance)
        {
            return (arg0) => call.Invoke(instance.Invoke(), arg0);
        }

        public static ConstProcedure<T0, T1> WithFixedInstance<TInstance, T0, T1>(this InstanceProcedure<TInstance, T0, T1> call, Func<TInstance> instance)
        {
            return (arg0, arg1) => call.Invoke(instance.Invoke(), arg0, arg1);
        }

        public static ConstProcedure<T0, T1, T2> WithFixedInstance<TInstance, T0, T1, T2>(this InstanceProcedure<TInstance, T0, T1, T2> call, Func<TInstance> instance)
        {
            return (arg0, arg1, arg2) => call.Invoke(instance.Invoke(), arg0, arg1, arg2);
        }

        public static ConstProcedure<T0, T1, T2, T3> WithFixedInstance<TInstance, T0, T1, T2, T3>(this InstanceProcedure<TInstance, T0, T1, T2, T3> call, Func<TInstance> instance)
        {
            return (arg0, arg1, arg2, arg3) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3);
        }

        public static ConstProcedure<T0, T1, T2, T3, T4> WithFixedInstance<TInstance, T0, T1, T2, T3, T4>(this InstanceProcedure<TInstance, T0, T1, T2, T3, T4> call, Func<TInstance> instance)
        {
            return (arg0, arg1, arg2, arg3, arg4) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3, arg4);
        }

        // A

        public static ConstProcedure WithFixedInstance(this InstanceProcedureA call, Func<object> instance)
        {
            return () => call.Invoke(instance.Invoke());
        }

        public static ConstProcedure<T0> WithFixedInstance<T0>(this InstanceProcedureA<T0> call, Func<object> instance)
        {
            return (arg0) => call.Invoke(instance.Invoke(), arg0);
        }

        public static ConstProcedure<T0, T1> WithFixedInstance<T0, T1>(this InstanceProcedureA<T0, T1> call, Func<object> instance)
        {
            return (arg0, arg1) => call.Invoke(instance.Invoke(), arg0, arg1);
        }

        public static ConstProcedure<T0, T1, T2> WithFixedInstance<T0, T1, T2>(this InstanceProcedureA<T0, T1, T2> call, Func<object> instance)
        {
            return (arg0, arg1, arg2) => call.Invoke(instance.Invoke(), arg0, arg1, arg2);
        }

        public static ConstProcedure<T0, T1, T2, T3> WithFixedInstance<T0, T1, T2, T3>(this InstanceProcedureA<T0, T1, T2, T3> call, Func<object> instance)
        {
            return (arg0, arg1, arg2, arg3) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3);
        }

        public static ConstProcedure<T0, T1, T2, T3, T4> WithFixedInstance<T0, T1, T2, T3, T4>(this InstanceProcedureA<T0, T1, T2, T3, T4> call, Func<object> instance)
        {
            return (arg0, arg1, arg2, arg3, arg4) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3, arg4);
        }

        // T

        public static ConstProcedureX WithFixedInstance<TInstance>(this InstanceProcedureT<TInstance> call, Func<TInstance> instance)
        {
            return args => call.Invoke(instance.Invoke(), args);
        }

        // X

        public static ConstProcedureX WithFixedInstance(this InstanceProcedureX call, Func<object> instance)
        {
            return args => call.Invoke(instance.Invoke(), args);
        }
    }
}