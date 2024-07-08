using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        //////////////////////////////////
        //////////   Instance   //////////
        //////////////////////////////////

        public static Func<TTarget, TValue> CreateInstanceGetter<TTarget, TValue>(this FieldInfo field) => InstanceGetter<TTarget, TValue>(field);
        public static Func<TTarget, object> CreateInstanceGetterT<TTarget>(this FieldInfo field) => InstanceGetter<TTarget, object>(field);
        public static Func<object, TValue> CreateInstanceGetterR<TValue>(this FieldInfo field) => InstanceGetter<object, TValue>(field);
        public static Func<object, object> CreateInstanceGetterX(this FieldInfo field) => InstanceGetter<object, object>(field);

        public static Action<TTarget, TValue> CreateInstanceSetter<TTarget, TValue>(this FieldInfo field) => InstanceSetter<TTarget, TValue>(field);
        public static Action<TTarget, object> CreateInstanceSetterT<TTarget>(this FieldInfo field) => InstanceSetter<TTarget, object>(field);
        public static Action<object, TValue> CreateInstanceSetterR<TValue>(this FieldInfo field) => InstanceSetter<object, TValue>(field);
        public static Action<object, object> CreateInstanceSetterX(this FieldInfo field) => InstanceSetter<object, object>(field);

        ////////////////////////////////////////
        //////////   Const Instance   //////////
        ////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateConstInstanceGetter<TValue>(this FieldInfo field, object constInstance) => ConstInstanceGetter<TValue>(field, constInstance);

        public static Func<object> CreateConstInstanceGetter(this FieldInfo field, object constInstance) => ConstInstanceGetter<object>(field, constInstance);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Action<TValue> CreateConstInstanceSetter<TValue>(this FieldInfo field, object constInstance) => ConstInstanceSetter<TValue>(field, constInstance);
        public static Action<object> CreateConstInstanceSetter(this FieldInfo field, object constInstance) => ConstInstanceSetter<object>(field, constInstance);

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        public static Func<TValue> CreateStaticGetter<TValue>(this FieldInfo field) => StaticGetter<TValue>(field);
        public static Func<object> CreateStaticGetterX(this FieldInfo field) => StaticGetter<object>(field);

        public static Action<TValue> CreateStaticSetter<TValue>(this FieldInfo field) => StaticSetter<TValue>(field);
        public static Action<object> CreateStaticSetterX(this FieldInfo field) => StaticSetter<object>(field);
    }
}