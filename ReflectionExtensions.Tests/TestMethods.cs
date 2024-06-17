// ReSharper disable CheckNamespace
// ReSharper disable RedundantNullableDirective

#nullable enable

using ReflectionExtensions.Tests.Classes;

namespace ReflectionExtensions.Tests
{
    [TestClass]
    public sealed class TestMethods
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
        public void Public_Instance_Method_No_Args()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out int actual, "Method");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Public_Instance_Method_Arg1()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out int actual, "Method", 7);
            Assert.AreEqual(ExpectedInt * 7, actual);
        }

        [TestMethod]
        public void Public_Instance_Method_Arg2()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out int actual, "Method", 7, 10);
            Assert.AreEqual(ExpectedInt * 7 + 10, actual);
        }

        [TestMethod]
        public void Public_Instance_VoidMethod()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out object actual, "VoidMethod");
            Assert.IsNull(actual);
            Assert.AreEqual("VoidMethod", TestClass_Public.ChangedBy);
        }

        [TestMethod]
        public void Public_Static_Method_No_Args()
        {
            typeof(TestClass_Public).CallStaticMethod(out int actual, "StaticMethod");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Public_Static_Method_Args1()
        {
            typeof(TestClass_Public).CallStaticMethod(out int actual, "StaticMethod", 7);
            Assert.AreEqual(ExpectedInt * 7, actual);
        }

        [TestMethod]
        public void Public_Static_Method_Args2()
        {
            typeof(TestClass_Public).CallStaticMethod(out int actual, "StaticMethod", 7, 10);
            Assert.AreEqual(ExpectedInt * 7 + 10, actual);
        }

        [TestMethod]
        public void Public_Static_VoidMethod()
        {
            typeof(TestClass_Public).CallStaticMethod(out object actual, "StaticVoidMethod");
            Assert.IsNull(actual);
            Assert.AreEqual("StaticVoidMethod", TestClass_Public.ChangedBy);
        }

        [TestMethod]
        public void Private_Instance_Method_No_Args()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out int actual, "Method");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Private_Instance_Method_Arg1()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out int actual, "Method", 7);
            Assert.AreEqual(ExpectedInt * 7, actual);
        }

        [TestMethod]
        public void Private_Instance_Method_Arg2()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out int actual, "Method", 7, 10);
            Assert.AreEqual(ExpectedInt * 7 + 10, actual);
        }

        [TestMethod]
        public void Private_Instance_VoidMethod()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out object actual, "VoidMethod");
            Assert.IsNull(actual);
            Assert.AreEqual("VoidMethod", TestClass_Private.ChangedBy);
        }

        [TestMethod]
        public void Private_Static_Method_No_Args()
        {
            typeof(TestClass_Private).CallStaticMethod(out int actual, "StaticMethod");
            Assert.AreEqual(ExpectedInt, actual);
        }

        [TestMethod]
        public void Private_Static_Method_Args1()
        {
            typeof(TestClass_Private).CallStaticMethod(out int actual, "StaticMethod", 7);
            Assert.AreEqual(ExpectedInt * 7, actual);
        }

        [TestMethod]
        public void Private_Static_Method_Args2()
        {
            typeof(TestClass_Private).CallStaticMethod(out int actual, "StaticMethod", 7, 10);
            Assert.AreEqual(ExpectedInt * 7 + 10, actual);
        }

        [TestMethod]
        public void Private_Static_VoidMethod()
        {
            typeof(TestClass_Private).CallStaticMethod(out object actual, "StaticVoidMethod");
            Assert.IsNull(actual);
            Assert.AreEqual("StaticVoidMethod", TestClass_Private.ChangedBy);
        }
    }
}