using System;

namespace ReflectionExtensions
{
    public readonly struct ConstructorX
    {
        private readonly Func<object[], object> _method;

        public ConstructorX(Func<object[], object> method)
        {
            _method = method;
        }

        public object Invoke(params object[] args) => _method.Invoke(args);
        public static implicit operator ConstructorX(Func<object[], object> m) => new(m);
        public static implicit operator Func<object[], object>(ConstructorX m) => m._method;
    }

    public readonly struct ConstructorX<TR>
    {
        private readonly Func<object[], TR> _method;

        public ConstructorX(Func<object[], TR> method)
        {
            _method = method;
        }

        public TR Invoke(params object[] args) => _method.Invoke(args);
        public static implicit operator ConstructorX<TR>(Func<object[], TR> m) => new(m);
        public static implicit operator Func<object[], TR>(ConstructorX<TR> m) => m._method;
    }

    public readonly struct Constructor<TTarget>
    {
        private readonly Func<TTarget> _method;

        public Constructor(Func<TTarget> method)
        {
            _method = method;
        }

        public TTarget Invoke() => _method.Invoke();
        public static implicit operator Constructor<TTarget>(Func<TTarget> m) => new(m);
        public static implicit operator Func<TTarget>(Constructor<TTarget> m) => m._method;
    }

    public readonly struct Constructor<TTarget, T0>
    {
        private readonly Func<T0, TTarget> _method;

        public Constructor(Func<T0, TTarget> method)
        {
            _method = method;
        }

        public TTarget Invoke(T0 arg0) => _method.Invoke(arg0);
        public static implicit operator Constructor<TTarget, T0>(Func<T0, TTarget> m) => new(m);
        public static implicit operator Func<T0, TTarget>(Constructor<TTarget, T0> m) => m._method;
    }

    public readonly struct Constructor<TTarget, T0, T1>
    {
        private readonly Func<T0, T1, TTarget> _method;

        public Constructor(Func<T0, T1, TTarget> method)
        {
            _method = method;
        }

        public TTarget Invoke(T0 arg0, T1 arg1) => _method.Invoke(arg0, arg1);
        public static implicit operator Constructor<TTarget, T0, T1>(Func<T0, T1, TTarget> m) => new(m);
        public static implicit operator Func<T0, T1, TTarget>(Constructor<TTarget, T0, T1> m) => m._method;
    }

    public readonly struct Constructor<TTarget, T0, T1, T2>
    {
        private readonly Func<T0, T1, T2, TTarget> _method;

        public Constructor(Func<T0, T1, T2, TTarget> method)
        {
            _method = method;
        }

        public TTarget Invoke(T0 arg0, T1 arg1, T2 arg2) => _method.Invoke(arg0, arg1, arg2);
        public static implicit operator Constructor<TTarget, T0, T1, T2>(Func<T0, T1, T2, TTarget> m) => new(m);
        public static implicit operator Func<T0, T1, T2, TTarget>(Constructor<TTarget, T0, T1, T2> m) => m._method;
    }

    public readonly struct Constructor<TTarget, T0, T1, T2, T3>
    {
        private readonly Func<T0, T1, T2, T3, TTarget> _method;

        public Constructor(Func<T0, T1, T2, T3, TTarget> method)
        {
            _method = method;
        }

        public TTarget Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3) => _method.Invoke(arg0, arg1, arg2, arg3);
        public static implicit operator Constructor<TTarget, T0, T1, T2, T3>(Func<T0, T1, T2, T3, TTarget> m) => new(m);
        public static implicit operator Func<T0, T1, T2, T3, TTarget>(Constructor<TTarget, T0, T1, T2, T3> m) => m._method;
    }

    public readonly struct Constructor<TTarget, T0, T1, T2, T3, T4>
    {
        private readonly Func<T0, T1, T2, T3, T4, TTarget> _method;

        public Constructor(Func<T0, T1, T2, T3, T4, TTarget> method)
        {
            _method = method;
        }

        public TTarget Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _method.Invoke(arg0, arg1, arg2, arg3, arg4);
        public static implicit operator Constructor<TTarget, T0, T1, T2, T3, T4>(Func<T0, T1, T2, T3, T4, TTarget> m) => new(m);
        public static implicit operator Func<T0, T1, T2, T3, T4, TTarget>(Constructor<TTarget, T0, T1, T2, T3, T4> m) => m._method;
    }
}