using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hashmap;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var map = new Hashmap<int, int>();

            for (int i = 0; i < 10000; i++)
            {
                map.Add(i, i);
            }

            for (int i = 0; i < 10000; i++)
            {
                Assert.AreEqual(map.Get(i), i);
            }
        }
    }
}
