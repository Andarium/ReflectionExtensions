using System;

namespace ReflectionExtensions
{
    public readonly struct ConstAccessor<TValue>
    {
        internal readonly ConstGetter<TValue> Getter;
        internal readonly ConstSetter<TValue> Setter;

        internal ConstAccessor(ConstGetter<TValue> getter, ConstSetter<TValue> setter)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public TValue GetValue() => Getter.Invoke();
        public void SetValue(TValue value) => Setter.Invoke(value);

        public TValue Value
        {
            get => Getter.Invoke();
            set => Setter.Invoke(value);
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