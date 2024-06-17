// ReSharper disable CheckNamespace
// ReSharper disable RedundantNullableDirective

#nullable enable

using ReflectionExtensions.Tests.Classes;

namespace ReflectionExtensions.Tests
{
    [TestClass]
    public sealed class TestProperties
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
        public void Public_Instance_Property_Get()
        {
            var obj = new TestClass_Public();
            var actual = obj.GetInstanceProperty<int>("Property");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Public_Instance_Property_Set()
        {
            var obj = new TestClass_Public();
            obj.SetInstanceProperty("Property", 1000);
            Assert.AreEqual(1000, obj.Property);
        }

        [TestMethod]
        public void Private_Instance_Property_Get()
        {
            var obj = new TestClass_Private();
            var actual = obj.GetInstanceProperty<int>("Property");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Private_Instance_Property_Set()
        {
            var obj = new TestClass_Private();
            obj.SetInstanceProperty("Property", 1000);
            Assert.AreEqual(1000, obj.PropertyPublic);
        }

        [TestMethod]
        public void Public_Static_Property_Get()
        {
            var actual = typeof(TestClass_Public).GetStaticProperty<int>("StaticProperty");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Public_Static_Property_Set()
        {
            typeof(TestClass_Public).SetStaticProperty("StaticProperty", 1000);
            Assert.AreEqual(1000, TestClass_Public.StaticProperty);
        }
    
    
        [TestMethod]
        public void Private_Static_Property_Get()
        {
            var actual = typeof(TestClass_Private).GetStaticProperty<int>("StaticProperty");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Private_Static_Property_Set()
        {
            typeof(TestClass_Private).SetStaticProperty("StaticProperty", 1000);
            Assert.AreEqual(1000, TestClass_Private.StaticPropertyPublic);
        }
    }
}