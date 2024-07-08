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

        public static InstanceGetter<TTarget, TValue> CreateInstanceGetter<TTarget, TValue>(this PropertyInfo prop) => InstanceGetter<TTarget, TValue, InstanceGetter<TTarget, TValue>>(prop);
        public static InstanceGetterT<TTarget> CreateInstanceGetterT<TTarget>(this PropertyInfo prop) => InstanceGetter<TTarget, object, InstanceGetterT<TTarget>>(prop);
        public static InstanceGetter<TValue> CreateInstanceGetterR<TValue>(this PropertyInfo prop) => InstanceGetter<object, TValue, InstanceGetter<TValue>>(prop);
        public static InstanceGetter CreateInstanceGetterX(this PropertyInfo prop) => InstanceGetter<object, object, InstanceGetter>(prop);

        public static InstanceSetter<TTarget, TValue> CreateInstanceSetter<TTarget, TValue>(this PropertyInfo prop) => InstanceSetter<TTarget, TValue, InstanceSetter<TTarget, TValue>>(prop);
        public static InstanceSetterT<TTarget> CreateInstanceSetterT<TTarget>(this PropertyInfo prop) => InstanceSetter<TTarget, object, InstanceSetterT<TTarget>>(prop);
        public static InstanceSetter<TValue> CreateInstanceSetterR<TValue>(this PropertyInfo prop) => InstanceSetter<object, TValue, InstanceSetter<TValue>>(prop);
        public static InstanceSetter CreateInstanceSetterX(this PropertyInfo prop) => InstanceSetter<object, object, InstanceSetter>(prop);

        ////////////////////////////////////////
        //////////   Const Instance   //////////
        ////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstGetter<TValue> CreateConstInstanceGetter<TValue>(this PropertyInfo prop, object constInstance) => ConstInstanceGetter<TValue, ConstGetter<TValue>>(prop, constInstance);

        public static ConstGetter CreateConstInstanceGetter(this PropertyInfo prop, object constInstance) => ConstInstanceGetter<object, ConstGetter>(prop, constInstance).Invoke;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstSetter<TValue> CreateConstInstanceSetter<TValue>(this PropertyInfo prop, object constInstance) => ConstInstanceSetter<TValue, ConstSetter<TValue>>(prop, constInstance);

        public static ConstSetter CreateConstInstanceSetter(this PropertyInfo prop, object constInstance) => ConstInstanceSetter<object, ConstSetter>(prop, constInstance).Invoke;

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        public static ConstGetter<TValue> CreateStaticGetter<TValue>(this PropertyInfo prop) => StaticGetter<TValue, ConstGetter<TValue>>(prop);
        public static ConstGetter CreateStaticGetterX(this PropertyInfo prop) => StaticGetter<object, ConstGetter>(prop);

        public static ConstSetter<TValue> CreateStaticSetter<TValue>(this PropertyInfo prop) => StaticSetter<TValue, ConstSetter<TValue>>(prop);
        public static ConstSetter CreateStaticSetterX(this PropertyInfo prop) => StaticSetter<object, ConstSetter>(prop);
    }
}