using System;

namespace ReflectionExtensions.Tests.Generators;

public sealed class StubFieldsGenerator : GeneratorBase
{
    private const int Value = 1337;
    protected override string TypeName => "StubFields";

    protected override void GenerateInternal()
    {
        using (WithStubFile(TypeName, "#pragma warning disable CS0169", "#pragma warning disable CS0649", ""))
        {
            PermutationUtil.Permutate<bool, bool, bool, Type>(GenerateField);
            AppendCommentBlock("Encapsulation");
            PermutationUtil.Permutate<bool, bool, bool, Type>(GenerateEncapsulation);

            AppendLine();

            using (WithIndentBraces($"public static void Reset()"))
            {
                PermutationUtil.Permutate<bool, Type>(SetField);
            }
        }
    }

    private void SetField(bool isPublic, Type type)
    {
        Append(GenerateFieldName(isPublic, true, false, type));
        Append(" = ");
        AppendWrap(Value, type == typeof(string));
        AppendLine(';');
    }

    private void GenerateField(bool isStatic, bool isPublic, bool isReadonly, Type type)
    {
        var pTypeName = type.Name;

        Append(isPublic ? "public " : "private ");
        Append(isStatic ? "static " : "");
        Append(isReadonly ? "readonly " : "");
        Append(pTypeName);
        Append(" ");
        Append(GenerateFieldName(isPublic, isStatic, isReadonly, type));
        // Append(" = ");
        // AppendWrap(Value, type == typeof(string));
        AppendLine(';');
    }

    private void GenerateEncapsulation(bool isStatic, bool isPublic, bool isReadonly, Type type)
    {
        var pTypeName = type.Name;

        Append("public ");
        Append(isStatic ? "static " : "");
        Append(pTypeName);
        Append(" ");
        Append(GenerateGetFieldName(isPublic, isStatic, isReadonly, type));
        Append(" => ");
        Append(GenerateFieldName(isPublic, isStatic, isReadonly, type));
        AppendLine(';');
    }

    public static string GenerateFieldName<T>(bool isPublic, bool isStatic, bool isReadonly) => GenerateFieldName(isPublic, isStatic, isReadonly, typeof(T));

    public static string GenerateFieldName(bool isPublic, bool isStatic, bool isReadonly, Type type)
    {
        var @public = isPublic ? "Public_" : "Private_";
        var @static = isStatic ? "Static_" : "";
        var @readonly = isReadonly ? "Readonly_" : "";
        return @public + @static + @readonly + type.Name;
    }

    public static string GenerateGetFieldName(bool isPublic, bool isStatic, bool isReadonly, Type type)
    {
        return "Get_" + GenerateFieldName(isPublic, isStatic, isReadonly, type);
    }
}