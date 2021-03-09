using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AesWpf
{
    public class AES
    {
        private readonly AesCryptoServiceProvider aesCryptoServiceProvider;

        public AES()
        {
            aesCryptoServiceProvider = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
            };

            // generate random key and IV for testing purposes
            aesCryptoServiceProvider.GenerateIV();
            aesCryptoServiceProvider.GenerateKey();
        }

        public string Encrypt(string plainText)
        {
            var transform = aesCryptoServiceProvider.CreateEncryptor();
            var encryptedBytes = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(plainText), 0, plainText.Length);

            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string encodedText)
        {
            var transform = aesCryptoServiceProvider.CreateDecryptor();

            var encryptedBytes = Convert.FromBase64String(encodedText);
            var decryptedBytes = transform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return ASCIIEncoding.ASCII.GetString(decryptedBytes);
        }
    }
}
