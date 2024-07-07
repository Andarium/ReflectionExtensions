using System;

namespace ReflectionExtensions
{
    public readonly struct ConstAccessor<TValue>
    {
        internal readonly Func<TValue> Getter;
        internal readonly Action<TValue> Setter;

        internal ConstAccessor(Func<TValue> getter, Action<TValue> setter)
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

        public static implicit operator TValue(ConstAccessor<TValue> accessor) => accessor.Value;
    }

    public readonly struct ConstAccessorX
    {
        private readonly Func<object> _getter;
        private readonly Action<object> _setter;

        internal ConstAccessorX(Func<object> getter, Action<object> setter)
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

        public static implicit operator ConstAccessorX(ConstAccessor<object> a) => new(a.Getter, a.Setter);
    }
}