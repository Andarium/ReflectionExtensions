using System;

namespace ReflectionExtensions
{
    public readonly struct ConstFunctionX
    {
        private readonly Func<object[], object> _method;

        public ConstFunctionX(Func<object[], object> method)
        {
            _method = method;
        }

        public object Invoke(params object[] args) => _method.Invoke(args);
        public static implicit operator ConstFunctionX(Func<object[], object> m) => new(m);
        public static implicit operator Func<object[], object>(ConstFunctionX m) => m._method;
    }

    public readonly struct ConstFunctionX<TR>
    {
        private readonly Func<object[], TR> _method;

        public ConstFunctionX(Func<object[], TR> method)
        {
            _method = method;
        }

        public TR Invoke(params object[] args) => _method.Invoke(args);
        public static implicit operator ConstFunctionX<TR>(Func<object[], TR> m) => new(m);
        public static implicit operator Func<object[], TR>(ConstFunctionX<TR> m) => m._method;
    }

    public readonly struct ConstFunction<TResult>
    {
        private readonly Func<TResult> _method;

        public ConstFunction(Func<TResult> method)
        {
            _method = method;
        }

        public TResult Invoke() => _method.Invoke();
        public static implicit operator ConstFunction<TResult>(Func<TResult> m) => new(m);
        public static implicit operator Func<TResult>(ConstFunction<TResult> m) => m._method;
    }

    public readonly struct ConstFunction<T0, TResult>
    {
        private readonly Func<T0, TResult> _method;

        public ConstFunction(Func<T0, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(T0 arg0) => _method.Invoke(arg0);
        public static implicit operator ConstFunction<T0, TResult>(Func<T0, TResult> m) => new(m);
        public static implicit operator Func<T0, TResult>(ConstFunction<T0, TResult> m) => m._method;
    }

    public readonly struct ConstFunction<T0, T1, TResult>
    {
        private readonly Func<T0, T1, TResult> _method;

        public ConstFunction(Func<T0, T1, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(T0 arg0, T1 arg1) => _method.Invoke(arg0, arg1);
        public static implicit operator ConstFunction<T0, T1, TResult>(Func<T0, T1, TResult> m) => new(m);
        public static implicit operator Func<T0, T1, TResult>(ConstFunction<T0, T1, TResult> m) => m._method;
    }

    public readonly struct ConstFunction<T0, T1, T2, TResult>
    {
        private readonly Func<T0, T1, T2, TResult> _method;

        public ConstFunction(Func<T0, T1, T2, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(T0 arg0, T1 arg1, T2 arg2) => _method.Invoke(arg0, arg1, arg2);
        public static implicit operator ConstFunction<T0, T1, T2, TResult>(Func<T0, T1, T2, TResult> m) => new(m);
        public static implicit operator Func<T0, T1, T2, TResult>(ConstFunction<T0, T1, T2, TResult> m) => m._method;
    }

    public readonly struct ConstFunction<T0, T1, T2, T3, TResult>
    {
        private readonly Func<T0, T1, T2, T3, TResult> _method;

        public ConstFunction(Func<T0, T1, T2, T3, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3) => _method.Invoke(arg0, arg1, arg2, arg3);
        public static implicit operator ConstFunction<T0, T1, T2, T3, TResult>(Func<T0, T1, T2, T3, TResult> m) => new(m);
        public static implicit operator Func<T0, T1, T2, T3, TResult>(ConstFunction<T0, T1, T2, T3, TResult> m) => m._method;
    }

    public readonly struct ConstFunction<T0, T1, T2, T3, T4, TResult>
    {
        private readonly Func<T0, T1, T2, T3, T4, TResult> _method;

        public ConstFunction(Func<T0, T1, T2, T3, T4, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _method.Invoke(arg0, arg1, arg2, arg3, arg4);
        public static implicit operator ConstFunction<T0, T1, T2, T3, T4, TResult>(Func<T0, T1, T2, T3, T4, TResult> m) => new(m);
        public static implicit operator Func<T0, T1, T2, T3, T4, TResult>(ConstFunction<T0, T1, T2, T3, T4, TResult> m) => m._method;
    }
}