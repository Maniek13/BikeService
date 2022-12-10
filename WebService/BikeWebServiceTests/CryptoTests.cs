using BikeWebService.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeWebServiceTests
{
    [TestClass]
    public class CryptoTests
    {
        [TestMethod]
        public void EncryptSha256()
        {
            string res = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";
            string testString = "test";

            Assert.AreEqual(res, Crypto.EncryptSha256(testString));
        }
    }
}
