using System;

namespace ReflectionExtensions.Tests.Generators;

public static class Extensions
{
    public static string Wrap<T>(this T value, char c = '"') => Wrap(value, true, c, c);
    public static string Wrap<T>(this T value, string c = "\"") => Wrap(value, true, c, c);
    public static string Wrap<T>(this T value, bool wrap, char start, char end) => Wrap(value, wrap, start.ToString(), end.ToString());
    public static string Wrap<T>(this T value, string start, string end) => Wrap(value, true, start, end);

    public static string Wrap<T>(this T value, bool wrap, string start = "\"", string end = "\"")
    {
        return wrap ? $"{start}{value}{end}" : value!.ToString();
    }

    public static bool HasEnumFlag<T>(this T input, T flag) where T : Enum
    {
        return input.HasFlag(flag);
    }

    public static string GetExtensionName(this ConstFunctionType type)
    {
        return type switch
        {
            // X don't have postfix in name
            ConstFunctionType.X => nameof(ReflectionExtensions.CreateConstInstanceFunction),
            ConstFunctionType.A => nameof(ReflectionExtensions.CreateConstInstanceFunctionA),
            ConstFunctionType.R => nameof(ReflectionExtensions.CreateConstInstanceFunctionR),
            ConstFunctionType.Generic => nameof(ReflectionExtensions.CreateConstInstanceFunction),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public static string GetExtensionName(this FunctionType type)
    {
        return type switch
        {
            FunctionType.X => nameof(ReflectionExtensions.CreateInstanceFunctionX),
            FunctionType.T => nameof(ReflectionExtensions.CreateInstanceFunctionT),
            FunctionType.A => nameof(ReflectionExtensions.CreateInstanceFunctionA),
            FunctionType.R => nameof(ReflectionExtensions.CreateInstanceFunctionR),
            FunctionType.TA => nameof(ReflectionExtensions.CreateInstanceFunctionTA),
            FunctionType.TR => nameof(ReflectionExtensions.CreateInstanceFunctionTR),
            FunctionType.AR => nameof(ReflectionExtensions.CreateInstanceFunctionAR),
            FunctionType.Generic => nameof(ReflectionExtensions.CreateInstanceFunction),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public static string GetExtensionName(this StaticFunctionType type)
    {
        return type switch
        {
            StaticFunctionType.X => nameof(ReflectionExtensions.CreateStaticFunctionX),
            StaticFunctionType.T => nameof(ReflectionExtensions.CreateStaticFunctionT),
            StaticFunctionType.A => nameof(ReflectionExtensions.CreateStaticFunctionA),
            StaticFunctionType.R => nameof(ReflectionExtensions.CreateStaticFunctionR),
            StaticFunctionType.TA => nameof(ReflectionExtensions.CreateStaticFunctionTA),
            StaticFunctionType.TR => nameof(ReflectionExtensions.CreateStaticFunctionTR),
            // AR don't have postfix in name
            StaticFunctionType.AR => nameof(ReflectionExtensions.CreateStaticFunction),
            StaticFunctionType.Generic => nameof(ReflectionExtensions.CreateStaticFunction),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}