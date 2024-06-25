using System;

namespace ReflectionExtensions
{
    public sealed class ConstAccessor<TValue>
    {
        private readonly Func<TValue> _getter;
        private readonly Action<TValue> _setter;

        internal ConstAccessor(Func<TValue> getter, Action<TValue> setter)
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

        public static implicit operator TValue(ConstAccessor<TValue> accessor) => accessor.Value;
    }
}