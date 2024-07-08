// ReSharper disable InconsistentNaming
namespace ReflectionExtensions
{
    public delegate object InstanceFunctionX(object instance, params object[] args);
    public delegate object InstanceFunctionT<in TInstance>(TInstance instance, params object[] args);
    public delegate TResult InstanceFunctionR<out TResult>(object instance, params object[] args);
    public delegate TResult InstanceFunctionTR<in TInstance, out TResult>(TInstance instance, params object[] args);

    public delegate object InstanceFunctionA<in T0>(object instance, T0 arg0);
    public delegate object InstanceFunctionA<in T0, in T1>(object instance, T0 arg0, T1 arg1);
    public delegate object InstanceFunctionA<in T0, in T1, in T2>(object instance, T0 arg0, T1 arg1, T2 arg2);
    public delegate object InstanceFunctionA<in T0, in T1, in T2, in T3>(object instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate object InstanceFunctionA<in T0, in T1, in T2, in T3, in T4>(object instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate object InstanceFunctionTA<in TInstance, in T0>(TInstance instance, T0 arg0);
    public delegate object InstanceFunctionTA<in TInstance, in T0, in T1>(TInstance instance, T0 arg0, T1 arg1);
    public delegate object InstanceFunctionTA<in TInstance, in T0, in T1, in T2>(TInstance instance, T0 arg0, T1 arg1, T2 arg2);
    public delegate object InstanceFunctionTA<in TInstance, in T0, in T1, in T2, in T3>(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate object InstanceFunctionTA<in TInstance, in T0, in T1, in T2, in T3, in T4>(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate TResult InstanceFunctionAR<in T0, out TResult>(object instance, T0 arg0);
    public delegate TResult InstanceFunctionAR<in T0, in T1, out TResult>(object instance, T0 arg0, T1 arg1);
    public delegate TResult InstanceFunctionAR<in T0, in T1, in T2, out TResult>(object instance, T0 arg0, T1 arg1, T2 arg2);
    public delegate TResult InstanceFunctionAR<in T0, in T1, in T2, in T3, out TResult>(object instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate TResult InstanceFunctionAR<in T0, in T1, in T2, in T3, in T4, out TResult>(object instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate TResult InstanceFunction<in TInstance, out TResult>(TInstance instance);
    public delegate TResult InstanceFunction<in TInstance, in T0, out TResult>(TInstance instance, T0 arg0);
    public delegate TResult InstanceFunction<in TInstance, in T0, in T1, out TResult>(TInstance instance, T0 arg0, T1 arg1);
    public delegate TResult InstanceFunction<in TInstance, in T0, in T1, in T2, out TResult>(TInstance instance, T0 arg0, T1 arg1, T2 arg2);
    public delegate TResult InstanceFunction<in TInstance, in T0, in T1, in T2, in T3, out TResult>(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate TResult InstanceFunction<in TInstance, in T0, in T1, in T2, in T3, in T4, out TResult>(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}