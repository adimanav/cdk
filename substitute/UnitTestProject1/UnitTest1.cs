using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using substitute;
using System.Security.Cryptography;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem(@"testfiles\inputfile1.txt")]
        [DeploymentItem(@"testfiles\inputdata1.txt")]
        [DeploymentItem(@"testfiles\outputfile1.txt")]
        public void TestMethod1()
        {
            string expectedOutputFile = "outputfile1.txt";
            string actualOutputFile = Guid.NewGuid() + ".txt";
            Substitute.Process(
                new[] {
                    "inputfile1.txt",
                    "inputdata1.txt",
                    actualOutputFile
                });

            var expected = File.ReadAllBytes(expectedOutputFile);
            var actual = File.ReadAllBytes(actualOutputFile);

            Assert.AreEqual<int>(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual<byte>(expected[i], actual[i]);
        }

        private byte[] GetMD5Hash(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }
    }
}
