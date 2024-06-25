using System;

namespace ReflectionExtensions
{
    public class InstanceAccessor<TInstance, TValue>
    {
        private readonly Func<TInstance, TValue> _getter;
        private readonly Action<TInstance, TValue> _setter;

        internal InstanceAccessor(Func<TInstance, TValue> getter, Action<TInstance, TValue> setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue(TInstance instance) => _getter.Invoke(instance);
        public void SetValue(TInstance instance, TValue value) => _setter.Invoke(instance, value);
    }

    public sealed class InstanceAccessor<TValue>
    {
        private readonly Func<object, TValue> _getter;
        private readonly Action<object, TValue> _setter;

        internal InstanceAccessor(Func<object, TValue> getter, Action<object, TValue> setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue(object instance) => _getter.Invoke(instance);
        public void SetValue(object instance, TValue value) => _setter.Invoke(instance, value);
    }
}