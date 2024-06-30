// ReSharper disable CheckNamespace
// ReSharper disable RedundantNullableDirective

#nullable enable

using NUnit.Framework;
using ReflectionExtensions.Tests.Classes;

namespace ReflectionExtensions.Tests
{
    [TestFixture]
    public sealed class TestMethods
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
        public void Public_Instance_Method_No_Args()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out int actual, "Method");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Public_Instance_Method_Arg1()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out int actual, "Method", Args.Of(7));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7));
        }

        [Test]
        public void Public_Instance_Method_Arg2()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out int actual, "Method", Args.Of(7, 10));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7 + 10));
        }

        [Test]
        public void Public_Instance_VoidMethod()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out object actual, "VoidMethod");
            Assert.That(actual, Is.Null);
            Assert.That(TestClass_Public.ChangedBy, Is.EqualTo("VoidMethod"));
        }

        [Test]
        public void Public_Static_Method_No_Args()
        {
            typeof(TestClass_Public).CallStaticMethod(out int actual, "StaticMethod");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Public_Static_Method_Args1()
        {
            typeof(TestClass_Public).CallStaticMethod(out int actual, "StaticMethod", Args.Of(7));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7));
        }

        [Test]
        public void Public_Static_Method_Args2()
        {
            typeof(TestClass_Public).CallStaticMethod(out int actual, "StaticMethod", Args.Of(7, 10));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7 + 10));
        }

        [Test]
        public void Public_Static_VoidMethod()
        {
            typeof(TestClass_Public).CallStaticMethod(out object actual, "StaticVoidMethod");
            Assert.That(actual, Is.Null);
            Assert.That(TestClass_Public.ChangedBy, Is.EqualTo("StaticVoidMethod"));
        }

        [Test]
        public void Private_Instance_Method_No_Args()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out int actual, "Method");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Private_Instance_Method_Arg1()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out int actual, "Method", Args.Of(7));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7));
        }

        [Test]
        public void Private_Instance_Method_Arg2()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out int actual, "Method", Args.Of(7, 10));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7 + 10));
        }

        [Test]
        public void Private_Instance_VoidMethod()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out object actual, "VoidMethod");
            Assert.That(actual, Is.Null);
            Assert.That(TestClass_Private.ChangedBy, Is.EqualTo("VoidMethod"));
        }

        [Test]
        public void Private_Static_Method_No_Args()
        {
            typeof(TestClass_Private).CallStaticMethod(out int actual, "StaticMethod");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Private_Static_Method_Args1()
        {
            typeof(TestClass_Private).CallStaticMethod(out int actual, "StaticMethod", Args.Of(7));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7));
        }

        [Test]
        public void Private_Static_Method_Args2()
        {
            typeof(TestClass_Private).CallStaticMethod(out int actual, "StaticMethod", Args.Of(7, 10));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7 + 10));
        }

        [Test]
        public void Private_Static_VoidMethod()
        {
            typeof(TestClass_Private).CallStaticMethod(out object actual, "StaticVoidMethod");
            Assert.That(actual, Is.Null);
            Assert.That(TestClass_Private.ChangedBy, Is.EqualTo("StaticVoidMethod"));
        }

        [Test]
        public void Private_Static_Method_Info_Existing()
        {
            var info = typeof(TestClass_Private).GetStaticMethodInfoOrNull("StaticMethod");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("StaticMethod"));
            Assert.That(info.IsStatic, Is.True);
            Assert.That(info.IsPrivate, Is.True);
        }

        [Test]
        public void Public_Static_Method_Info_Existing()
        {
            var info = typeof(TestClass_Public).GetStaticMethodInfoOrNull("StaticMethod");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("StaticMethod"));
            Assert.That(info.IsStatic, Is.True);
            Assert.That(info.IsPrivate, Is.False);
        }

        [Test]
        public void Private_Instance_Method_Info_Existing()
        {
            var info = typeof(TestClass_Private).GetInstanceMethodInfoOrNull("Method");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("Method"));
            Assert.That(info.IsStatic, Is.False);
            Assert.That(info.IsPrivate, Is.True);
        }

        [Test]
        public void Public_Instance_Method_Info_Existing()
        {
            var info = typeof(TestClass_Public).GetInstanceMethodInfoOrNull("Method");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("Method"));
            Assert.That(info.IsStatic, Is.False);
            Assert.That(info.IsPrivate, Is.False);
        }

        [Test]
        public void Private_Static_Method_Info_NonExisting()
        {
            var info = typeof(TestClass_Private).GetStaticMethodInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Public_Static_Method_Info_NonExisting()
        {
            var info = typeof(TestClass_Public).GetStaticMethodInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Private_Instance_Method_Info_NonExisting()
        {
            var info = typeof(TestClass_Private).GetInstanceMethodInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Public_Instance_Method_Info_NonExisting()
        {
            var info = typeof(TestClass_Public).GetInstanceMethodInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }
    }
}