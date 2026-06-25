using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace _Project.Scripts.Systems.Save {
    public class JsonSerializer : ISerializer {

        private readonly string encryptionKey = "74AF6D58A2DB8C97E3F42BCB619B3FE47F1DC5B98A62C3E4D5A1927F30E8B6F2";
        
        public string Serialize<T>(T obj) {
            var json = JsonUtility.ToJson(obj, false);
            return EncryptString(json);
        }

        public T Deserialize<T>(string json) {
            var decryptedContent = DecryptString(json);
            return JsonUtility.FromJson<T>(decryptedContent);
        }
        
        private string EncryptString(string plainText) {
            byte[] key = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 32));
            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = key;
                aesAlg.GenerateIV();
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream()) {
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                        using (var swEncrypt = new StreamWriter(csEncrypt)) {
                            swEncrypt.Write(plainText);
                        }
                    }
                    
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        
        private string DecryptString(string cipherText) {
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - 16];
            
            Array.Copy(fullCipher, iv, iv.Length);
            Array.Copy(fullCipher, 16, cipher, 0, cipher.Length);
            
            byte[] key = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 32));
            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipher)) {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                        using (var srDecrypt = new StreamReader(csDecrypt)) {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        
    }
}