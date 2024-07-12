using System.Linq;

namespace ReflectionExtensions.Tests.Generators;

public sealed class TestCallMethodGenerator : GeneratorBase
{
    protected override string TypeName => "TestCallInstanceMethod";

    protected override void GenerateInternal()
    {
        using (WithTestFile(TypeName))
        {
            AppendMethodsGroup<int>(5, false, true, false);
            AppendLine();
            AppendMethodsGroup<string>(5, false, true, false);
            AppendLine();
            AppendMethodsGroup<int>(5, false, false, false);
            AppendLine();
            AppendMethodsGroup<string>(5, false, false, false);
            AppendLine();
            AppendMethodsGroup<int>(5, false, true, true);
            AppendLine();
            AppendMethodsGroup<string>(5, false, true, true);
            AppendLine();
            AppendMethodsGroup<int>(5, false, false, true);
            AppendLine();
            AppendMethodsGroup<string>(5, false, false, true);

            AppendLine();

            AppendMethodsGroup<int>(5, true, true, false);
            AppendLine();
            AppendMethodsGroup<string>(5, true, true, false);
            AppendLine();
            AppendMethodsGroup<int>(5, true, false, false);
            AppendLine();
            AppendMethodsGroup<string>(5, true, false, false);
            AppendLine();
            AppendMethodsGroup<int>(5, true, true, true);
            AppendLine();
            AppendMethodsGroup<string>(5, true, true, true);
            AppendLine();
            AppendMethodsGroup<int>(5, true, false, true);
            AppendLine();
            AppendMethodsGroup<string>(5, true, false, true);
        }
    }

    private void AppendMethodsGroup<T>(int upToArgs, bool isStatic, bool isPublic, bool isProcedure)
    {
        if (isStatic)
        {
            InvokeSequence(upToArgs + 1, i => AppendStaticMethods<T>(i, isPublic, isProcedure), AppendType.NewLine);
        }
        else
        {
            InvokeSequence(upToArgs + 1, i => AppendInstanceMethods<T>(i, isPublic, isProcedure), AppendType.NewLine);
        }
    }

    private void AppendInstanceMethods<T>(int args, bool isPublic, bool isProcedure)
    {
        const string extensionName = nameof(ReflectionExtensions.CallInstanceMethod);

        var testMethodNameBase = "Test_" + GenerateMethodName<T>(args, false, isPublic, isProcedure: isProcedure);

        if (!isProcedure)
        {
            using (WithTestMethodScope(testMethodNameBase + "_R"))
            {
                // R
                using (WithMethodResultScope(false, isProcedure))
                {
                    Append($"instance.{extensionName}");
                    AppendGenerics<T>(1);
                    Append("(");
                    AppendMethodName<T>(args, false, isPublic, true);
                    Append(", Args.Of(");
                    AppendParameterValues<T>(args);
                    AppendLine("));");
                }

                AppendAssert<T>(args);
            }

            AppendLine();
        }

        using (WithTestMethodScope(testMethodNameBase + "_X"))
        {
            // X
            using (WithMethodResultScope(false, isProcedure))
            {
                Append($"instance.{extensionName}");
                Append("(");
                AppendMethodName<T>(args, false, isPublic, true);
                Append(", Args.Of(");
                AppendParameterValues<T>(args);
                AppendLine("));");
            }

            AppendAssert<T>(args);
        }
    }

    private void AppendStaticMethods<T>(int args, bool isPublic, bool isProcedure)
    {
        var targetClass = isProcedure ? "StubProcedures" : "StubFunctions";
        const string extensionName = nameof(ReflectionExtensions.CallStaticMethod);

        var testMethodNameBase = "Test_" + GenerateMethodName<T>(args, true, isPublic, isProcedure: isProcedure);

        if (!isProcedure)
        {
            using (WithTestMethodScope(testMethodNameBase + "_TR"))
            {
                // TR
                using (WithMethodResultScope(true, isProcedure))
                {
                    Append($"{extensionName}");
                    AppendGenerics<T>(1, targetClass);
                    Append("(");
                    AppendMethodName<T>(args, true, isPublic, true);
                    Append(", Args.Of(");
                    AppendParameterValues<T>(args);
                    AppendLine("));");
                }

                AppendAssert<T>(args);
            }

            AppendLine();

            using (WithTestMethodScope(testMethodNameBase + "_R"))
            {
                // R
                using (WithMethodResultScope(true, isProcedure))
                {
                    Append($"typeof({targetClass}).{extensionName}");
                    // AppendGenerics<T>(1, targetClass);
                    Append("(");
                    AppendMethodName<T>(args, true, isPublic, true);
                    Append(", Args.Of(");
                    AppendParameterValues<T>(args);
                    AppendLine("));");
                }

                AppendAssert<T>(args);
            }

            AppendLine();
        }

        using (WithTestMethodScope(testMethodNameBase + "_T"))
        {
            // T
            using (WithMethodResultScope(true, isProcedure))
            {
                Append($"{extensionName}");
                AppendGenerics<T>(0, targetClass);
                Append("(");
                AppendMethodName<T>(args, true, isPublic, true);
                Append(", Args.Of(");
                AppendParameterValues<T>(args);
                AppendLine("));");
            }

            AppendAssert<T>(args);
        }

        AppendLine();

        using (WithTestMethodScope(testMethodNameBase + "_X"))
        {
            // X
            using (WithMethodResultScope(true, isProcedure))
            {
                Append($"typeof({targetClass}).{extensionName}");
                Append("(");
                AppendMethodName<T>(args, true, isPublic, true);
                Append(", Args.Of(");
                AppendParameterValues<T>(args);
                AppendLine("));");
            }

            AppendAssert<T>(args);
        }
    }

    private void AppendAssert<T>(int args)
    {
        // Assert
        if (typeof(T) == typeof(string))
        {
            if (args is 0)
            {
                AppendLine("Assert.That(actual, Is.Null);");
                return;
            }

            var expected = string.Join("", Enumerable.Range(1, args));
            AppendLine($"Assert.That(actual, Is.EqualTo(\"{expected}\"));");
        }
        else
        {
            var expected = args * (args + 1) / 2;
            AppendLine($"Assert.That(actual, Is.EqualTo({expected}));");
        }
    }
}