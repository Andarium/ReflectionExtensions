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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TValue> CreateConstInstanceGetter<TValue>(this PropertyInfo prop, object constInstance) => ConstInstanceGetter<TValue>(prop, constInstance);

        public static Func<object> CreateConstInstanceGetter(this PropertyInfo prop, object constInstance) => ConstInstanceGetter<object>(prop, constInstance);

        public static Action<TValue> CreateConstInstanceSetter<TValue>(this PropertyInfo prop, object constInstance) => ConstInstanceSetter<TValue>(prop, constInstance);
        public static Action<object> CreateConstInstanceSetter(this PropertyInfo prop, object constInstance) => ConstInstanceSetter<object>(prop, constInstance);

        ////////////////////////////////
        //////////   Static   //////////
        ////////////////////////////////

        public static Func<TValue> CreateStaticGetter<TValue>(this PropertyInfo prop) => StaticGetter<TValue>(prop);
        public static Func<object> CreateStaticGetterX(this PropertyInfo prop) => StaticGetter<object>(prop);

        public static Action<TValue> CreateStaticSetter<TValue>(this PropertyInfo prop) => StaticSetter<TValue>(prop);
        public static Action<object> CreateStaticSetterX(this PropertyInfo prop) => StaticSetter<object>(prop);
    }
}