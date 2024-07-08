using System;

namespace ReflectionExtensions
{
    public readonly struct InstanceAccessor<TInstance, TValue>
    {
        internal readonly InstanceGetter<TInstance, TValue> Getter;
        internal readonly InstanceSetter<TInstance, TValue> Setter;

        internal InstanceAccessor(InstanceGetter<TInstance, TValue> getter, InstanceSetter<TInstance, TValue> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue(TInstance instance) => Getter.Invoke(instance);
        public void SetValue(TInstance instance, TValue value) => Setter.Invoke(instance, value);
    }

    public readonly struct InstanceAccessorT<TTarget>
    {
        internal readonly InstanceGetterT<TTarget> Getter;
        internal readonly InstanceSetterT<TTarget> Setter;

        internal InstanceAccessorT(InstanceGetterT<TTarget> getter, InstanceSetterT<TTarget> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public object GetValue(TTarget instance) => Getter.Invoke(instance);
        public void SetValue(TTarget instance, object value) => Setter.Invoke(instance, value);
    }

    public readonly struct InstanceAccessorR<TValue>
    {
        internal readonly InstanceGetter<TValue> Getter;
        internal readonly InstanceSetter<TValue> Setter;

        internal InstanceAccessorR(InstanceGetter<TValue> getter, InstanceSetter<TValue> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue(object instance) => Getter.Invoke(instance);
        public void SetValue(object instance, TValue value) => Setter.Invoke(instance, value);
    }

    public readonly struct InstanceAccessorX
    {
        internal readonly InstanceGetter Getter;
        internal readonly InstanceSetter Setter;

        internal InstanceAccessorX(InstanceGetter getter, InstanceSetter setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public object GetValue(object instance) => Getter.Invoke(instance);
        public void SetValue(object instance, object value) => Setter.Invoke(instance, value);
    }

    public delegate object InstanceGetter(object instance);

    public delegate object InstanceGetterT<in TInstance>(TInstance instance);

    public delegate TResult InstanceGetter<in TInstance, out TResult>(TInstance instance);

    public delegate TResult InstanceGetter<out TResult>(object instance);

    public delegate void InstanceSetter(object instance, object value);

    public delegate void InstanceSetterT<in TInstance>(TInstance instance, object value);

    public delegate void InstanceSetter<in TInstance, in TValue>(TInstance instance, TValue value);

    public delegate void InstanceSetter<in TValue>(object instance, TValue value);
}