using System;
using System.Linq;

namespace ReflectionExtensions
{
    public readonly struct Args
    {
        private readonly object[] _argsValues;
        private readonly Type[] _argTypes;

        public object[] Values => _argsValues ?? Array.Empty<object>();
        public Type[] Types => _argTypes ?? Array.Empty<Type>();

        public Args(object[] argsValues, Type[] argTypes)
        {
            _argsValues = argsValues;
            _argTypes = argTypes;
        }

        public Args(params object[] argsValues) : this()
        {
            _argsValues = argsValues;
            _argTypes = argsValues.Select(x => x.GetType()).ToArray();
        }

        public static Args Of(object[] argsValues, Type[] argTypes) => new(argsValues, argTypes);
        public static Args Of(params object[] argsValues) => new(argsValues);
    }
}