namespace ReflectionExtensions
{
    public delegate object InstanceFunctionX(object instance, params object[] args);

    public delegate TResult InstanceFunction<in TInstance, out TResult>(TInstance instance);

    public delegate TResult InstanceFunction<in TInstance, in T0, out TResult>(TInstance instance, T0 arg0);

    public delegate TResult InstanceFunction<in TInstance, in T0, in T1, out TResult>(TInstance instance, T0 arg0, T1 arg1);

    public delegate TResult InstanceFunction<in TInstance, in T0, in T1, in T2, out TResult>(TInstance instance, T0 arg0, T1 arg1, T2 arg2);

    public delegate TResult InstanceFunction<in TInstance, in T0, in T1, in T2, in T3, out TResult>(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3);

    public delegate TResult InstanceFunction<in TInstance, in T0, in T1, in T2, in T3, in T4, out TResult>(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}