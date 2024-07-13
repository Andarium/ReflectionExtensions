using System;

namespace ReflectionExtensions
{
    public static partial class DelegateExtensions
    {
        public static ConstFunction<TResult> WithFixedInstance<TInstance, TResult>(this InstanceFunction<TInstance, TResult> call, Func<TInstance> instance)
        {
            return () => call.Invoke(instance.Invoke());
        }

        public static ConstFunction<T0, TResult> WithFixedInstance<TInstance, T0, TResult>(this InstanceFunction<TInstance, T0, TResult> call, Func<TInstance> instance)
        {
            return (arg0) => call.Invoke(instance.Invoke(), arg0);
        }

        public static ConstFunction<T0, T1, TResult> WithFixedInstance<TInstance, T0, T1, TResult>(this InstanceFunction<TInstance, T0, T1, TResult> call, Func<TInstance> instance)
        {
            return (arg0, arg1) => call.Invoke(instance.Invoke(), arg0, arg1);
        }

        public static ConstFunction<T0, T1, T2, TResult> WithFixedInstance<TInstance, T0, T1, T2, TResult>(this InstanceFunction<TInstance, T0, T1, T2, TResult> call, Func<TInstance> instance)
        {
            return (arg0, arg1, arg2) => call.Invoke(instance.Invoke(), arg0, arg1, arg2);
        }

        public static ConstFunction<T0, T1, T2, T3, TResult> WithFixedInstance<TInstance, T0, T1, T2, T3, TResult>(this InstanceFunction<TInstance, T0, T1, T2, T3, TResult> call, Func<TInstance> instance)
        {
            return (arg0, arg1, arg2, arg3) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3);
        }

        public static ConstFunction<T0, T1, T2, T3, T4, TResult> WithFixedInstance<TInstance, T0, T1, T2, T3, T4, TResult>(this InstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult> call, Func<TInstance> instance)
        {
            return (arg0, arg1, arg2, arg3, arg4) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3, arg4);
        }

        // AR

        public static ConstFunction<TResult> WithFixedInstance<TResult>(this InstanceFunctionAR<TResult> call, Func<object> instance)
        {
            return () => call.Invoke(instance.Invoke());
        }

        public static ConstFunction<T0, TResult> WithFixedInstance<T0, TResult>(this InstanceFunctionAR<T0, TResult> call, Func<object> instance)
        {
            return (arg0) => call.Invoke(instance.Invoke(), arg0);
        }

        public static ConstFunction<T0, T1, TResult> WithFixedInstance<T0, T1, TResult>(this InstanceFunctionAR<T0, T1, TResult> call, Func<object> instance)
        {
            return (arg0, arg1) => call.Invoke(instance.Invoke(), arg0, arg1);
        }

        public static ConstFunction<T0, T1, T2, TResult> WithFixedInstance<T0, T1, T2, TResult>(this InstanceFunctionAR<T0, T1, T2, TResult> call, Func<object> instance)
        {
            return (arg0, arg1, arg2) => call.Invoke(instance.Invoke(), arg0, arg1, arg2);
        }

        public static ConstFunction<T0, T1, T2, T3, TResult> WithFixedInstance<T0, T1, T2, T3, TResult>(this InstanceFunctionAR<T0, T1, T2, T3, TResult> call, Func<object> instance)
        {
            return (arg0, arg1, arg2, arg3) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3);
        }

        public static ConstFunction<T0, T1, T2, T3, T4, TResult> WithFixedInstance<T0, T1, T2, T3, T4, TResult>(this InstanceFunctionAR<T0, T1, T2, T3, T4, TResult> call, Func<object> instance)
        {
            return (arg0, arg1, arg2, arg3, arg4) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3, arg4);
        }

        // A

        public static ConstFunctionA WithFixedInstance(this InstanceFunctionA call, Func<object> instance)
        {
            return () => call.Invoke(instance.Invoke());
        }

        public static ConstFunctionA<T0> WithFixedInstance<T0>(this InstanceFunctionA<T0> call, Func<object> instance)
        {
            return (arg0) => call.Invoke(instance.Invoke(), arg0);
        }

        public static ConstFunctionA<T0, T1> WithFixedInstance<T0, T1>(this InstanceFunctionA<T0, T1> call, Func<object> instance)
        {
            return (arg0, arg1) => call.Invoke(instance.Invoke(), arg0, arg1);
        }

        public static ConstFunctionA<T0, T1, T2> WithFixedInstance<T0, T1, T2>(this InstanceFunctionA<T0, T1, T2> call, Func<object> instance)
        {
            return (arg0, arg1, arg2) => call.Invoke(instance.Invoke(), arg0, arg1, arg2);
        }

        public static ConstFunctionA<T0, T1, T2, T3> WithFixedInstance<T0, T1, T2, T3>(this InstanceFunctionA<T0, T1, T2, T3> call, Func<object> instance)
        {
            return (arg0, arg1, arg2, arg3) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3);
        }

        public static ConstFunctionA<T0, T1, T2, T3, T4> WithFixedInstance<T0, T1, T2, T3, T4>(this InstanceFunctionA<T0, T1, T2, T3, T4> call, Func<object> instance)
        {
            return (arg0, arg1, arg2, arg3, arg4) => call.Invoke(instance.Invoke(), arg0, arg1, arg2, arg3, arg4);
        }

        // TResult

        public static ConstFunctionX WithFixedInstance<TInstance>(this InstanceFunctionT<TInstance> call, Func<TInstance> instance)
        {
            return args => call.Invoke(instance.Invoke(), args);
        }

        // R

        public static ConstFunctionR<TResult> WithFixedInstance<TResult>(this InstanceFunctionR<TResult> call, Func<object> instance)
        {
            return args => call.Invoke(instance.Invoke(), args);
        }

        // X

        public static ConstFunctionX WithFixedInstance<TResult>(this InstanceFunctionX call, Func<object> instance)
        {
            return args => call.Invoke(instance.Invoke(), args);
        }
    }
}