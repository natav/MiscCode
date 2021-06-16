using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Granicus.Legistar.Common.Security;

namespace UnitTestCryptography
{
    [TestClass]
    public class TestLegistarEncryption
    {
        [TestMethod]
        public void TestRijndaelMethod()
        {
            var bytes = System.Text.UTF8Encoding.UTF8.GetBytes("Hello!");
            var key_string = "thisisatestkey"; //"xXVo3QDHXvDqK3GcIIOT8lAl00rZqInoDeMWGaZsjJw9xwbtV7ZfCpYdQfS5gap";
            var MyDeriveBytes = new Rfc2898DeriveBytes(key_string, 32);
            var key = MyDeriveBytes.GetBytes(32);
            var iv = MyDeriveBytes.GetBytes(16);
            var ciphertext = LegistarEncryption.Rijndael(true, bytes, key, iv);
            var plaintext = LegistarEncryption.Rijndael(false, ciphertext, key, iv);

            for (var i = 0; i < bytes.Length; i++)
            {
                Assert.AreEqual(bytes[i], plaintext[i]);
            }
        }
    }
}
