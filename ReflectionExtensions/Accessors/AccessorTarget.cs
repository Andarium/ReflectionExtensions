// ReSharper disable CheckNamespace
using System;

namespace ReflectionExtensions
{
    [Flags]
    public enum AccessorTarget
    {
        Field = 1,
        Property = 2,
        FieldOrProperty = Field | Property
    }

    internal static class AccessorTargetExtensions
    {
        public static ReflectionExtensions.MemberType ToMemberType(this AccessorTarget target)
        {
            return target switch
            {
                AccessorTarget.Field => ReflectionExtensions.MemberType.Field,
                AccessorTarget.Property => ReflectionExtensions.MemberType.Property,
                AccessorTarget.FieldOrProperty => ReflectionExtensions.MemberType.FieldOrProperty,
                _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
            };
        }
    }
}