using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace AesWpf
{
    // Useful resources:
    // https://www.allkeysgenerator.com/Random/Security-Encryption-Key-Generator.aspx - generate byte specific strings
    // https://cryptii.com/pipes/base64-to-hex - base64 to HEX
    // https://www.base64encode.org/ - base64 decoder

    public class AES
    {
        private byte[] IV = Encoding.ASCII.GetBytes("TjWnZq4t7w!z%C*F");
        private readonly AesCryptoServiceProvider aesCryptoServiceProvider;

        public AES(string key, string mode = "CBC")
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);

            if (keyBytes.Length != 32)
            {
                throw new Exception("Key is not 32 bytes long (256 bits for AES256 required)");
            }

            aesCryptoServiceProvider = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                IV = IV,
                Key = keyBytes,
            };
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
