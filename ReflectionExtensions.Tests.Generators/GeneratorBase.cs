using System;
using System.Text;

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

    protected void AppendLine() => _s.AppendLine();

    protected void AppendOffset() => _s.Append(Offset);
    protected void AppendOffset2() => _s.Append(Offset).Append(Offset);

    protected void AppendOffset(string value) => _s.Append(Offset).Append(value);
    protected void AppendOffset2(string value) => _s.Append(Offset).Append(Offset).Append(value);

    protected void AppendOffsetLine() => _s.Append(Offset).AppendLine();
    protected void AppendOffset2Line() => _s.Append(Offset).Append(Offset).AppendLine();

    protected void AppendOffsetLine(string value) => _s.Append(Offset).AppendLine(value);
    protected void AppendOffset2Line(string value) => _s.Append(Offset).Append(Offset).AppendLine(value);

    protected void AppendLine<T>(T value)
    {
        Append(value);
        AppendLine();
    }

    protected void AppendWrap<T>(T value, bool wrap, char c = '"')
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

    protected void AppendWrap<T>(T value, char c = '"') => AppendWrap(value, true, c);

    protected void AppendName<T>() => Append(typeof(T).Name);

    protected void AppendTypeOf<T>(int count, bool hasPrev = true)
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

    protected void AppendTypes<T>(int count) => AppendSequence(count, _ => typeof(T).Name, AppendType.Comma);

    protected void AppendSequence(int count, Func<int, string> f, AppendType append = AppendType.None)
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

    protected void InvokeSequence(int count, Action<int> f, AppendType append = AppendType.None, int startIndex = 0)
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

    protected enum AppendType
    {
        None,
        Comma,
        NewLine,
        Plus
    }

    protected void AppendParameterValues<T>(int args)
    {
        InvokeSequence(args, i => AppendWrap(i + 1, typeof(string) == typeof(T)), AppendType.Comma);
    }

    protected void AppendGenerics<T>(int args, string? prependTargetClass = null)
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

    protected void Append<T>(T value)
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

    protected void AppendFunName<T>(int args, bool isStatic, bool isPublic, bool wrap = false)
    {
        Append(GenerateFunName<T>(args, isStatic, isPublic, wrap));
    }

    protected string GenerateFunName<T>(int args, bool isStatic, bool isPublic, bool wrap = false)
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
        }

        return _temp.ToString();
    }

    protected void AppendConstructorName<T>(string baseName, int args, bool wrap = true)
    {
        Append(GenerateConstructorName<T>(baseName, args, wrap));
    }

    protected string GenerateConstructorName<T>(string baseName, int args, bool wrap = true)
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

    protected void AppendNewArray<T>(int count, Func<int, string> onElement)
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

    protected TestMethodScope WithTestMethodScope(string methodName)
    {
        return new TestMethodScope(this, methodName);
    }

    protected NewArrayScope<T> WithNewArrayScope<T>()
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

    protected readonly struct TestMethodScope : IDisposable
    {
        private readonly GeneratorBase _generator;

        public TestMethodScope(GeneratorBase generator, string methodName)
        {
            _generator = generator;

            _generator.AppendOffsetLine("[Test]");
            _generator.AppendOffset("public void ");
            _generator.Append(methodName);
            _generator.AppendLine("()");
            _generator.AppendOffsetLine("{");
        }

        public void Dispose()
        {
            _generator.AppendOffsetLine("}");
        }
    }

    protected readonly struct NewArrayScope<T> : IDisposable
    {
        private readonly GeneratorBase _generator;

        public NewArrayScope(GeneratorBase generator)
        {
            _generator = generator;
            _generator.Append("new ");
            _generator.Append(typeof(T).Name);
            _generator.Append("[] { ");
        }

        public void Dispose()
        {
            _generator.Append(" }");
        }

        public static string GetEmptyArray()
        {
            return $"new {typeof(T).Name}[] {{ }}";
        }
    }

    protected readonly struct WrapScope : IDisposable
    {
        private readonly StringBuilder _sb;
        private readonly string _s;

        public WrapScope(StringBuilder stringBuilder, string s)
        {
            _sb = stringBuilder;
            _s = s;
            _sb.Append(_s);
        }

        public void Dispose()
        {
            _sb.Append(_s);
        }
    }

    private readonly struct DummyScope : IDisposable
    {
        public void Dispose()
        {
        }
    }
}