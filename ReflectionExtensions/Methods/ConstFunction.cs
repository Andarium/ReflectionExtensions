// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace ReflectionExtensions
{
    // public delegate object ConstFunction();
    public delegate object ConstFunctionX(params object[] args);
    public delegate TResult ConstFunctionR<out TResult>(params object[] args);

    public delegate object ConstFunctionA();
    public delegate object ConstFunctionA<in T0>(T0 arg0);
    public delegate object ConstFunctionA<in T0, in T1>(T0 arg0, T1 arg1);
    public delegate object ConstFunctionA<in T0, in T1, in T2>(T0 arg0, T1 arg1, T2 arg2);
    public delegate object ConstFunctionA<in T0, in T1, in T2, in T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate object ConstFunctionA<in T0, in T1, in T2, in T3, in T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate TResult ConstFunction<out TResult>();
    public delegate TResult ConstFunction<in T0, out TResult>(T0 arg0);
    public delegate TResult ConstFunction<in T0, in T1, out TResult>(T0 arg0, T1 arg1);
    public delegate TResult ConstFunction<in T0, in T1, in T2, out TResult>(T0 arg0, T1 arg1, T2 arg2);
    public delegate TResult ConstFunction<in T0, in T1, in T2, in T3, out TResult>(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate TResult ConstFunction<in T0, in T1, in T2, in T3, in T4, out TResult>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}