// ReSharper disable CheckNamespace
// ReSharper disable RedundantNullableDirective

#nullable enable

using NUnit.Framework;
using ReflectionExtensions.Tests.Classes;

namespace ReflectionExtensions.Tests
{
    [TestFixture]
    public sealed class TestProperties
    {
        private const int ExpectedInt = 777;

        [SetUp]
        public void TestInitialize()
        {
            ReflectionExtensions.ClearCache();
            TestClass_Private.Reset();
            TestClass_Public.Reset();
        }

        [Test]
        public void Public_Instance_Property_Get()
        {
            var obj = new TestClass_Public();
            var actual = obj.GetInstanceProperty<int>("Property");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Public_Instance_Property_Set()
        {
            var obj = new TestClass_Public();
            obj.SetInstanceProperty("Property", 1000);
            Assert.That(obj.Property, Is.EqualTo(1000));
        }

        [Test]
        public void Private_Instance_Property_Get()
        {
            var obj = new TestClass_Private();
            var actual = obj.GetInstanceProperty<int>("Property");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Private_Instance_Property_Set()
        {
            var obj = new TestClass_Private();
            obj.SetInstanceProperty("Property", 1000);
            Assert.That(obj.PropertyPublic, Is.EqualTo(1000));
        }

        [Test]
        public void Public_Static_Property_Get()
        {
            var actual = typeof(TestClass_Public).GetStaticProperty<int>("StaticProperty");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Public_Static_Property_Set()
        {
            typeof(TestClass_Public).SetStaticProperty("StaticProperty", 1000);
            Assert.That(TestClass_Public.StaticProperty, Is.EqualTo(1000));
        }

        [Test]
        public void Private_Static_Property_Get()
        {
            var actual = typeof(TestClass_Private).GetStaticProperty<int>("StaticProperty");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Private_Static_Property_Set()
        {
            typeof(TestClass_Private).SetStaticProperty("StaticProperty", 1000);
            Assert.That(TestClass_Private.StaticPropertyPublic, Is.EqualTo(1000));
        }

        [Test]
        public void Private_Static_Property_Info_Existing()
        {
            var info = typeof(TestClass_Private).GetStaticPropertyInfoOrNull("StaticProperty");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("StaticProperty"));
            Assert.That(info.GetMethod.IsStatic, Is.True);
            Assert.That(info.GetMethod.IsPrivate, Is.True);
        }

        [Test]
        public void Public_Static_Property_Info_Existing()
        {
            var info = typeof(TestClass_Public).GetStaticPropertyInfoOrNull("StaticProperty");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("StaticProperty"));
            Assert.That(info.GetMethod.IsStatic, Is.True);
            Assert.That(info.GetMethod.IsPrivate, Is.False);
        }

        [Test]
        public void Private_Instance_Property_Info_Existing()
        {
            var info = typeof(TestClass_Private).GetInstancePropertyInfoOrNull("Property");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("Property"));
            Assert.That(info.GetMethod.IsStatic, Is.False);
            Assert.That(info.GetMethod.IsPrivate, Is.True);
        }

        [Test]
        public void Public_Instance_Property_Info_Existing()
        {
            var info = typeof(TestClass_Public).GetInstancePropertyInfoOrNull("Property");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("Property"));
            Assert.That(info.GetMethod.IsStatic, Is.False);
            Assert.That(info.GetMethod.IsPrivate, Is.False);
        }

        [Test]
        public void Private_Static_Field_Info_NonExisting()
        {
            var info = typeof(TestClass_Private).GetStaticPropertyInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Public_Static_Property_Info_NonExisting()
        {
            var info = typeof(TestClass_Public).GetStaticPropertyInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Private_Instance_Property_Info_NonExisting()
        {
            var info = typeof(TestClass_Private).GetStaticPropertyInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Public_Instance_Property_Info_NonExisting()
        {
            var info = typeof(TestClass_Public).GetStaticPropertyInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }
    }
}