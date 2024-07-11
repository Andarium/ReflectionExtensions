// ReSharper disable InconsistentNaming
namespace ReflectionExtensions
{
    public delegate void InstanceProcedureX(object instance, params object[] args);
    public delegate void InstanceProcedureT<in TInstance>(TInstance instance, params object[] args);

    public delegate void InstanceProcedureA(object instance);
    public delegate void InstanceProcedureA<in T0>(object instance, T0 arg0);
    public delegate void InstanceProcedureA<in T0, in T1>(object instance, T0 arg0, T1 arg1);
    public delegate void InstanceProcedureA<in T0, in T1, in T2>(object instance, T0 arg0, T1 arg1, T2 arg2);
    public delegate void InstanceProcedureA<in T0, in T1, in T2, in T3>(object instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate void InstanceProcedureA<in T0, in T1, in T2, in T3, in T4>(object instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate void InstanceProcedure<in TInstance>(TInstance instance);
    public delegate void InstanceProcedure<in TInstance, in T0>(TInstance instance, T0 arg0);
    public delegate void InstanceProcedure<in TInstance, in T0, in T1>(TInstance instance, T0 arg0, T1 arg1);
    public delegate void InstanceProcedure<in TInstance, in T0, in T1, in T2>(TInstance instance, T0 arg0, T1 arg1, T2 arg2);
    public delegate void InstanceProcedure<in TInstance, in T0, in T1, in T2, in T3>(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate void InstanceProcedure<in TInstance, in T0, in T1, in T2, in T3, in T4>(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}