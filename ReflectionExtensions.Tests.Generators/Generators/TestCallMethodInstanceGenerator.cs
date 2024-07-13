namespace ReflectionExtensions.Tests.Generators;

public sealed class TestCallMethodInstanceGenerator : GeneratorBase
{
    protected override string TypeName => "TestCallMethodInstance";

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
        InvokeSequence(upToArgs + 1, i => AppendInstanceMethods<T>(i, isPublic, isProcedure), AppendType.NewLine);
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

                AppendSumAssert<T>(args);
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

            AppendSumAssert<T>(args);
        }
    }
}