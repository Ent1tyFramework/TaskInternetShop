using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;

namespace InternetShop.Common.Encryption
{
    public class Encryptor
    {
        private string key;
        private RSACryptoServiceProvider rsa;

        public Encryptor()
        {
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = "EncryptKey";

            rsa = new RSACryptoServiceProvider(csp);
        }

        public string Encrypt(string value)
              => Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(value), true));

        public string Decrypt(string value)
              => Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(value), true));

        public string ShowKey()
           => rsa.ToXmlString(true);

    }
}
