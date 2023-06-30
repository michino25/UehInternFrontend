using System;
using System.Security.Cryptography;
using System.Text;

namespace UehInternFrontend
{
    public class AES
    {

        public static string EncryptData(string data)
        {
            try
            {
                string key = "0DiKx5N5PU60jOozndHWHISm/MzAgRjDcfkLXxqDELQ=";
                string iv = "Wo/6qIrrDMAYPlE2aZrmdQ==";
                string encryptedJson = Encrypt(data, key, iv);

                return encryptedJson;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string DecryptData(string data)
        {
            try
            {
                string key = "0DiKx5N5PU60jOozndHWHISm/MzAgRjDcfkLXxqDELQ=";
                string iv = "Wo/6qIrrDMAYPlE2aZrmdQ==";
                string decryptedJson = Decrypt(data, key, iv);

                return decryptedJson;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static string Decrypt(string encryptedJson, string key, string iv)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedJson);
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(encryptedBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static string Encrypt(string json, string key, string iv)
        {
            byte[] encryptedBytes;
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                        cs.WriteAsync(jsonBytes, 0, jsonBytes.Length);
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }



    }
}