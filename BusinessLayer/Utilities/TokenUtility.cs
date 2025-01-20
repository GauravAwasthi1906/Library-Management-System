using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Utilities
{
    public static class TokenUtility
    {
        public static string EncryptToken(string plainTextToken)
        {
            // Generate the key from a hash (SHA256) to ensure the key is 256 bits (32 bytes)
            var key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("YourSecretKey12345"));

            using (var aes = Aes.Create())
            {
                aes.Key = key; // Now the key is 32 bytes (256 bits)
                aes.GenerateIV();   
                var iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainTextToken);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptToken(string encryptedToken)
        {
            var fullCipher = Convert.FromBase64String(encryptedToken);
            var iv = new byte[16]; // AES block size (16 bytes)
            var cipher = new byte[fullCipher.Length - iv.Length];

            Array.Copy(fullCipher, iv, iv.Length);
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            // Generate the key from a hash (SHA256) to ensure the key is 256 bits (32 bytes)
            var key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("YourSecretKey12345"));

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd(); // Returns the decrypted JWT token
                }
            }
        }


    }
}
