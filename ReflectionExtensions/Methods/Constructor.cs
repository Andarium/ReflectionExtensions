﻿// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace ReflectionExtensions
{
    public delegate object Constructor(params object[] args);
    public delegate TTarget ConstructorT<out TTarget>(params object[] args);

    public delegate object ConstructorA();
    public delegate object ConstructorA<in T0>(T0 arg0);
    public delegate object ConstructorA<in T0, in T1>(T0 arg0, T1 arg1);
    public delegate object ConstructorA<in T0, in T1, in T2>(T0 arg0, T1 arg1, T2 arg2);
    public delegate object ConstructorA<in T0, in T1, in T2, in T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate object ConstructorA<in T0, in T1, in T2, in T3, in T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate TTarget Constructor<out TTarget>();
    public delegate TTarget Constructor<out TTarget, in T0>(T0 arg0);
    public delegate TTarget Constructor<out TTarget, in T0, in T1>(T0 arg0, T1 arg1);
    public delegate TTarget Constructor<out TTarget, in T0, in T1, in T2>(T0 arg0, T1 arg1, T2 arg2);
    public delegate TTarget Constructor<out TTarget, in T0, in T1, in T2, in T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate TTarget Constructor<out TTarget, in T0, in T1, in T2, in T3, in T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}