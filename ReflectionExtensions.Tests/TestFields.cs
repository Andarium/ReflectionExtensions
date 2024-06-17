// ReSharper disable CheckNamespace
// ReSharper disable RedundantNullableDirective

#nullable enable

using ReflectionExtensions.Tests.Classes;

namespace ReflectionExtensions.Tests
{
    [TestClass]
    public sealed class TestFields
    {
        private const int ExpectedInt = 777;

        [TestInitialize]
        public void TestInitialize()
        {
            ReflectionExtensions.ClearCache();
            TestClass_Private.Reset();
            TestClass_Public.Reset();
        }

        [TestMethod]
        public void Public_Instance_Field_Get()
        {
            var obj = new TestClass_Public();
            var actual = obj.GetInstanceField<int>("Field");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Public_Instance_Field_Set()
        {
            var obj = new TestClass_Public();
            obj.SetInstanceField("Field", 1000);
            Assert.AreEqual(1000, obj.Field);
        }

        [TestMethod]
        public void Private_Instance_Field_Get()
        {
            var obj = new TestClass_Private();
            var actual = obj.GetInstanceField<int>("Field");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Private_Instance_Field_Set()
        {
            var obj = new TestClass_Private();
            obj.SetInstanceField("Field", 1000);
            Assert.AreEqual(1000, obj.FieldPublic);
        }

        [TestMethod]
        public void Public_Static_Field_Get()
        {
            var actual = typeof(TestClass_Public).GetStaticField<int>("StaticField");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Public_Static_Field_Set()
        {
            typeof(TestClass_Public).SetStaticField("StaticField", 1000);
            Assert.AreEqual(1000, TestClass_Public.StaticField);
        }
    
    
        [TestMethod]
        public void Private_Static_Field_Get()
        {
            var actual = typeof(TestClass_Private).GetStaticField<int>("StaticField");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Private_Static_Field_Set()
        {
            typeof(TestClass_Private).SetStaticField("StaticField", 1000);
            Assert.AreEqual(1000, TestClass_Private.StaticFieldPublic);
        }
    }
}