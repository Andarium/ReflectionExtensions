using System;

namespace ReflectionExtensions.Tests.Generators;

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
            _generator.AppendOffset2Line($"var instance = new {_targetClass}();");
        }

        if (isProcedure)
        {
            _generator.AppendOffset2();
        }
        else
        {
            _generator.AppendOffset2($"var {_resultName} = "); //
        }

        //instance.{extensionName}");
        // _generator.AppendGenerics<T>(1);
        // _generator.Append("(");
        // _generator.AppendFunName<T>(args, isStatic, isPublic, true);
        // _generator.AppendLine(");");
        // _generator.AppendInvokeAndAssert<T>(args);
    }

    public void Dispose()
    {
        if (_isProcedure)
        {
            _generator.AppendOffset2Line($"var {_resultName} = {_targetClass}.Result;");
        }
    }
}