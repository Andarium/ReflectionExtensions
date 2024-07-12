namespace ReflectionExtensions.Tests.Generators;

public sealed class TestCallMethodStaticGenerator : GeneratorBase
{
    protected override string TypeName => "TestCallMethodStatic";

    protected override void GenerateInternal()
    {
        using (WithTestFile(TypeName))
        {
            AppendMethodsGroup<int>(5, true, false);
            AppendLine();
            AppendMethodsGroup<string>(5, true, false);
            AppendLine();
            AppendMethodsGroup<int>(5, false, false);
            AppendLine();
            AppendMethodsGroup<string>(5, false, false);
            AppendLine();
            AppendMethodsGroup<int>(5, true, true);
            AppendLine();
            AppendMethodsGroup<string>(5, true, true);
            AppendLine();
            AppendMethodsGroup<int>(5, false, true);
            AppendLine();
            AppendMethodsGroup<string>(5, false, true);
        }
    }

    private void AppendMethodsGroup<T>(int upToArgs, bool isPublic, bool isProcedure)
    {
        InvokeSequence(upToArgs + 1, i => AppendStaticMethods<T>(i, isPublic, isProcedure), AppendType.NewLine);
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

                AppendSumAssert<T>(args);
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

                AppendSumAssert<T>(args);
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

            AppendSumAssert<T>(args);
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

            AppendSumAssert<T>(args);
        }
    }
}