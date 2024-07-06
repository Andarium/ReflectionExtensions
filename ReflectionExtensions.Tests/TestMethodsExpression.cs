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
        var f = instance.CreateConstInstanceFunction<TestClass_Public, int, float, string>(FunctionString);
        var actual = f.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Const_Function_Arg2_ReturnDouble()
    {
        var instance = new TestClass_Public();
        var f = instance.CreateConstInstanceFunction<TestClass_Public, int, float, double>(FunctionDouble);
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
        var f = instance.CreateConstInstanceFunctionX(FunctionString, typeof(int), typeof(float));
        var actual = f.Invoke(10, 100f);
        Assert.That(actual, Is.EqualTo("7870"));
    }

    [Test]
    public void Public_Const_Function_Arg2_X_ReturnDouble()
    {
        var instance = new TestClass_Public();
        var f = instance.CreateConstInstanceFunctionX(FunctionDouble, typeof(int), typeof(float));
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
}