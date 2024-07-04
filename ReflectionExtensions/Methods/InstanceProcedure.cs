using System;

namespace ReflectionExtensions
{
    public readonly struct InstanceProcedureX
    {
        private readonly Action<object, object[]> _method;

        public InstanceProcedureX(Action<object, object[]> method)
        {
            _method = method;
        }

        public void Invoke(object instance, params object[] args) => _method.Invoke(instance, args);
        public static implicit operator InstanceProcedureX(Action<object, object[]> m) => new(m);
        public static implicit operator Action<object, object[]>(InstanceProcedureX m) => m._method;
    }

    public readonly struct InstanceProcedure<TInstance>
    {
        private readonly Action<TInstance> _method;

        public InstanceProcedure(Action<TInstance> method)
        {
            _method = method;
        }

        public void Invoke(TInstance instance) => _method.Invoke(instance);
        public static implicit operator InstanceProcedure<TInstance>(Action<TInstance> m) => new(m);
        public static implicit operator Action<TInstance>(InstanceProcedure<TInstance> m) => m._method;
    }

    public readonly struct InstanceProcedure<TInstance, T0>
    {
        private readonly Action<TInstance, T0> _method;

        public InstanceProcedure(Action<TInstance, T0> method)
        {
            _method = method;
        }

        public void Invoke(TInstance instance, T0 arg0) => _method.Invoke(instance, arg0);
        public static implicit operator InstanceProcedure<TInstance, T0>(Action<TInstance, T0> m) => new(m);
        public static implicit operator Action<TInstance, T0>(InstanceProcedure<TInstance, T0> m) => m._method;
    }

    public readonly struct InstanceProcedure<TInstance, T0, T1>
    {
        private readonly Action<TInstance, T0, T1> _method;

        public InstanceProcedure(Action<TInstance, T0, T1> method)
        {
            _method = method;
        }

        public void Invoke(TInstance instance, T0 arg0, T1 arg1) => _method.Invoke(instance, arg0, arg1);
        public static implicit operator InstanceProcedure<TInstance, T0, T1>(Action<TInstance, T0, T1> m) => new(m);
        public static implicit operator Action<TInstance, T0, T1>(InstanceProcedure<TInstance, T0, T1> m) => m._method;
    }

    public readonly struct InstanceProcedure<TInstance, T0, T1, T2>
    {
        private readonly Action<TInstance, T0, T1, T2> _method;

        public InstanceProcedure(Action<TInstance, T0, T1, T2> method)
        {
            _method = method;
        }

        public void Invoke(TInstance instance, T0 arg0, T1 arg1, T2 arg2) => _method.Invoke(instance, arg0, arg1, arg2);
        public static implicit operator InstanceProcedure<TInstance, T0, T1, T2>(Action<TInstance, T0, T1, T2> m) => new(m);
        public static implicit operator Action<TInstance, T0, T1, T2>(InstanceProcedure<TInstance, T0, T1, T2> m) => m._method;
    }

    public readonly struct InstanceProcedure<TInstance, T0, T1, T2, T3>
    {
        private readonly Action<TInstance, T0, T1, T2, T3> _method;

        public InstanceProcedure(Action<TInstance, T0, T1, T2, T3> method)
        {
            _method = method;
        }

        public void Invoke(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3) => _method.Invoke(instance, arg0, arg1, arg2, arg3);
        public static implicit operator InstanceProcedure<TInstance, T0, T1, T2, T3>(Action<TInstance, T0, T1, T2, T3> m) => new(m);
        public static implicit operator Action<TInstance, T0, T1, T2, T3>(InstanceProcedure<TInstance, T0, T1, T2, T3> m) => m._method;
    }

    public readonly struct InstanceProcedure<TInstance, T0, T1, T2, T3, T4>
    {
        private readonly Action<TInstance, T0, T1, T2, T3, T4> _method;

        public InstanceProcedure(Action<TInstance, T0, T1, T2, T3, T4> method)
        {
            _method = method;
        }

        public void Invoke(TInstance instance, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => _method.Invoke(instance, arg0, arg1, arg2, arg3, arg4);
        public static implicit operator InstanceProcedure<TInstance, T0, T1, T2, T3, T4>(Action<TInstance, T0, T1, T2, T3, T4> m) => new(m);
        public static implicit operator Action<TInstance, T0, T1, T2, T3, T4>(InstanceProcedure<TInstance, T0, T1, T2, T3, T4> m) => m._method;
    }
}