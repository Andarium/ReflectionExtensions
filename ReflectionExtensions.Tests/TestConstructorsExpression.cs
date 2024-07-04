#pragma warning disable NUnit2023

using NUnit.Framework;
using ReflectionExtensions.Tests.Classes;
using static ReflectionExtensions.ReflectionExtensions;

namespace ReflectionExtensions.Tests;

[TestFixture]
public sealed class TestConstructorsExpression
{
    [Test]
    public void Public_Arg0()
    {
        var c = CreateConstructor<TestClass_Public>();
        Assert.That(c, Is.Not.Null);
        var actual = c.Invoke();
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Field, Is.EqualTo(777));
    }

    [Test]
    public void Public_Arg2()
    {
        var c = CreateConstructor<TestClass_Public, int, float>();
        Assert.That(c, Is.Not.Null);
        var actual = c.Invoke(333, 1001f);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Field, Is.EqualTo(333333));
    }

    [Test]
    public void Public_Arg0_XR()
    {
        var c = CreateConstructorX<TestClass_Public>();
        Assert.That(c, Is.Not.Null);
        var actual = c.Invoke();
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Field, Is.EqualTo(777));
    }

    [Test]
    public void Public_Arg2_XR()
    {
        var c = CreateConstructorX<TestClass_Public>(typeof(int), typeof(float));
        Assert.That(c, Is.Not.Null);
        var actual = c.Invoke(333, 1001f);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Field, Is.EqualTo(333333));
    }

    [Test]
    public void Public_Arg0_X()
    {
        var c = typeof(TestClass_Public).CreateConstructorX();
        Assert.That(c, Is.Not.Null);
        var actual = c.Invoke();
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.InstanceOf<TestClass_Public>());
        Assert.That(((TestClass_Public) actual).Field, Is.EqualTo(777));
    }

    [Test]
    public void Public_Arg2_X()
    {
        var c = typeof(TestClass_Public).CreateConstructorX(typeof(int), typeof(float));
        Assert.That(c, Is.Not.Null);
        var actual = c.Invoke(333, 1001f);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.InstanceOf<TestClass_Public>());
        Assert.That(((TestClass_Public) actual).Field, Is.EqualTo(333333));
    }
}