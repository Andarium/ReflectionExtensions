using System;

namespace ReflectionExtensions
{
    public readonly struct InstanceFunctionX
    {
        private readonly Func<object, object[], object> _method;

        public InstanceFunctionX(Func<object, object[], object> method)
        {
            _method = method;
        }

        public object Invoke(object instance, params object[] args) => _method.Invoke(instance, args);
        public static implicit operator InstanceFunctionX(Func<object, object[], object> m) => new(m);
        public static implicit operator Func<object, object[], object>(InstanceFunctionX m) => m._method;
    }

    public readonly struct InstanceFunction<TInstance, TResult>
    {
        private readonly Func<TInstance, TResult> _method;

        public InstanceFunction(Func<TInstance, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(TInstance instance) => _method.Invoke(instance);
        public static implicit operator InstanceFunction<TInstance, TResult>(Func<TInstance, TResult> m) => new(m);
        public static implicit operator Func<TInstance, TResult>(InstanceFunction<TInstance, TResult> m) => m._method;
    }

    public readonly struct InstanceFunction<TInstance, T0, TResult>
    {
        private readonly Func<TInstance, T0, TResult> _method;

        public InstanceFunction(Func<TInstance, T0, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(TInstance instance, T0 arg0) => _method.Invoke(instance, arg0);
        public static implicit operator InstanceFunction<TInstance, T0, TResult>(Func<TInstance, T0, TResult> m) => new(m);
        public static implicit operator Func<TInstance, T0, TResult>(InstanceFunction<TInstance, T0, TResult> m) => m._method;
    }

    public readonly struct InstanceFunction<TInstance, T0, T1, TResult>
    {
        private readonly Func<TInstance, T0, T1, TResult> _method;

        public InstanceFunction(Func<TInstance, T0, T1, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(TInstance instance, T0 arg0, T1 arg1) => _method.Invoke(instance, arg0, arg1);
        public static implicit operator InstanceFunction<TInstance, T0, T1, TResult>(Func<TInstance, T0, T1, TResult> m) => new(m);
        public static implicit operator Func<TInstance, T0, T1, TResult>(InstanceFunction<TInstance, T0, T1, TResult> m) => m._method;
    }

    public readonly struct InstanceFunction<TInstance, T0, T1, T2, TResult>
    {
        private readonly Func<TInstance, T0, T1, T2, TResult> _method;

        public InstanceFunction(Func<TInstance, T0, T1, T2, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(TInstance instance, T0 arg0, T1 arg1, T2 arg2) => _method.Invoke(instance, arg0, arg1, arg2);
        public static implicit operator InstanceFunction<TInstance, T0, T1, T2, TResult>(Func<TInstance, T0, T1, T2, TResult> m) => new(m);
        public static implicit operator Func<TInstance, T0, T1, T2, TResult>(InstanceFunction<TInstance, T0, T1, T2, TResult> m) => m._method;
    }

    public readonly struct InstanceFunction<TInstance, T0, T1, T2, T3, TResult>
    {
        private readonly Func<TInstance, T0, T1, T2, T3, TResult> _method;

        public InstanceFunction(Func<TInstance, T0, T1, T2, T3, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3) => _method.Invoke(instance, arg0, arg1, arg2, arg3);
        public static implicit operator InstanceFunction<TInstance, T0, T1, T2, T3, TResult>(Func<TInstance, T0, T1, T2, T3, TResult> m) => new(m);
        public static implicit operator Func<TInstance, T0, T1, T2, T3, TResult>(InstanceFunction<TInstance, T0, T1, T2, T3, TResult> m) => m._method;
    }

    public readonly struct InstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult>
    {
        private readonly Func<TInstance, T0, T1, T2, T3, T4, TResult> _method;

        public InstanceFunction(Func<TInstance, T0, T1, T2, T3, T4, TResult> method)
        {
            _method = method;
        }

        public TResult Invoke(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _method.Invoke(instance, arg0, arg1, arg2, arg3, arg4);
        public static implicit operator InstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult>(Func<TInstance, T0, T1, T2, T3, T4, TResult> m) => new(m);
        public static implicit operator Func<TInstance, T0, T1, T2, T3, T4, TResult>(InstanceFunction<TInstance, T0, T1, T2, T3, T4, TResult> m) => m._method;
    }
}