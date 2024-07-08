using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        //////////////////////////////////
        //////////   Instance   //////////
        //////////////////////////////////

        public static InstanceGetter<TTarget, TValue> CreateInstanceGetter<TTarget, TValue>(this FieldInfo field) => InstanceGetter<TTarget, TValue, InstanceGetter<TTarget, TValue>>(field);
        public static InstanceGetterT<TTarget> CreateInstanceGetterT<TTarget>(this FieldInfo field) => InstanceGetter<TTarget, object, InstanceGetterT<TTarget>>(field);
        public static InstanceGetter<TValue> CreateInstanceGetterR<TValue>(this FieldInfo field) => InstanceGetter<object, TValue, InstanceGetter<TValue>>(field);
        public static InstanceGetter CreateInstanceGetterX(this FieldInfo field) => InstanceGetter<object, object, InstanceGetter>(field);

        public static InstanceSetter<TTarget, TValue> CreateInstanceSetter<TTarget, TValue>(this FieldInfo field) => InstanceSetter<TTarget, TValue, InstanceSetter<TTarget, TValue>>(field);
        public static InstanceSetterT<TTarget> CreateInstanceSetterT<TTarget>(this FieldInfo field) => InstanceSetter<TTarget, object, InstanceSetterT<TTarget>>(field);
        public static InstanceSetter<TValue> CreateInstanceSetterR<TValue>(this FieldInfo field) => InstanceSetter<object, TValue, InstanceSetter<TValue>>(field);
        public static InstanceSetter CreateInstanceSetterX(this FieldInfo field) => InstanceSetter<object, object, InstanceSetter>(field);

        ////////////////////////////////////////
        //////////   Const Instance   //////////
        ////////////////////////////////////////

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstGetter<TValue> CreateConstInstanceGetter<TValue>(this FieldInfo field, object constInstance) => ConstInstanceGetter<TValue, ConstGetter<TValue>>(field, constInstance);

        public static ConstGetter CreateConstInstanceGetter(this FieldInfo field, object constInstance) => ConstInstanceGetter<object, ConstGetter>(field, constInstance).Invoke;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstSetter<TValue> CreateConstInstanceSetter<TValue>(this FieldInfo field, object constInstance) => ConstInstanceSetter<TValue, ConstSetter<TValue>>(field, constInstance);

        public static ConstSetter CreateConstInstanceSetter(this FieldInfo field, object constInstance) => ConstInstanceSetter<object, ConstSetter>(field, constInstance).Invoke;

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        public static ConstGetter<TValue> CreateStaticGetter<TValue>(this FieldInfo field) => StaticGetter<TValue, ConstGetter<TValue>>(field);
        public static ConstGetter CreateStaticGetterX(this FieldInfo field) => StaticGetter<object, ConstGetter>(field);

        public static ConstSetter<TValue> CreateStaticSetter<TValue>(this FieldInfo field) => StaticSetter<TValue, ConstSetter<TValue>>(field);
        public static ConstSetter CreateStaticSetterX(this FieldInfo field) => StaticSetter<object, ConstSetter>(field);
    }
}