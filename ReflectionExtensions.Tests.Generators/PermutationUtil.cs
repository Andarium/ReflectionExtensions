using System;
using System.Collections.Generic;
using System.Linq;

namespace ReflectionExtensions.Tests.Generators;

public static class PermutationUtil
{
    private static readonly bool[] Bools = [false, true];
    private static readonly Type[] Types = [typeof(int), typeof(string)];

    private static T[] GetOptionsDefault<T>()
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

    public static void PermutateCall<T1>(Action<T1> method, Action? afterIteration = null, bool skipLastIteration = true)
    {
        var options = GeneratePermutations<T1>();
        Call(method, options.ToList(), afterIteration, skipLastIteration);
    }

    public static void PermutateCall<T1, T2>(Action<T1, T2> method, Action? afterIteration = null, bool skipLastIteration = true)
    {
        var options = GeneratePermutations<T1, T2>();
        Call(method, options.ToList(), afterIteration, skipLastIteration);
    }

    public static void PermutateCall<T1, T2, T3>(Action<T1, T2, T3> method, Action? afterIteration = null, bool skipLastIteration = true)
    {
        var options = GeneratePermutations<T1, T2, T3>();
        Call(method, options.ToList(), afterIteration, skipLastIteration);
    }

    public static void PermutateCall<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, Action? afterIteration = null, bool skipLastIteration = true)
    {
        var options = GeneratePermutations<T1, T2, T3, T4>();
        Call(method, options.ToList(), afterIteration, skipLastIteration);
    }

    public static void PermutateCall<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> method, Action? afterIteration = null, bool skipLastIteration = true)
    {
        var options = GeneratePermutations<T1, T2, T3, T4, T5>();
        Call(method, options.ToList(), afterIteration, skipLastIteration);
    }

    public static IEnumerable<T1> GeneratePermutations<T1>()
    {
        return GetOptionsDefault<T1>();
    }

    public static IEnumerable<(T1, T2)> GeneratePermutations<T1, T2>()
    {
        var t2Options = GetOptionsDefault<T2>();
        var t1Options = GeneratePermutations<T1>();

        foreach (var t1 in t1Options)
        {
            foreach (var t2 in t2Options)
            {
                yield return (t1, t2);
            }
        }
    }

    public static IEnumerable<(T1, T2, T3)> GeneratePermutations<T1, T2, T3>()
    {
        var t23Options = GeneratePermutations<T2, T3>().ToList();
        var t1Options = GeneratePermutations<T1>();

        foreach (var t1 in t1Options)
        {
            foreach (var (t2, t3) in t23Options)
            {
                yield return (t1, t2, t3);
            }
        }
    }

    public static IEnumerable<(T1, T2, T3, T4)> GeneratePermutations<T1, T2, T3, T4>()
    {
        var t234Options = GeneratePermutations<T2, T3, T4>().ToList();
        var t1Options = GeneratePermutations<T1>();

        foreach (var t1 in t1Options)
        {
            foreach (var (t2, t3, t4) in t234Options)
            {
                yield return (t1, t2, t3, t4);
            }
        }
    }

    public static IEnumerable<(T1, T2, T3, T4, T5)> GeneratePermutations<T1, T2, T3, T4, T5>()
    {
        var t234Options = GeneratePermutations<T2, T3, T4, T5>().ToList();
        var t1Options = GeneratePermutations<T1>();

        foreach (var t1 in t1Options)
        {
            foreach (var (t2, t3, t4, t5) in t234Options)
            {
                yield return (t1, t2, t3, t4, t5);
            }
        }
    }

    public static void Call<T1>(Action<T1> method, IList<T1> argSets, Action? afterIteration = null, bool skipLastIteration = false)
    {
        var count = argSets.Count;
        for (var i = 0; i < count; i++)
        {
            var args = argSets[i];
            method.Invoke(args);
            if (i < count - 1 || !skipLastIteration)
            {
                afterIteration?.Invoke();
            }
        }
    }

    public static void Call<T1, T2>(Action<T1, T2> method, IList<(T1, T2)> argSets, Action? afterIteration = null, bool skipLastIteration = false)
    {
        var count = argSets.Count;
        for (var i = 0; i < count; i++)
        {
            var args = argSets[i];
            method.Invoke(args.Item1, args.Item2);
            if (i < count - 1 || !skipLastIteration)
            {
                afterIteration?.Invoke();
            }
        }
    }

    public static void Call<T1, T2, T3>(Action<T1, T2, T3> method, IList<(T1, T2, T3)> argSets, Action? afterIteration = null, bool skipLastIteration = false)
    {
        var count = argSets.Count;
        for (var i = 0; i < count; i++)
        {
            var args = argSets[i];
            method.Invoke(args.Item1, args.Item2, args.Item3);
            if (i < count - 1 || !skipLastIteration)
            {
                afterIteration?.Invoke();
            }
        }
    }

    public static void Call<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, IList<(T1, T2, T3, T4)> argSets, Action? afterIteration = null, bool skipLastIteration = false)
    {
        var count = argSets.Count;
        for (var i = 0; i < count; i++)
        {
            var args = argSets[i];
            method.Invoke(args.Item1, args.Item2, args.Item3, args.Item4);
            if (i < count - 1 || !skipLastIteration)
            {
                afterIteration?.Invoke();
            }
        }
    }

    public static void Call<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> method, IList<(T1, T2, T3, T4, T5)> argSets, Action? afterIteration = null, bool skipLastIteration = false)
    {
        var count = argSets.Count;
        for (var i = 0; i < count; i++)
        {
            var args = argSets[i];
            method.Invoke(args.Item1, args.Item2, args.Item3, args.Item4, args.Item5);

            if (i < count - 1 || !skipLastIteration)
            {
                afterIteration?.Invoke();
            }
        }
    }
}