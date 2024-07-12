using System;
using System.Text;
using static ReflectionExtensions.ReflectionExtensions;

namespace ReflectionExtensions.Tests.Generators;

public abstract class GeneratorBase : IGenerator
{
    protected const string Offset = "    ";

    private readonly StringBuilder _s = new();
    private readonly StringBuilder _temp = new();

    protected abstract string TypeName { get; }

    public SourceFile Generate()
    {
        _s.Clear();
        GenerateInternal();
        return new SourceFile($"{TypeName}.gs.c", _s.ToString());
    }

    protected abstract void GenerateInternal();

    internal void AppendLine() => _s.AppendLine();

    internal void AppendOffset() => _s.Append(Offset);
    internal void AppendOffset2() => _s.Append(Offset).Append(Offset);

    internal void AppendOffset(string value) => _s.Append(Offset).Append(value);
    internal void AppendOffset2(string value) => _s.Append(Offset).Append(Offset).Append(value);

    internal void AppendOffsetLine() => _s.Append(Offset).AppendLine();
    internal void AppendOffset2Line() => _s.Append(Offset).Append(Offset).AppendLine();

    internal void AppendOffsetLine(string value) => _s.Append(Offset).AppendLine(value);
    internal void AppendOffset2Line(string value) => _s.Append(Offset).Append(Offset).AppendLine(value);

    internal void AppendLine<T>(T value)
    {
        Append(value);
        AppendLine();
    }

    internal void AppendWrap<T>(T value, bool wrap, char c = '"')
    {
        if (wrap)
        {
            Append(c);
            Append(value);
            Append(c);
        }
        else
        {
            Append(value);
        }
    }

    internal void AppendWrap<T>(T value, char c = '"') => AppendWrap(value, true, c);

    internal void AppendName<T>() => Append(typeof(T).Name);

    internal void AppendTypeOf<T>(int count, bool hasPrev = true)
    {
        if (hasPrev)
        {
            Append(", ");
        }

        if (count is 0)
        {
            Append(NewArrayScope<Type>.GetEmptyArray());
            return;
        }

        AppendSequence(count, _ => $"typeof({typeof(T).Name})", AppendType.Comma);
    }

    internal void AppendTypes<T>(int count) => AppendSequence(count, _ => typeof(T).Name, AppendType.Comma);

    internal void AppendSequence(int count, Func<int, string> f, AppendType append = AppendType.None)
    {
        for (var i = 0; i < count; i++)
        {
            Append(f.Invoke(i));

            if (i < count - 1)
            {
                Append(append);
            }
        }
    }

    internal void InvokeSequence(int count, Action<int> f, AppendType append = AppendType.None, int startIndex = 0)
    {
        for (var i = startIndex; i < count; i++)
        {
            f.Invoke(i);

            if (i < count - 1)
            {
                Append(append);
            }
        }
    }

    private void Append(AppendType append)
    {
        switch (append)
        {
            case AppendType.Comma:
                Append(", ");
                break;
            case AppendType.Plus:
                Append(" + ");
                break;
            case AppendType.NewLine:
                AppendLine();
                break;
        }
    }

    internal enum AppendType
    {
        None,
        Comma,
        NewLine,
        Plus
    }

    internal void AppendParameterValues<T>(int args, bool hasPrev = false)
    {
        if (hasPrev)
        {
            Append(", ");
        }

        InvokeSequence(args, i => AppendWrap(i + 1, typeof(string) == typeof(T)), AppendType.Comma);
    }

    internal void AppendGenerics<T>(int args, string? prependTargetClass = null)
    {
        var totalArgs = args + (prependTargetClass is null ? 0 : 1);
        if (totalArgs is 0)
        {
            return;
        }

        Append('<');

        if (prependTargetClass is not null)
        {
            Append(prependTargetClass);
            if (args > 0)
            {
                Append(", ");
            }
        }

        AppendTypes<T>(args);

        Append('>');
    }

    internal void Append<T>(T value)
    {
        switch (value)
        {
            case char c:
            {
                _s.Append(c);
                break;
            }
            case char[] c:
            {
                _s.Append(c);
                break;
            }
            case string c:
            {
                _s.Append(c);
                break;
            }
            case bool c:
            {
                _s.Append(c);
                break;
            }
            case byte c:
            {
                _s.Append(c);
                break;
            }
            case sbyte c:
            {
                _s.Append(c);
                break;
            }
            case short c:
            {
                _s.Append(c);
                break;
            }
            case int c:
            {
                _s.Append(c);
                break;
            }
            case long c:
            {
                _s.Append(c);
                break;
            }
            case float c:
            {
                _s.Append(c);
                break;
            }
            case double c:
            {
                _s.Append(c);
                break;
            }
            case decimal c:
            {
                _s.Append(c);
                break;
            }
            default:
            {
                _s.Append(value);
                break;
            }
        }
    }

    internal void AppendMethodName<T>(int args, bool isStatic, bool isPublic, bool wrap = false, bool? isProcedure = null)
    {
        Append(GenerateMethodName<T>(args, isStatic, isPublic, wrap, isProcedure));
    }

    internal string GenerateMethodName<T>(int args, bool isStatic, bool isPublic, bool wrap = false, bool? isProcedure = null)
    {
        _temp.Clear();

        using (WithWrapScope(wrap, '"', _temp))
        {
            _temp.Append(isPublic ? "Public_" : "Private_");
            _temp.Append(isStatic ? "Static_" : "Instance_");
            _temp.Append("Sum");
            _temp.Append(args);
            _temp.Append('_');
            _temp.Append(typeof(T).Name);
            if (isProcedure is true)
            {
                _temp.Append("_Procedure");
            }
            else if (isProcedure is false)
            {
                _temp.Append("_Function");
            }
        }

        return _temp.ToString();
    }

    internal void AppendConstructorName<T>(string baseName, int args, bool wrap = true)
    {
        Append(GenerateConstructorName<T>(baseName, args, wrap));
    }

    internal string GenerateConstructorName<T>(string baseName, int args, bool wrap = true)
    {
        _temp.Clear();

        using (WithWrapScope(wrap, '"', _temp))
        {
            _temp.Append(baseName);
            _temp.Append('_');
            _temp.Append(typeof(T).Name);
            _temp.Append('_');
            _temp.Append(args);
        }

        return _temp.ToString();
    }

    internal void AppendNewArray<T>(int count, Func<int, string> onElement)
    {
        if (count is 0)
        {
            Append(NewArrayScope<T>.GetEmptyArray());
            return;
        }

        using (WithNewArrayScope<T>())
        {
            AppendSequence(count, onElement, AppendType.Comma);
        }
    }

    internal void AppendTestFileStart()
    {
        AppendLine("// <auto-generated/>");
        AppendLine("using System;");
        AppendLine("using NUnit.Framework;");
        AppendLine("using static ReflectionExtensions.ReflectionExtensions;");
        AppendLine();
        AppendLine("namespace ReflectionExtensions.Tests;");
        AppendLine();
        AppendLine("[TestFixture]");
        AppendLine($"public sealed class {TypeName}");
        AppendLine("{");

        AppendOffsetLine("[SetUp]");
        AppendOffsetLine("public void Setup()");
        AppendOffsetLine("{");
        AppendOffset2Line($"{nameof(ReflectionExtensions)}.{nameof(ClearCache)}();");
        AppendOffset2Line("StubProcedures.Clear();");
        AppendOffsetLine("}");
        AppendLine();
    }

    protected IDisposable WithMethodResultScope(bool isProcedure, string resultName = "actual")
    {
        return new MethodResultScope(this, isProcedure, resultName);
    }

    protected IDisposable WithTestMethodScope(string methodName)
    {
        return new TestMethodScope(this, methodName);
    }

    protected IDisposable WithNewArrayScope<T>()
    {
        return new NewArrayScope<T>(this);
    }

    protected IDisposable WithWrapScope(bool wrap, string s, StringBuilder? sb = default)
    {
        if (!wrap)
        {
            return new DummyScope();
        }

        return new WrapScope(sb ?? _s, s);
    }

    protected IDisposable WithWrapScope(bool wrap, char c, StringBuilder? sb = default)
    {
        if (!wrap)
        {
            return new DummyScope();
        }

        return new WrapScope(sb ?? _s, c.ToString());
    }

    private readonly struct DummyScope : IDisposable
    {
        public void Dispose()
        {
        }
    }
}