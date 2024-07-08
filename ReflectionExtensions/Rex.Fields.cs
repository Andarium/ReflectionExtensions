using System;
using System.Reflection;

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

        public static Func<TValue> CreateConstInstanceGetter<TTarget, TValue>(this FieldInfo field, TTarget constInstance) => ConstInstanceGetter<TTarget, TValue>(field, constInstance);
        public static Func<TValue> CreateConstInstanceGetterR<TValue>(this FieldInfo field, object constInstance) => ConstInstanceGetter<object, TValue>(field, constInstance);
        public static Func<object> CreateConstInstanceGetterT<TTarget>(this FieldInfo field, TTarget constInstance) => ConstInstanceGetter<TTarget, object>(field, constInstance);
        public static Func<object> CreateConstInstanceGetterX(this FieldInfo field, object constInstance) => ConstInstanceGetter<object, object>(field, constInstance);

        public static Action<TValue> CreateConstInstanceSetter<TTarget, TValue>(this FieldInfo field, TTarget constInstance) => ConstInstanceSetter<TTarget, TValue>(field, constInstance);
        public static Action<object> CreateConstInstanceSetterT<TTarget>(this FieldInfo field, TTarget constInstance) => ConstInstanceSetter<TTarget, object>(field, constInstance);
        public static Action<TValue> CreateConstInstanceSetterR<TValue>(this FieldInfo field, object constInstance) => ConstInstanceSetter<object, TValue>(field, constInstance);
        public static Action<object> CreateConstInstanceSetterX(this FieldInfo field, object constInstance) => ConstInstanceSetter<object, object>(field, constInstance);

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        public static Func<TValue> CreateStaticGetter<TValue>(this FieldInfo field) => StaticGetter<TValue>(field);
        public static Func<object> CreateStaticGetterX(this FieldInfo field) => StaticGetter<object>(field);

        public static Action<TValue> CreateStaticSetter<TValue>(this FieldInfo field) => StaticSetter<TValue>(field);
        public static Action<object> CreateStaticSetterX(this FieldInfo field) => StaticSetter<object>(field);
    }
}