using System;

namespace ReflectionExtensions
{
    public readonly struct ConstProcedureX
    {
        private readonly Action<object[]> _method;

        public ConstProcedureX(Action<object[]> method)
        {
            _method = method;
        }

        public void Invoke(params object[] args) => _method.Invoke(args);
        public static implicit operator ConstProcedureX(Action<object[]> m) => new(m);
        public static implicit operator Action<object[]>(ConstProcedureX m) => m._method;
    }

    public readonly struct ConstProcedure
    {
        private readonly Action _method;

        public ConstProcedure(Action method)
        {
            _method = method;
        }

        public void Invoke() => _method.Invoke();
        public static implicit operator ConstProcedure(Action m) => new(m);
        public static implicit operator Action(ConstProcedure m) => m._method;
    }

    public readonly struct ConstProcedure<T0>
    {
        private readonly Action<T0> _method;

        public ConstProcedure(Action<T0> method)
        {
            _method = method;
        }

        public void Invoke(T0 arg0) => _method.Invoke(arg0);
        public static implicit operator ConstProcedure<T0>(Action<T0> m) => new(m);
        public static implicit operator Action<T0>(ConstProcedure<T0> m) => m._method;
    }

    public readonly struct ConstProcedure<T0, T1>
    {
        private readonly Action<T0, T1> _method;

        public ConstProcedure(Action<T0, T1> method)
        {
            _method = method;
        }

        public void Invoke(T0 arg0, T1 arg1) => _method.Invoke(arg0, arg1);
        public static implicit operator ConstProcedure<T0, T1>(Action<T0, T1> m) => new(m);
        public static implicit operator Action<T0, T1>(ConstProcedure<T0, T1> m) => m._method;
    }

    public readonly struct ConstProcedure<T0, T1, T2>
    {
        private readonly Action<T0, T1, T2> _method;

        public ConstProcedure(Action<T0, T1, T2> method)
        {
            _method = method;
        }

        public void Invoke(T0 arg0, T1 arg1, T2 arg2) => _method.Invoke(arg0, arg1, arg2);
        public static implicit operator ConstProcedure<T0, T1, T2>(Action<T0, T1, T2> m) => new(m);
        public static implicit operator Action<T0, T1, T2>(ConstProcedure<T0, T1, T2> m) => m._method;
    }

    public readonly struct ConstProcedure<T0, T1, T2, T3>
    {
        private readonly Action<T0, T1, T2, T3> _method;

        public ConstProcedure(Action<T0, T1, T2, T3> method)
        {
            _method = method;
        }

        public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3) => _method.Invoke(arg0, arg1, arg2, arg3);
        public static implicit operator ConstProcedure<T0, T1, T2, T3>(Action<T0, T1, T2, T3> m) => new(m);
        public static implicit operator Action<T0, T1, T2, T3>(ConstProcedure<T0, T1, T2, T3> m) => m._method;
    }

    public readonly struct ConstProcedure<T0, T1, T2, T3, T4>
    {
        private readonly Action<T0, T1, T2, T3, T4> _method;

        public ConstProcedure(Action<T0, T1, T2, T3, T4> method)
        {
            _method = method;
        }

        public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _method.Invoke(arg0, arg1, arg2, arg3, arg4);
        public static implicit operator ConstProcedure<T0, T1, T2, T3, T4>(Action<T0, T1, T2, T3, T4> m) => new(m);
        public static implicit operator Action<T0, T1, T2, T3, T4>(ConstProcedure<T0, T1, T2, T3, T4> m) => m._method;
    }
}