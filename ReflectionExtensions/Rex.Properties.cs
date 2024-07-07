using System;
using System.Reflection;

namespace ReflectionExtensions
{
    public static partial class ReflectionExtensions
    {
        //////////////////////////////////
        //////////   Instance   //////////
        //////////////////////////////////

        public static Func<TTarget, TValue> CreateInstanceGetter<TTarget, TValue>(this PropertyInfo prop) => InstanceGetter<TTarget, TValue>(prop);
        public static Func<TTarget, object> CreateInstanceGetterT<TTarget>(this PropertyInfo prop) => InstanceGetter<TTarget, object>(prop);
        public static Func<object, TValue> CreateInstanceGetterR<TValue>(this PropertyInfo prop) => InstanceGetter<object, TValue>(prop);
        public static Func<object, object> CreateInstanceGetterX(this PropertyInfo prop) => InstanceGetter<object, object>(prop);

        public static Action<TTarget, TValue> CreateInstanceSetter<TTarget, TValue>(this PropertyInfo prop) => InstanceSetter<TTarget, TValue>(prop);
        public static Action<TTarget, object> CreateInstanceSetterT<TTarget>(this PropertyInfo prop) => InstanceSetter<TTarget, object>(prop);
        public static Action<object, TValue> CreateInstanceSetterR<TValue>(this PropertyInfo prop) => InstanceSetter<object, TValue>(prop);
        public static Action<object, object> CreateInstanceSetterX(this PropertyInfo prop) => InstanceSetter<object, object>(prop);

        ////////////////////////////////////////
        //////////   Const Instance   //////////
        ////////////////////////////////////////

        public static Func<TValue> CreateConstInstanceGetter<TTarget, TValue>(this PropertyInfo prop, TTarget constInstance) => ConstInstanceGetter<TTarget, TValue>(prop, constInstance);
        public static Func<TValue> CreateConstInstanceGetterR<TValue>(this PropertyInfo prop, object constInstance) => ConstInstanceGetter<object, TValue>(prop, constInstance);
        public static Func<object> CreateConstInstanceGetterT<TTarget>(this PropertyInfo prop, TTarget constInstance) => ConstInstanceGetter<TTarget, object>(prop, constInstance);
        public static Func<object> CreateConstInstanceGetterX(this PropertyInfo prop, object constInstance) => ConstInstanceGetter<object, object>(prop, constInstance);

        public static Action<TValue> CreateConstInstanceSetter<TTarget, TValue>(this PropertyInfo prop, TTarget constInstance) => ConstInstanceSetter<TTarget, TValue>(prop, constInstance);
        public static Action<object> CreateConstInstanceSetterT<TTarget>(this PropertyInfo prop, TTarget constInstance) => ConstInstanceSetter<TTarget, object>(prop, constInstance);
        public static Action<TValue> CreateConstInstanceSetterR<TValue>(this PropertyInfo prop, object constInstance) => ConstInstanceSetter<object, TValue>(prop, constInstance);
        public static Action<object> CreateConstInstanceSetterX(this PropertyInfo prop, object constInstance) => ConstInstanceSetter<object, object>(prop, constInstance);

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        public static Func<TValue> CreateStaticGetter<TValue>(this PropertyInfo prop) => StaticGetter<TValue>(prop);
        public static Func<object> CreateStaticGetterX(this PropertyInfo prop) => StaticGetter<object>(prop);

        public static Action<TValue> CreateStaticSetter<TValue>(this PropertyInfo prop) => StaticSetter<TValue>(prop);
        public static Action<object> CreateStaticSetterX(this PropertyInfo prop) => StaticSetter<object>(prop);
    }
}