using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        private static Expression Cast<T>(this Expression input) => input.Cast(typeof(T));

        private static Expression Cast(this Expression input, Type targetType)
        {
            if (input.Type == targetType)
            {
                return input;
            }

            return targetType.IsValueType && (input.Type == typeof(object) || input.Type.IsInterface)
                ? Expression.Unbox(input, targetType)
                : Expression.Convert(input, targetType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TryGetInstanceAccessors<TTarget, TValue>(
            this Type type,
            string memberName,
            AccessorTarget accessor,
            [NotNullWhen(true)] out Func<TTarget, TValue>? getter,
            [NotNullWhen(true)] out Action<TTarget, TValue>? setter
        )
        {
            if (!typeof(TTarget).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Type {typeof(TTarget).AssemblyQualifiedName} is not assignable from {type.AssemblyQualifiedName}", nameof(type));
            }

            if (accessor is AccessorTarget.Field or AccessorTarget.FieldOrProperty)
            {
                var field = type.GetInstanceFieldInfoOrNull(memberName);
                if (field is not null)
                {
                    getter = field.CreateInstanceGetter<TTarget, TValue>();
                    setter = field.CreateInstanceSetter<TTarget, TValue>();
                    return true;
                }
            }

            if (accessor is AccessorTarget.Property or AccessorTarget.FieldOrProperty)
            {
                var prop = type.GetInstancePropertyInfoOrNull(memberName);
                if (prop is not null)
                {
                    getter = prop.CreateInstanceGetter<TTarget, TValue>();
                    setter = prop.CreateInstanceSetter<TTarget, TValue>();
                    return true;
                }
            }

            getter = null;
            setter = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate LogAndCompile<TDelegate>(this Expression<TDelegate> input)
        {
#if DEBUG
            Console.WriteLine(input.ToString());
#endif
            return input.Compile();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceAccessor<TValue> CreateInstanceAccessor<TValue>(this Type type, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetInstanceAccessors<object, TValue>(type, memberName, accessor, out var getter, out var setter))
            {
                return new InstanceAccessor<TValue>(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {type.AssemblyQualifiedName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceAccessor<TTarget, TValue> CreateInstanceAccessor<TTarget, TValue>(this Type type, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetInstanceAccessors<TTarget, TValue>(type, memberName, accessor, out var getter, out var setter))
            {
                return new InstanceAccessor<TTarget, TValue>(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {typeof(TTarget).AssemblyQualifiedName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessor<TValue> CreateConstInstanceAccessor<TValue>([NotNull] this object? instance, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            return CreateConstInstanceAccessor<TValue>(instance?.GetType(), instance, memberName, accessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessor<TValue> CreateConstInstanceAccessor<TValue>([NotNull] this object? instance, Type instanceType, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            return CreateConstInstanceAccessor<TValue>(instanceType, instance, memberName, accessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessor<TValue> CreateConstInstanceAccessor<TTarget, TValue>([NotNull] this TTarget? instance, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            return CreateConstInstanceAccessor<TValue>(typeof(TTarget), instance, memberName, accessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessor<TValue> CreateConstInstanceAccessor<TValue>([NotNull] this Type? type, [NotNull] object? instance, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            AssertInstanceAndType(instance, ref type, memberName, accessor.ToMemberType());

            if (accessor is AccessorTarget.Field or AccessorTarget.FieldOrProperty)
            {
                var field = type.GetInstanceFieldInfoOrNull(memberName);
                if (field is not null)
                {
                    var getter = field.CreateConstInstanceGetter<TValue>(instance);
                    var setter = field.CreateConstInstanceSetter<TValue>(instance);
                    return new ConstAccessor<TValue>(getter, setter);
                }
            }

            if (accessor is AccessorTarget.Property or AccessorTarget.FieldOrProperty)
            {
                var prop = type.GetInstancePropertyInfoOrNull(memberName);
                if (prop is not null)
                {
                    var getter = prop.CreateConstInstanceGetter<TValue>(instance);
                    var setter = prop.CreateConstInstanceSetter<TValue>(instance);
                    return new ConstAccessor<TValue>(getter, setter);
                }
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {type.AssemblyQualifiedName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessor<TValue> CreateStaticAccessor<TTarget, TValue>(string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            return CreateStaticAccessor<TValue>(typeof(TTarget), memberName, accessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessor<TValue> CreateStaticAccessor<TValue>(this Type type, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (accessor is AccessorTarget.Field or AccessorTarget.FieldOrProperty)
            {
                var field = type.GetStaticFieldInfoOrNull(memberName);
                if (field is not null)
                {
                    var getter = field.CreateStaticGetter<TValue>();
                    var setter = field.CreateStaticSetter<TValue>();
                    return new ConstAccessor<TValue>(getter, setter);
                }
            }

            if (accessor is AccessorTarget.Property or AccessorTarget.FieldOrProperty)
            {
                var prop = type.GetStaticPropertyInfoOrNull(memberName);
                if (prop is not null)
                {
                    var getter = prop.CreateStaticGetter<TValue>();
                    var setter = prop.CreateStaticSetter<TValue>();
                    return new ConstAccessor<TValue>(getter, setter);
                }
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {type.AssemblyQualifiedName}");
        }
    }
}