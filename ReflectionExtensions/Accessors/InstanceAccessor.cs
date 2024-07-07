using System;

namespace ReflectionExtensions
{
    public readonly struct InstanceAccessor<TInstance, TValue>
    {
        internal readonly Func<TInstance, TValue> Getter;
        internal readonly Action<TInstance, TValue> Setter;

        internal InstanceAccessor(Func<TInstance, TValue> getter, Action<TInstance, TValue> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue(TInstance instance) => Getter.Invoke(instance);
        public void SetValue(TInstance instance, TValue value) => Setter.Invoke(instance, value);
    }

    public readonly struct InstanceAccessorT<TTarget>
    {
        internal readonly Func<TTarget, object> Getter;
        internal readonly Action<TTarget, object> Setter;

        internal InstanceAccessorT(Func<TTarget, object> getter, Action<TTarget, object> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public object GetValue(TTarget instance) => Getter.Invoke(instance);
        public void SetValue(TTarget instance, object value) => Setter.Invoke(instance, value);

        public static implicit operator InstanceAccessorT<TTarget>(InstanceAccessor<TTarget, object> a) => new(a.Getter, a.Setter);
    }

    public readonly struct InstanceAccessorR<TValue>
    {
        internal readonly Func<object, TValue> Getter;
        internal readonly Action<object, TValue> Setter;

        internal InstanceAccessorR(Func<object, TValue> getter, Action<object, TValue> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue(object instance) => Getter.Invoke(instance);
        public void SetValue(object instance, TValue value) => Setter.Invoke(instance, value);

        public static implicit operator InstanceAccessorR<TValue>(InstanceAccessor<object, TValue> a) => new(a.Getter, a.Setter);
    }

    public readonly struct InstanceAccessorX
    {
        internal readonly Func<object, object> Getter;
        internal readonly Action<object, object> Setter;

        internal InstanceAccessorX(Func<object, object> getter, Action<object, object> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public object GetValue(object instance) => Getter.Invoke(instance);
        public void SetValue(object instance, object value) => Setter.Invoke(instance, value);

        public static implicit operator InstanceAccessorX(InstanceAccessorR<object> a) => new(a.Getter, a.Setter);
        public static implicit operator InstanceAccessorR<object>(InstanceAccessorX a) => new(a.Getter, a.Setter);
        public static implicit operator InstanceAccessorX(InstanceAccessorT<object> a) => new(a.Getter, a.Setter);
        public static implicit operator InstanceAccessorT<object>(InstanceAccessorX a) => new(a.Getter, a.Setter);
        public static implicit operator InstanceAccessorX(InstanceAccessor<object, object> a) => new(a.Getter, a.Setter);
    }
}