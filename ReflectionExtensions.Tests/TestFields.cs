// ReSharper disable CheckNamespace
// ReSharper disable RedundantNullableDirective

#nullable enable

using NUnit.Framework;
using ReflectionExtensions.Tests.Classes;

namespace ReflectionExtensions.Tests
{
    [TestFixture]
    public sealed class TestFields
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
        public void Public_Instance_Field_Get()
        {
            var obj = new TestClass_Public();
            var actual = obj.GetInstanceField<int>("Field");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Public_Instance_Field_Set()
        {
            var obj = new TestClass_Public();
            obj.SetInstanceField("Field", 1000);
            Assert.That(obj.Field, Is.EqualTo(1000));
        }

        [Test]
        public void Private_Instance_Field_Get()
        {
            var obj = new TestClass_Private();
            var actual = obj.GetInstanceField<int>("Field");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Private_Instance_Field_Set()
        {
            var obj = new TestClass_Private();
            obj.SetInstanceField("Field", 1000);
            Assert.That(obj.FieldPublic, Is.EqualTo(1000));
        }

        [Test]
        public void Public_Static_Field_Get()
        {
            var actual = typeof(TestClass_Public).GetStaticField<int>("StaticField");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Public_Static_Field_Set()
        {
            typeof(TestClass_Public).SetStaticField("StaticField", 1000);
            Assert.That(TestClass_Public.StaticField, Is.EqualTo(1000));
        }

        [Test]
        public void Private_Static_Field_Get()
        {
            var actual = typeof(TestClass_Private).GetStaticField<int>("StaticField");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Private_Static_Field_Set()
        {
            typeof(TestClass_Private).SetStaticField("StaticField", 1000);
            Assert.That(TestClass_Private.StaticFieldPublic, Is.EqualTo(1000));
        }

        [Test]
        public void Private_Static_Field_Info_Existing()
        {
            var info = typeof(TestClass_Private).GetStaticFieldInfoOrNull("StaticField");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("StaticField"));
            Assert.That(info.IsStatic, Is.True);
            Assert.That(info.IsPrivate, Is.True);
        }

        [Test]
        public void Public_Static_Field_Info_Existing()
        {
            var info = typeof(TestClass_Public).GetStaticFieldInfoOrNull("StaticField");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("StaticField"));
            Assert.That(info.IsStatic, Is.True);
            Assert.That(info.IsPrivate, Is.False);
        }

        [Test]
        public void Private_Instance_Field_Info_Existing()
        {
            var info = typeof(TestClass_Private).GetInstanceFieldInfoOrNull("Field");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("Field"));
            Assert.That(info.IsStatic, Is.False);
            Assert.That(info.IsPrivate, Is.True);
        }

        [Test]
        public void Public_Instance_Field_Info_Existing()
        {
            var info = typeof(TestClass_Public).GetInstanceFieldInfoOrNull("Field");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("Field"));
            Assert.That(info.IsStatic, Is.False);
            Assert.That(info.IsPrivate, Is.False);
        }

        [Test]
        public void Private_Static_Field_Info_NonExisting()
        {
            var info = typeof(TestClass_Private).GetStaticFieldInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Public_Static_Field_Info_NonExisting()
        {
            var info = typeof(TestClass_Public).GetStaticFieldInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Private_Instance_Field_Info_NonExisting()
        {
            var info = typeof(TestClass_Private).GetInstanceFieldInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Public_Instance_Field_Info_NonExisting()
        {
            var info = typeof(TestClass_Public).GetInstanceFieldInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }
    }
}