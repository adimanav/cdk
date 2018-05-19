using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hashmap;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private const int numValues = 1000000;

        [TestMethod]
        public void TestHashmapForInts()
        {
            var map = new Hashmap<int, int>(numValues);

            for (int i = 0; i < numValues; i++)
            {
                map.Add(i, i);
            }

            for (int i = 0; i < numValues; i++)
            {
                Assert.AreEqual(i, map.Get(i));
            }

            for (int i = 0; i < numValues; i++)
            {
                map.Remove(i);
            }

            Assert.AreEqual(0, map.Count);
        }

        [TestMethod]
        public void TestHashmapForStrings()
        {
            var map = new Hashmap<string, string>(numValues);

            for (int i = 0; i < numValues; i++)
            {
                var tmp = i + string.Empty;
                map.Add(tmp, tmp);
            }

            for (int i = 0; i < numValues; i++)
            {
                var tmp = i + string.Empty;
                Assert.AreEqual(tmp, map.Get(tmp));
            }

            for (int i = 0; i < numValues; i++)
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

            for (int i = 0; i < numValues; i++)
            {
                var tmp = i + string.Empty;
                map.Add(tmp, tmp);
            }

            for (int i = 0; i < numValues; i++)
            {
                var tmp = i + string.Empty;
                Assert.AreEqual(tmp, map.Get(tmp));
            }

            for (int i = 0; i < numValues; i++)
            {
                var tmp = i + string.Empty;
                map.Remove(tmp);
            }

            Assert.AreEqual(0, map.Count);
        }
    }
}
