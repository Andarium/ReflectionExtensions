// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
using System;

namespace ReflectionExtensions
{
    public readonly struct InstanceAccessor<TInstance, TValue>
    {
        private readonly InstanceGetter<TInstance, TValue> _getter;
        private readonly InstanceSetter<TInstance, TValue> _setter;

        internal InstanceAccessor(InstanceGetter<TInstance, TValue> getter, InstanceSetter<TInstance, TValue> setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue(TInstance instance) => _getter.Invoke(instance);
        public void SetValue(TInstance instance, TValue value) => _setter.Invoke(instance, value);
    }

    public readonly struct InstanceAccessorT<TTarget>
    {
        private readonly InstanceGetterT<TTarget> _getter;
        private readonly InstanceSetterT<TTarget> _setter;

        internal InstanceAccessorT(InstanceGetterT<TTarget> getter, InstanceSetterT<TTarget> setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public object GetValue(TTarget instance) => _getter.Invoke(instance);
        public void SetValue(TTarget instance, object value) => _setter.Invoke(instance, value);
    }

    public readonly struct InstanceAccessor<TValue>
    {
        private readonly InstanceGetter<TValue> _getter;
        private readonly InstanceSetter<TValue> _setter;

        internal InstanceAccessor(InstanceGetter<TValue> getter, InstanceSetter<TValue> setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue(object instance) => _getter.Invoke(instance);
        public void SetValue(object instance, TValue value) => _setter.Invoke(instance, value);
    }

    public readonly struct InstanceAccessor
    {
        private readonly InstanceGetter _getter;
        private readonly InstanceSetter _setter;

        internal InstanceAccessor(InstanceGetter getter, InstanceSetter setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public object GetValue(object instance) => _getter.Invoke(instance);
        public void SetValue(object instance, object value) => _setter.Invoke(instance, value);
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