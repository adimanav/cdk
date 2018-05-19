using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hashmap;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestHashmapForInts()
        {
            var map = new Hashmap<int, int>(1000000);

            for (int i = 0; i < 1000000; i++)
            {
                map.Add(i, i);
            }

            for (int i = 0; i < 1000000; i++)
            {
                Assert.AreEqual(map.Get(i), i);
            }

            for (int i = 0; i < 1000000; i++)
            {
                map.Remove(i);
            }

            Assert.AreEqual(0, map.Count);
        }

        [TestMethod]
        public void TestHashmapForStrings()
        {
            var map = new Hashmap<string, string>(1000000);

            for (int i = 0; i < 1000000; i++)
            {
                var tmp = i + string.Empty;
                map.Add(tmp, tmp);
            }

            for (int i = 0; i < 1000000; i++)
            {
                var tmp = i + string.Empty;
                Assert.AreEqual(map.Get(tmp), tmp);
            }

            for (int i = 0; i < 1000000; i++)
            {
                var tmp = i + string.Empty;
                map.Remove(tmp);
            }

            Assert.AreEqual(0, map.Count);
        }


        [TestMethod]
        public void TestTrie()
        {
            var map = new Trie<string>();

            for (int i = 0; i < 1000000; i++)
            {
                var tmp = i + string.Empty;
                map.Add(tmp, tmp);
            }

            for (int i = 0; i < 1000000; i++)
            {
                var tmp = i + string.Empty;
                Assert.AreEqual(tmp, map.Get(tmp));
            }

            for (int i = 0; i < 1000000; i++)
            {
                var tmp = i + string.Empty;
                map.Remove(tmp);
            }

            Assert.AreEqual(0, map.Count);
        }
    }
}
