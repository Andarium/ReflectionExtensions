// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
using System;

namespace ReflectionExtensions
{
    public readonly struct ConstAccessor<TValue>
    {
        private readonly ConstGetter<TValue> _getter;
        private readonly ConstSetter<TValue> _setter;

        internal ConstAccessor(ConstGetter<TValue> getter, ConstSetter<TValue> setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue() => _getter.Invoke();
        public void SetValue(TValue value) => _setter.Invoke(value);

        public TValue Value
        {
            get => _getter.Invoke();
            set => _setter.Invoke(value);
        }
    }

    public readonly struct ConstAccessorX
    {
        private readonly ConstGetter _getter;
        private readonly ConstSetter _setter;

        internal ConstAccessorX(ConstGetter getter, ConstSetter setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public object GetValue() => _getter.Invoke();
        public void SetValue(object value) => _setter.Invoke(value);

        public object Value
        {
            get => _getter.Invoke();
            set => _setter.Invoke(value);
        }
    }

    public delegate object ConstGetter();

    public delegate TResult ConstGetter<out TResult>();

    public delegate void ConstSetter(object value);

    public delegate void ConstSetter<in TValue>(TValue value);
}