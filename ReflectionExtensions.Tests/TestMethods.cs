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
        public void Public_Instance_Function_No_Args()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out int actual, "Function");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Public_Instance_Function_Arg1()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out int actual, "Function", Args.Of(7));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7));
        }

        [Test]
        public void Public_Instance_Function_Arg2()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out string actual, "Function", Args.Of(7, 10f));
            Assert.That(actual, Is.EqualTo("5449"));
        }

        [Test]
        public void Public_Instance_Procedure()
        {
            var obj = new TestClass_Public();
            obj.CallInstanceMethod(out object actual, "Procedure");
            Assert.That(actual, Is.Null);
            Assert.That(TestClass_Public.ChangedBy, Is.EqualTo("Procedure"));
        }

        [Test]
        public void Public_Static_Function_No_Args()
        {
            typeof(TestClass_Public).CallStaticMethod(out int actual, "StaticFunction");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Public_Static_Function_Args1()
        {
            typeof(TestClass_Public).CallStaticMethod(out int actual, "StaticFunction", Args.Of(7));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7));
        }

        [Test]
        public void Public_Static_Function_Args2()
        {
            typeof(TestClass_Public).CallStaticMethod(out string actual, "StaticFunction", Args.Of(7, 10f));
            Assert.That(actual, Is.EqualTo("5449"));
        }

        [Test]
        public void Public_Static_Procedure()
        {
            typeof(TestClass_Public).CallStaticMethod(out object actual, "StaticProcedure");
            Assert.That(actual, Is.Null);
            Assert.That(TestClass_Public.ChangedBy, Is.EqualTo("StaticProcedure"));
        }

        [Test]
        public void Private_Instance_Function_No_Args()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out int actual, "Function");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Private_Instance_Function_Arg1()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out int actual, "Function", Args.Of(7));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7));
        }

        [Test]
        public void Private_Instance_Function_Arg2()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out string actual, "Function", Args.Of(7, 10f));
            Assert.That(actual, Is.EqualTo("5449"));
        }

        [Test]
        public void Private_Instance_Procedure()
        {
            var obj = new TestClass_Private();
            obj.CallInstanceMethod(out object actual, "Procedure");
            Assert.That(actual, Is.Null);
            Assert.That(TestClass_Private.ChangedBy, Is.EqualTo("Procedure"));
        }

        [Test]
        public void Private_Static_Function_No_Args()
        {
            typeof(TestClass_Private).CallStaticMethod(out int actual, "StaticFunction");
            Assert.That(actual, Is.EqualTo(ExpectedInt));
        }

        [Test]
        public void Private_Static_Function_Args1()
        {
            typeof(TestClass_Private).CallStaticMethod(out int actual, "StaticFunction", Args.Of(7));
            Assert.That(actual, Is.EqualTo(ExpectedInt * 7));
        }

        [Test]
        public void Private_Static_Function_Args2()
        {
            typeof(TestClass_Private).CallStaticMethod(out string actual, "StaticFunction", Args.Of(7, 10f));
            Assert.That(actual, Is.EqualTo("5449"));
        }

        [Test]
        public void Private_Static_Procedure()
        {
            typeof(TestClass_Private).CallStaticMethod(out object actual, "StaticProcedure");
            Assert.That(actual, Is.Null);
            Assert.That(TestClass_Private.ChangedBy, Is.EqualTo("StaticProcedure"));
        }

        [Test]
        public void Private_Static_Function_Info_Existing()
        {
            var info = typeof(TestClass_Private).GetStaticMethodInfoOrNull("StaticFunction");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("StaticFunction"));
            Assert.That(info.IsStatic, Is.True);
            Assert.That(info.IsPrivate, Is.True);
        }

        [Test]
        public void Public_Static_Function_Info_Existing()
        {
            var info = typeof(TestClass_Public).GetStaticMethodInfoOrNull("StaticFunction");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("StaticFunction"));
            Assert.That(info.IsStatic, Is.True);
            Assert.That(info.IsPrivate, Is.False);
        }

        [Test]
        public void Private_Instance_Function_Info_Existing()
        {
            var info = typeof(TestClass_Private).GetInstanceMethodInfoOrNull("Function");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("Function"));
            Assert.That(info.IsStatic, Is.False);
            Assert.That(info.IsPrivate, Is.True);
        }

        [Test]
        public void Public_Instance_Function_Info_Existing()
        {
            var info = typeof(TestClass_Public).GetInstanceMethodInfoOrNull("Function");
            Assert.That(info, Is.Not.Null);
            Assert.That(info.Name, Is.EqualTo("Function"));
            Assert.That(info.IsStatic, Is.False);
            Assert.That(info.IsPrivate, Is.False);
        }

        [Test]
        public void Private_Static_Function_Info_NonExisting()
        {
            var info = typeof(TestClass_Private).GetStaticMethodInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Public_Static_Function_Info_NonExisting()
        {
            var info = typeof(TestClass_Public).GetStaticMethodInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Private_Instance_Function_Info_NonExisting()
        {
            var info = typeof(TestClass_Private).GetInstanceMethodInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }

        [Test]
        public void Public_Instance_Function_Info_NonExisting()
        {
            var info = typeof(TestClass_Public).GetInstanceMethodInfoOrNull("vvvvvv");
            Assert.That(info, Is.Null);
        }
    }
}