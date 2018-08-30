using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Assert
            Assert.AreEqual("Hello", "Hello");
        }
        [TestMethod]
        public void TestMethod2()
        {
            // Assert
            Assert.AreEqual("Bonjour", "Bonjour");
        }

    }
}
