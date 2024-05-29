using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Gdpr;

namespace SecureTeamSimulator.Application.Services
{
    public class EncryptionService(IOptions<EncryptionSettings> encryptionSettings) : IEncryptionService
    {
        private readonly EncryptionSettings _encryptionSettings = encryptionSettings.Value;

        public string Encrypt(string plainText)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = Convert.FromBase64String(_encryptionSettings.Key);
            aesAlg.IV = Convert.FromBase64String(_encryptionSettings.IV);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = Convert.FromBase64String(_encryptionSettings.Key);
            aesAlg.IV = Convert.FromBase64String(_encryptionSettings.IV);

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}
