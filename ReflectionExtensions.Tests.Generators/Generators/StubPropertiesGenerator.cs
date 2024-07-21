using System;
using System.Linq;

namespace ReflectionExtensions.Tests.Generators;

public sealed class StubPropertiesGenerator : GeneratorBase
{
    private const int Value = 59962;
    protected override string TypeName => "StubProperties";

    protected override void GenerateInternal()
    {
        using (WithStubFile(TypeName, "#pragma warning disable CS0169", "#pragma warning disable CS0649", ""))
        {
            // filter out static init comboS
            var options = PermutationUtil.GeneratePermutations<bool, bool, PropertyMutability, Type>()
                .Where(x => x.Item1 is not true || x.Item3 is not PropertyMutability.Init)
                .ToList();

            PermutationUtil.Call(GenerateProperty, options);
            AppendCommentBlock("Encapsulation");
            PermutationUtil.Call(GenerateEncapsulation, options);

            AppendLine();

            using (WithIndentBraces("public static void Reset()"))
            {
                PermutationUtil.PermutateCall<bool, Type>(SetProperty);
            }
        }
    }

    private void SetProperty(bool isPublic, Type type)
    {
        Append(GeneratePropertyName(isPublic, true, PropertyMutability.Mutable, type));
        Append(" = ");
        AppendWrap(Value, type == typeof(string));
        AppendLine(';');
    }

    private void GenerateProperty(bool isStatic, bool isPublic, PropertyMutability mutability, Type type)
    {
        var pTypeName = type.Name;

        Append(isPublic ? "public " : "private ");
        Append(isStatic ? "static " : "");
        // Append(isReadonly ? "readonly " : "");
        Append(pTypeName);
        Append(" ");
        Append(GeneratePropertyName(isPublic, isStatic, mutability, type));
        
        Append(" { get; ");
        if (mutability is PropertyMutability.Init)
        {
            Append("init; ");
        }
        else if (mutability is PropertyMutability.Mutable)
        {
            Append("set; ");
        }
        Append("}");
        
        Append(" = ");
        AppendWrap(Value, type == typeof(string));
        AppendLine(';');
    }

    private void GenerateEncapsulation(bool isStatic, bool isPublic, PropertyMutability mutability, Type type)
    {
        var pTypeName = type.Name;

        Append("public ");
        Append(isStatic ? "static " : "");
        Append(pTypeName);
        Append(" ");
        Append(GenerateGetPropertyName(isPublic, isStatic, mutability, type));
        Append(" => ");
        Append(GeneratePropertyName(isPublic, isStatic, mutability, type));
        AppendLine(';');
    }

    public static string GeneratePropertyName(bool isPublic, bool isStatic, PropertyMutability mutability, Type type)
    {
        var @public = isPublic ? "Public_" : "Private_";
        var @static = isStatic ? "Static_" : "";
        return $"{@public}{@static}{mutability}_{type.Name}";
    }

    public static string GenerateGetPropertyName(bool isPublic, bool isStatic, PropertyMutability mutability, Type type)
    {
        return "Get_" + GeneratePropertyName(isPublic, isStatic, mutability, type);
    }
}
    
public enum PropertyMutability
{
    Mutable,
    GetOnly,
    Init
}