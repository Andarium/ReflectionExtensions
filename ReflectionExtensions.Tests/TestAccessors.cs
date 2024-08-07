﻿using NUnit.Framework;
using ReflectionExtensions.Tests.Classes;
using static ReflectionExtensions.ReflectionExtensions;
using TestClass = ReflectionExtensions.Tests.Classes.ExpressionTestClass;

namespace ReflectionExtensions.Tests;

[TestFixture]
public sealed class TestAccessors
{
    private TestClass _instance = default!;

    private InstanceAccessor<int> _instanceValueField = default!;
    private InstanceAccessor<TestObject> _instanceRefField = default!;
    private InstanceAccessor<TestClass, int> _instanceValueProperty = default!;
    private InstanceAccessor<TestClass, TestObject> _instanceRefProperty = default!;

    private ConstAccessor<int> _constInstanceValueField = default!;
    private ConstAccessor<TestObject> _constInstanceRefField = default!;
    private ConstAccessor<int> _constInstanceValueProperty = default!;
    private ConstAccessor<TestObject> _constInstanceRefProperty = default!;

    private ConstAccessor<int> _staticValueField = default!;
    private ConstAccessor<TestObject> _staticRefField = default!;
    private ConstAccessor<int> _staticValueProperty = default!;
    private ConstAccessor<TestObject> _staticRefProperty = default!;

    private ConstAccessorX _staticValueFieldX1 = default!;
    private ConstAccessorX _staticValueFieldX2 = default!;

    [SetUp]
    public void Setup()
    {
        TestClass.Reset();
        _instance = new();
        var t = typeof(TestClass);

        _instanceValueField = t.CreateInstanceAccessor<int>("ValueField");
        _instanceRefField = t.CreateInstanceAccessor<TestObject>("RefField");
        _instanceValueProperty = CreateInstanceAccessor<TestClass, int>("ValueProperty");
        _instanceRefProperty = CreateInstanceAccessor<TestClass, TestObject>("RefProperty");

        _constInstanceValueField = _instance.CreateConstInstanceAccessor<int>("ValueField");
        _constInstanceRefField = _instance.CreateConstInstanceAccessor<TestObject>("RefField");
        _constInstanceValueProperty = _instance.CreateConstInstanceAccessor<int>("ValueProperty");
        _constInstanceRefProperty = _instance.CreateConstInstanceAccessor<TestObject>("RefProperty");

        _staticValueField = CreateStaticAccessor<TestClass, int>("StaticValueField");
        _staticRefField = t.CreateStaticAccessor<TestObject>("StaticRefField");
        _staticValueProperty = t.CreateStaticAccessor<int>("StaticValueProperty");
        _staticRefProperty = t.CreateStaticAccessor<TestObject>("StaticRefProperty");

        _staticValueFieldX1 = t.CreateStaticAccessor("StaticValueField");
        _staticValueFieldX2 = CreateStaticAccessor<TestClass>("StaticValueField");
    }

    [Test]
    public void Instance_Field()
    {
        Assert.That(_instance.ValueField, Is.EqualTo(55));
        Assert.That(_instanceValueField.GetValue(_instance), Is.EqualTo(55));

        _instanceValueField.SetValue(_instance, 10);

        Assert.That(_instance.ValueField, Is.EqualTo(10));
        Assert.That(_instanceValueField.GetValue(_instance), Is.EqualTo(10));

        _instance.ValueField = 20;

        Assert.That(_instance.ValueField, Is.EqualTo(20));
        Assert.That(_instanceValueField.GetValue(_instance), Is.EqualTo(20));
    }

    [Test]
    public void Instance_Field_Ref()
    {
        Assert.That(_instance.RefField, Is.Not.Null);
        Assert.That(_instanceRefField.GetValue(_instance), Is.EqualTo(_instance.RefField));

        var newValue = new TestObject();
        _instanceRefField.SetValue(_instance, newValue);

        Assert.That(_instance.RefField, Is.Not.Null);
        Assert.That(_instanceRefField.GetValue(_instance), Is.EqualTo(_instance.RefField));

        newValue = new TestObject();
        _instance.RefField = newValue;

        Assert.That(_instance.RefField, Is.Not.Null);
        Assert.That(_instanceRefField.GetValue(_instance), Is.EqualTo(_instance.RefField));
    }

    [Test]
    public void Instance_Property()
    {
        Assert.That(_instance.ValueProperty, Is.EqualTo(66));
        Assert.That(_instanceValueProperty.GetValue(_instance), Is.EqualTo(66));

        _instanceValueProperty.SetValue(_instance, 10);

        Assert.That(_instance.ValueProperty, Is.EqualTo(10));
        Assert.That(_instanceValueProperty.GetValue(_instance), Is.EqualTo(10));

        _instance.ValueProperty = 20;

        Assert.That(_instance.ValueProperty, Is.EqualTo(20));
        Assert.That(_instanceValueProperty.GetValue(_instance), Is.EqualTo(20));
    }

    [Test]
    public void Instance_Property_Ref()
    {
        Assert.That(_instance.RefProperty, Is.Not.Null);
        Assert.That(_instanceRefProperty.GetValue(_instance), Is.EqualTo(_instance.RefProperty));

        var newValue = new TestObject();
        _instanceRefProperty.SetValue(_instance, newValue);

        Assert.That(_instance.RefProperty, Is.Not.Null);
        Assert.That(_instanceRefProperty.GetValue(_instance), Is.EqualTo(_instance.RefProperty));

        newValue = new TestObject();
        _instance.RefProperty = newValue;

        Assert.That(_instance.RefProperty, Is.Not.Null);
        Assert.That(_instanceRefProperty.GetValue(_instance), Is.EqualTo(_instance.RefProperty));
    }

    // Const

    [Test]
    public void Const_Field()
    {
        Assert.That(_instance.ValueField, Is.EqualTo(55));
        Assert.That(_constInstanceValueField.GetValue(), Is.EqualTo(55));

        _constInstanceValueField.SetValue(10);

        Assert.That(_instance.ValueField, Is.EqualTo(10));
        Assert.That(_constInstanceValueField.GetValue(), Is.EqualTo(10));

        _instance.ValueField = 20;

        Assert.That(_instance.ValueField, Is.EqualTo(20));
        Assert.That(_constInstanceValueField.GetValue(), Is.EqualTo(20));
    }

    [Test]
    public void Const_Field_Ref()
    {
        Assert.That(_instance.RefField, Is.Not.Null);
        Assert.That(_constInstanceRefField.GetValue(), Is.EqualTo(_instance.RefField));

        var newValue = new TestObject();
        _constInstanceRefField.SetValue(newValue);

        Assert.That(_instance.RefField, Is.Not.Null);
        Assert.That(_constInstanceRefField.GetValue(), Is.EqualTo(_instance.RefField));

        newValue = new TestObject();
        _instance.RefField = newValue;

        Assert.That(_instance.RefField, Is.Not.Null);
        Assert.That(_constInstanceRefField.GetValue(), Is.EqualTo(_instance.RefField));
    }

    [Test]
    public void Const_Property()
    {
        Assert.That(_instance.ValueProperty, Is.EqualTo(66));
        Assert.That(_constInstanceValueProperty.GetValue(), Is.EqualTo(66));

        _constInstanceValueProperty.SetValue(10);

        Assert.That(_instance.ValueProperty, Is.EqualTo(10));
        Assert.That(_constInstanceValueProperty.GetValue(), Is.EqualTo(10));

        _instance.ValueProperty = 20;

        Assert.That(_instance.ValueProperty, Is.EqualTo(20));
        Assert.That(_constInstanceValueProperty.GetValue(), Is.EqualTo(20));
    }

    [Test]
    public void Const_Property_Ref()
    {
        Assert.That(_instance.RefProperty, Is.Not.Null);
        Assert.That(_constInstanceRefProperty.GetValue(), Is.EqualTo(_instance.RefProperty));

        var newValue = new TestObject();
        _constInstanceRefProperty.SetValue(newValue);

        Assert.That(_instance.RefProperty, Is.Not.Null);
        Assert.That(_constInstanceRefProperty.GetValue(), Is.EqualTo(_instance.RefProperty));

        newValue = new TestObject();
        _instance.RefProperty = newValue;

        Assert.That(_instance.RefProperty, Is.Not.Null);
        Assert.That(_constInstanceRefProperty.GetValue(), Is.EqualTo(_instance.RefProperty));
    }

    // Static

    [Test]
    public void Static_Field()
    {
        Assert.That(TestClass.StaticValueField, Is.EqualTo(77));
        Assert.That(_staticValueField.GetValue(), Is.EqualTo(77));

        _staticValueField.SetValue(10);

        Assert.That(TestClass.StaticValueField, Is.EqualTo(10));
        Assert.That(_staticValueField.GetValue(), Is.EqualTo(10));

        TestClass.StaticValueField = 20;

        Assert.That(TestClass.StaticValueField, Is.EqualTo(20));
        Assert.That(_staticValueField.GetValue(), Is.EqualTo(20));
    }

    [Test]
    public void Static_Field_X1()
    {
        Assert.That(TestClass.StaticValueField, Is.EqualTo(77));
        Assert.That(_staticValueFieldX1.GetValue(), Is.EqualTo(77));

        _staticValueField.SetValue(10);

        Assert.That(TestClass.StaticValueField, Is.EqualTo(10));
        Assert.That(_staticValueFieldX1.GetValue(), Is.EqualTo(10));

        TestClass.StaticValueField = 20;

        Assert.That(TestClass.StaticValueField, Is.EqualTo(20));
        Assert.That(_staticValueFieldX1.GetValue(), Is.EqualTo(20));
    }

    [Test]
    public void Static_Field_X2()
    {
        Assert.That(TestClass.StaticValueField, Is.EqualTo(77));
        Assert.That(_staticValueFieldX2.GetValue(), Is.EqualTo(77));

        _staticValueField.SetValue(10);

        Assert.That(TestClass.StaticValueField, Is.EqualTo(10));
        Assert.That(_staticValueFieldX2.GetValue(), Is.EqualTo(10));

        TestClass.StaticValueField = 20;

        Assert.That(TestClass.StaticValueField, Is.EqualTo(20));
        Assert.That(_staticValueFieldX2.GetValue(), Is.EqualTo(20));
    }

    [Test]
    public void Static_Field_Ref()
    {
        Assert.That(TestClass.StaticRefField, Is.Not.Null);
        Assert.That(_staticRefField.GetValue(), Is.EqualTo(TestClass.StaticRefField));

        var newValue = new TestObject();
        _staticRefField.SetValue(newValue);

        Assert.That(TestClass.StaticRefField, Is.Not.Null);
        Assert.That(_staticRefField.GetValue(), Is.EqualTo(TestClass.StaticRefField));

        newValue = new TestObject();
        TestClass.StaticRefField = newValue;

        Assert.That(TestClass.StaticRefField, Is.Not.Null);
        Assert.That(_staticRefField.GetValue(), Is.EqualTo(TestClass.StaticRefField));
    }

    [Test]
    public void Static_Property()
    {
        Assert.That(TestClass.StaticValueProperty, Is.EqualTo(88));
        Assert.That(_staticValueProperty.GetValue(), Is.EqualTo(88));

        _staticValueProperty.SetValue(10);

        Assert.That(TestClass.StaticValueProperty, Is.EqualTo(10));
        Assert.That(_staticValueProperty.GetValue(), Is.EqualTo(10));

        TestClass.StaticValueProperty = 20;

        Assert.That(TestClass.StaticValueProperty, Is.EqualTo(20));
        Assert.That(_staticValueProperty.GetValue(), Is.EqualTo(20));
    }

    [Test]
    public void Static_Property_Ref()
    {
        Assert.That(TestClass.StaticRefProperty, Is.Not.Null);
        Assert.That(_staticRefProperty.GetValue(), Is.EqualTo(TestClass.StaticRefProperty));

        var newValue = new TestObject();
        _staticRefProperty.SetValue(newValue);

        Assert.That(TestClass.StaticRefProperty, Is.Not.Null);
        Assert.That(_staticRefProperty.GetValue(), Is.EqualTo(TestClass.StaticRefProperty));

        newValue = new TestObject();
        TestClass.StaticRefProperty = newValue;

        Assert.That(TestClass.StaticRefProperty, Is.Not.Null);
        Assert.That(_staticRefProperty.GetValue(), Is.EqualTo(TestClass.StaticRefProperty));
    }
}