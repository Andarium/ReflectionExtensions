// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Linq;

namespace ReflectionExtensions
{
    public readonly struct Args
    {
        private readonly object[] _argValues;
        private readonly Type[] _argTypes;

        public object[] Values => _argValues ?? Array.Empty<object>();
        public Type[] Types => _argTypes ?? Array.Empty<Type>();

        public Args(object[] argValues, Type[] argTypes)
        {
            _argValues = argValues;
            _argTypes = argTypes;
        }

        public Args(params object[] argValues) : this()
        {
            _argValues = argValues;
            _argTypes = argValues.Select(x => x.GetType()).ToArray();
        }

        public Args(params Arg[] args) : this()
        {
            _argValues = new object[args.Length];
            _argTypes = new Type[args.Length];

            for (var i = 0; i < args.Length; i++)
            {
                _argValues[i] = args[i].Value;
                _argTypes[i] = args[i].Type;
            }

            _argValues = args.Select(x => x.Value).ToArray();
            _argTypes = args.Select(x => x.Type).ToArray();
        }

        public static Args Of(object[] argsValues, Type[] argTypes) => new(argsValues, argTypes);
        public static Args Of(params object[] argsValues) => new(argsValues);
        public static Args Of(params Arg[] args) => new(args);
        public static Args Of() => default;
    }

    public readonly struct Arg
    {
        public readonly object Value;
        public readonly Type Type;

        public Arg(object value, Type type)
        {
            Value = value;
            Type = type;
        }

        public static Arg Of(object value, Type type)
        {
            return new Arg(value, type);
        }
    }
}