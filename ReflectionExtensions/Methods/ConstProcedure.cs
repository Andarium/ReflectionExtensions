// ReSharper disable once CheckNamespace
namespace ReflectionExtensions
{
    public delegate void ConstProcedureX(params object[] args);

    public delegate void ConstProcedure();
    public delegate void ConstProcedure<in T0>(T0 arg0);
    public delegate void ConstProcedure<in T0, in T1>(T0 arg0, T1 arg1);
    public delegate void ConstProcedure<in T0, in T1, in T2>(T0 arg0, T1 arg1, T2 arg2);
    public delegate void ConstProcedure<in T0, in T1, in T2, in T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate void ConstProcedure<in T0, in T1, in T2, in T3, in T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}