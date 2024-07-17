using System;
using System.Linq;

namespace ReflectionExtensions.Tests.Generators;

public static class PermutationUtil
{
    private static readonly bool[] Bools = [false, true];
    private static readonly Type[] Types = [typeof(int), typeof(string)];
    // private static readonly StaticAccessorType[] StaticAccessorTypes = Enum.GetValues(typeof(StaticAccessorType)).Cast<StaticAccessorType>().ToArray();

    public static void Permutate<T1>(Action<T1> call, Params parameters = default)
    {
        var options = GetOptions<T1>();
        for (var i = 0; i < options.Length; i++)
        {
            var last = i == options.Length - 1;
            var option = options[i];
            call.Invoke(option);
            if (!last || !parameters.SkipLastIteration)
            {
                parameters.AfterIteration?.Invoke();
            }
        }
    }

    public static void Permutate<T1, T2>(Action<T1, T2> call, Params parameters = default)
    {
        if (parameters.LastToFirst)
        {
            var options = GetOptions<T2>();
            for (var i = 0; i < options.Length; i++)
            {
                var o2 = options[i];
                var last = i == options.Length - 1;
                Permutate<T1>(o1 => call.Invoke(o1, o2), parameters.ToSub(true));
                if (!last || !parameters.SkipLastIteration)
                {
                    parameters.AfterIteration?.Invoke();
                }
            }
        }
        else
        {
            var options = GetOptions<T1>();
            for (var i = 0; i < options.Length; i++)
            {
                var o1 = options[i];
                var last = i == options.Length - 1;
                Permutate<T2>(o2 => call.Invoke(o1, o2), parameters.ToSub(true));
                if (!last || !parameters.SkipLastIteration)
                {
                    parameters.AfterIteration?.Invoke();
                }
            }
        }
    }

    public static void Permutate<T1, T2, T3>(Action<T1, T2, T3> call, Params parameters = default)
    {
        if (parameters.LastToFirst)
        {
            var options = GetOptions<T3>();
            for (var i = 0; i < options.Length; i++)
            {
                var o3 = options[i];
                var last = i == options.Length - 1;
                Permutate<T1, T2>((o1, o2) => call.Invoke(o1, o2, o3), parameters.ToSub(true));
                if (!last || !parameters.SkipLastIteration)
                {
                    parameters.AfterIteration?.Invoke();
                }
            }
        }
        else
        {
            var options = GetOptions<T1>();
            for (var i = 0; i < options.Length; i++)
            {
                var o1 = options[i];
                var last = i == options.Length - 1;
                Permutate<T2, T3>((o2, o3) => call.Invoke(o1, o2, o3), parameters.ToSub(true));
                if (!last || !parameters.SkipLastIteration)
                {
                    parameters.AfterIteration?.Invoke();
                }
            }
        }
    }

    public static void Permutate<T1, T2, T3, T4>(Action<T1, T2, T3, T4> call, Params parameters = default)
    {
        if (parameters.LastToFirst)
        {
            var options = GetOptions<T4>();
            for (var i = 0; i < options.Length; i++)
            {
                var o4 = options[i];
                var last = i == options.Length - 1;
                Permutate<T1, T2, T3>((o1, o2, o3) => call.Invoke(o1, o2, o3, o4), parameters.ToSub(true));
                if (!last || !parameters.SkipLastIteration)
                {
                    parameters.AfterIteration?.Invoke();
                }
            }
        }
        else
        {
            var options = GetOptions<T1>();
            for (var i = 0; i < options.Length; i++)
            {
                var o1 = options[i];
                var last = i == options.Length - 1;
                Permutate<T2, T3, T4>((o2, o3, o4) => call.Invoke(o1, o2, o3, o4), parameters.ToSub(true));
                if (!last || !parameters.SkipLastIteration)
                {
                    parameters.AfterIteration?.Invoke();
                }
            }
        }
    }

    public static void Permutate<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> call, Params parameters = default)
    {
        if (parameters.LastToFirst)
        {
            var options = GetOptions<T5>();
            for (var i = 0; i < options.Length; i++)
            {
                var o5 = options[i];
                var last = i == options.Length - 1;
                Permutate<T1, T2, T3, T4>((o1, o2, o3, o4) => call.Invoke(o1, o2, o3, o4, o5), parameters.ToSub(true));
                if (!last || !parameters.SkipLastIteration)
                {
                    parameters.AfterIteration?.Invoke();
                }
            }
        }
        else
        {
            var options = GetOptions<T1>();
            for (var i = 0; i < options.Length; i++)
            {
                var o1 = options[i];
                var last = i == options.Length - 1;
                Permutate<T2, T3, T4, T5>((o2, o3, o4, o5) => call.Invoke(o1, o2, o3, o4, o5), parameters.ToSub(true));
                if (!last || !parameters.SkipLastIteration)
                {
                    parameters.AfterIteration?.Invoke();
                }
            }
        }
    }

    private static T[] GetOptions<T>()
    {
        var type = typeof(T);
        if (type == typeof(bool))
        {
            return (Bools as T[])!;
        }

        if (type == typeof(Type))
        {
            return (Types as T[])!;
        }
        
        if (type.IsEnum)
        {
            return Enum.GetValues(type).Cast<T>().ToArray();
        }

        return default!;
    }

    public readonly struct Params(Action? afterIteration = null, bool skipLastIteration = false, bool lastToFirst = false, GeneratorBase? generatorBase = default)
    {
        public readonly Action? AfterIteration = afterIteration;
        public readonly bool SkipLastIteration = skipLastIteration;
        public readonly bool LastToFirst = lastToFirst;
        public readonly GeneratorBase? GeneratorBase = generatorBase;

        public Params ToSub(bool skipLast)
        {
            return new Params(AfterIteration, skipLast, LastToFirst, GeneratorBase);
        }
    }
}