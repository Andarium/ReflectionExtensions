using System;

namespace ReflectionExtensions.Tests.Generators.Scopes;

public readonly struct MethodResultScope : IDisposable
{
    private readonly bool _isProcedure;
    private readonly string _resultName;
    private readonly GeneratorBase _generator;
    private readonly string _targetClass;

    public MethodResultScope(GeneratorBase generator, bool isStatic, bool isProcedure, string resultName = "actual")
    {
        _targetClass = isProcedure ? "StubProcedures" : "StubFunctions";

        _generator = generator;
        _resultName = resultName;
        _isProcedure = isProcedure;

        if (!isStatic)
        {
            _generator.AppendLine($"var instance = new {_targetClass}();");
        }

        if (!isProcedure)
        {
            _generator.Append($"var {_resultName} = ");
        }
    }

    public void Dispose()
    {
        if (_isProcedure)
        {
            _generator.AppendLine($"var {_resultName} = {_targetClass}.Result;");
        }
    }
}