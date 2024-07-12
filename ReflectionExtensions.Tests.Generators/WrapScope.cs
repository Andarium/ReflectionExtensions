using System;
using System.Text;

namespace ReflectionExtensions.Tests.Generators;

internal readonly struct WrapScope : IDisposable
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