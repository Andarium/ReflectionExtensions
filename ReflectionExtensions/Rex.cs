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
        private static bool TryGetInstanceAccessors<TTarget, TValue, TGetter, TSetter>(
            this Type type,
            string memberName,
            AccessorTarget accessor,
            [NotNullWhen(true)] out TGetter? getter,
            [NotNullWhen(true)] out TSetter? setter
        )
            where TGetter : Delegate
            where TSetter : Delegate
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
                    getter = InstanceGetter<TTarget, TValue, TGetter>(field);
                    setter = InstanceSetter<TTarget, TValue, TSetter>(field);
                    return true;
                }
            }

            if (accessor is AccessorTarget.Property or AccessorTarget.FieldOrProperty)
            {
                var prop = type.GetInstancePropertyInfoOrNull(memberName);
                if (prop is not null)
                {
                    getter = InstanceGetter<TTarget, TValue, TGetter>(prop);
                    setter = InstanceSetter<TTarget, TValue, TSetter>(prop);
                    return true;
                }
            }

            getter = null;
            setter = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TryGetConstInstanceAccessors<TTarget, TValue, TGetter, TSetter>(
            [NotNull] this TTarget? constInstance,
            string memberName,
            AccessorTarget accessor,
            [NotNullWhen(true)] out TGetter? getter,
            [NotNullWhen(true)] out TSetter? setter
        )
            where TGetter : Delegate
            where TSetter : Delegate
        {
            AssertInstance<TTarget>(constInstance, out var instanceType, memberName, accessor.ToMemberType());

            if (accessor is AccessorTarget.Field or AccessorTarget.FieldOrProperty)
            {
                var field = instanceType.GetInstanceFieldInfoOrNull(memberName);
                if (field is not null)
                {
                    getter = ConstInstanceGetter<TValue, TGetter>(field, constInstance);
                    setter = ConstInstanceSetter<TValue, TSetter>(field, constInstance);
                    return true;
                }
            }

            if (accessor is AccessorTarget.Property or AccessorTarget.FieldOrProperty)
            {
                var prop = instanceType.GetInstancePropertyInfoOrNull(memberName);
                if (prop is not null)
                {
                    getter = ConstInstanceGetter<TValue, TGetter>(prop, constInstance);
                    setter = ConstInstanceSetter<TValue, TSetter>(prop, constInstance);
                    return true;
                }
            }

            getter = null;
            setter = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TryGetStaticAccessors<TValue, TGetter, TSetter>(
            this Type type,
            string memberName,
            AccessorTarget accessor,
            [NotNullWhen(true)] out TGetter? getter,
            [NotNullWhen(true)] out TSetter? setter
        )
            where TGetter : Delegate
            where TSetter : Delegate
        {
            if (accessor is AccessorTarget.Field or AccessorTarget.FieldOrProperty)
            {
                var field = type.GetStaticFieldInfoOrNull(memberName);
                if (field is not null)
                {
                    getter = StaticGetter<TValue, TGetter>(field);
                    setter = StaticSetter<TValue, TSetter>(field);
                    return true;
                }
            }

            if (accessor is AccessorTarget.Property or AccessorTarget.FieldOrProperty)
            {
                var prop = type.GetStaticPropertyInfoOrNull(memberName);
                if (prop is not null)
                {
                    getter = StaticGetter<TValue, TGetter>(prop);
                    setter = StaticSetter<TValue, TSetter>(prop);
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

        public static InstanceAccessor CreateInstanceAccessorX(this Type instanceType, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetInstanceAccessors<object, object, InstanceGetter, InstanceSetter>(instanceType, memberName, accessor, out var getter, out var setter))
            {
                return new InstanceAccessor(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {instanceType.AssemblyQualifiedName}");
        }

        public static InstanceAccessorT<TTarget> CreateInstanceAccessorT<TTarget>(this Type instanceType, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetInstanceAccessors<TTarget, object, InstanceGetterT<TTarget>, InstanceSetterT<TTarget>>(instanceType, memberName, accessor, out var getter, out var setter))
            {
                return new InstanceAccessorT<TTarget>(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {instanceType.AssemblyQualifiedName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceAccessor<TValue> CreateInstanceAccessor<TValue>(this Type instanceType, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetInstanceAccessors<object, TValue, InstanceGetter<TValue>, InstanceSetter<TValue>>(instanceType, memberName, accessor, out var getter, out var setter))
            {
                return new InstanceAccessor<TValue>(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {instanceType.AssemblyQualifiedName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstanceAccessor<TTarget, TValue> CreateInstanceAccessor<TTarget, TValue>(string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetInstanceAccessors<TTarget, TValue, InstanceGetter<TTarget, TValue>, InstanceSetter<TTarget, TValue>>(typeof(TTarget), memberName, accessor, out var getter, out var setter))
            {
                return new InstanceAccessor<TTarget, TValue>(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {typeof(TTarget).AssemblyQualifiedName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessorX CreateConstInstanceAccessor([NotNull] this object? constInstance, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetConstInstanceAccessors<object, object, ConstGetter, ConstSetter>(constInstance, memberName, accessor, out var getter, out var setter))
            {
                return new ConstAccessorX(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {constInstance.GetType().AssemblyQualifiedName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessor<TValue> CreateConstInstanceAccessor<TValue>([NotNull] this object? constInstance, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetConstInstanceAccessors<object, TValue, ConstGetter<TValue>, ConstSetter<TValue>>(constInstance, memberName, accessor, out var getter, out var setter))
            {
                return new ConstAccessor<TValue>(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {constInstance.GetType().AssemblyQualifiedName}");
        }

        public static ConstAccessor<TValue> CreateStaticAccessor<TTarget, TValue>(string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetStaticAccessors<TValue, ConstGetter<TValue>, ConstSetter<TValue>>(typeof(TTarget), memberName, accessor, out var getter, out var setter))
            {
                return new ConstAccessor<TValue>(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {typeof(TTarget).AssemblyQualifiedName}");
        }

        public static ConstAccessorX CreateStaticAccessor(this Type type, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetStaticAccessors<object, ConstGetter, ConstSetter>(type, memberName, accessor, out var getter, out var setter))
            {
                return new ConstAccessorX(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {type.AssemblyQualifiedName}");
        }

        public static ConstAccessorX CreateStaticAccessor<TTarget>(string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetStaticAccessors<object, ConstGetter, ConstSetter>(typeof(TTarget), memberName, accessor, out var getter, out var setter))
            {
                return new ConstAccessorX(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {typeof(TTarget).AssemblyQualifiedName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstAccessor<TValue> CreateStaticAccessor<TValue>(this Type type, string memberName, AccessorTarget accessor = AccessorTarget.FieldOrProperty)
        {
            if (TryGetStaticAccessors<TValue, ConstGetter<TValue>, ConstSetter<TValue>>(type, memberName, accessor, out var getter, out var setter))
            {
                return new ConstAccessor<TValue>(getter, setter);
            }

            throw new NullReferenceException($"Can't find member with name {memberName} on type {type.AssemblyQualifiedName}");
        }
    }
}