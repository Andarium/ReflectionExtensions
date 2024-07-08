using NUnit.Framework;
using ReflectionExtensions.Tests.Classes;
using static ReflectionExtensions.ReflectionExtensions;

namespace ReflectionExtensions.Tests;

[TestFixture]
public sealed class TestMethodsExpression
{
    private const string FunctionString = "Function";
    private const string FunctionDouble = "Function2";
    private const string StaticFunctionString = "StaticFunction";

    [Test]
    public void Public_Function_Arg2_ReturnString()
    {
        var instance = new TestClass_Public();
        var f = CreateInstanceFunction<TestClass_Public, int, float, string>(FunctionString);
        var actual = f.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Function_Arg2_ReturnDouble()
    {
        var instance = new TestClass_Public();
        var f = CreateInstanceFunction<TestClass_Public, int, float, double>(FunctionDouble);
        var actual = f.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo(7870d));
    }

    [Test]
    public void Public_Const_Function_Arg2_ReturnString()
    {
        var instance = new TestClass_Public();
        var f = instance.CreateConstInstanceFunction<int, float, string>(FunctionString);
        var actual = f.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Const_Function_Arg2_ReturnDouble()
    {
        var instance = new TestClass_Public();
        var f = instance.CreateConstInstanceFunction<int, float, double>(FunctionDouble);
        var actual = f.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo(7870d));
    }

    [Test]
    public void Public_Function_Arg2_X_ReturnString()
    {
        var instance = new TestClass_Public();
        var f = typeof(TestClass_Public).CreateInstanceFunctionX(FunctionString, typeof(int), typeof(float));
        var actual = f.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Function_Arg2_X_ReturnDouble()
    {
        var instance = new TestClass_Public();
        var f = typeof(TestClass_Public).CreateInstanceFunctionX(FunctionDouble, typeof(int), typeof(float));
        var actual = f.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo(7870d));
    }

    [Test]
    public void Public_Const_Function_Arg2_X_ReturnString()
    {
        var instance = new TestClass_Public();
        var f = instance.CreateConstInstanceFunction(FunctionString, typeof(int), typeof(float));
        var actual = f.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Const_Function_Arg2_X_ReturnDouble()
    {
        var instance = new TestClass_Public();
        var f = instance.CreateConstInstanceFunction(FunctionDouble, typeof(int), typeof(float));
        var actual = f.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo(7870d));
    }

    [Test]
    public void Public_Static_Function_Arg2_ReturnString()
    {
        var f1 = CreateStaticFunction<TestClass_Public, int, float, string>(StaticFunctionString);
        var actual = f1.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        var f2 = typeof(TestClass_Public).CreateStaticFunction<int, float, string>(StaticFunctionString);
        actual = f2.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Const_Instance_Function_Arg2_ReturnString()
    {
        object instance = new TestClass_Public();
        var info = typeof(TestClass_Public).GetInstanceMethodInfo(FunctionString, typeof(int), typeof(float));
        var f1 = info.CreateConstInstanceFunctionR<string>(instance);

        // var f1 = CreateStaticFunction<TestClass_Public, int, float, string>(StaticFunctionString);
        var actual = f1.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // var f2 = typeof(TestClass_Public).CreateStaticFunction<int, float, string>(StaticFunctionString);
        // actual = f2.Invoke(10, 100f);
        // Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Instance_Function()
    {
        var instance = new TestClass_Public();

        // full generic
        var f1 = CreateInstanceFunction<TestClass_Public, int, float, string>(FunctionString);
        object actual = f1.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // X
        var f2 = typeof(TestClass_Public).CreateInstanceFunctionX(FunctionString, typeof(int), typeof(float));
        actual = f2.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // T
        var f3 = CreateInstanceFunctionT<TestClass_Public>(FunctionString, typeof(int), typeof(float));
        actual = f3.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // R
        var f4 = typeof(TestClass_Public).CreateInstanceFunctionR<string>(FunctionString, typeof(int), typeof(float));
        actual = f4.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // TR
        var f5 = CreateInstanceFunctionTR<TestClass_Public, string>(FunctionString, typeof(int), typeof(float));
        actual = f5.Invoke(instance, 10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Const_Instance_Function()
    {
        var instance = new TestClass_Public();
        object obj = instance;

        // full generic
        var f1 = instance.CreateConstInstanceFunction<int, float, string>(FunctionString);
        object actual = f1.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // X obj
        var f2 = obj.CreateConstInstanceFunction(FunctionString, typeof(int), typeof(float));
        actual = f2.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // X type
        var f3 = instance.CreateConstInstanceFunction(FunctionString, typeof(int), typeof(float));
        actual = f3.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // R obj
        var f4 = obj.CreateConstInstanceFunctionR<string>(FunctionString, typeof(int), typeof(float));
        actual = f4.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // R type
        var f5 = instance.CreateConstInstanceFunctionR<string>(FunctionString, typeof(int), typeof(float));
        actual = f5.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Static_Function()
    {
        var instance = new TestClass_Public();
        object obj = instance;

        // full generic
        var f1 = CreateStaticFunction<TestClass_Public, int, float, string>(StaticFunctionString);
        object actual = f1.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // X
        var f2 = typeof(TestClass_Public).CreateStaticFunctionX(StaticFunctionString, typeof(int), typeof(float));
        actual = f2.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // T
        var f3 = CreateStaticFunctionT<TestClass_Public>(StaticFunctionString, typeof(int), typeof(float));
        actual = f3.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // R
        var f4 = typeof(TestClass_Public).CreateStaticFunctionR<string>(StaticFunctionString, typeof(int), typeof(float));
        actual = f4.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));

        // TR
        var f5 = CreateStaticFunctionTR<TestClass_Public, string>(StaticFunctionString, typeof(int), typeof(float));
        actual = f5.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }
}