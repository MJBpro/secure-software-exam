using System.Security.Cryptography;
using System.Text;
using SecureTeamSimulator.Application.Services.Interfaces;

namespace SecureTeamSimulator.Application.Services;

public class EncryptionService : IEncryptionService
{
    public string Encrypt(string plainText, string key, string iv)
    {
        var keyBytes = Convert.FromBase64String(key);
        var ivBytes = Encoding.UTF8.GetBytes(iv);

        using var aesAlg = Aes.Create();
        aesAlg.Key = keyBytes;
        aesAlg.IV = ivBytes;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }
        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public string Decrypt(string cipherText, string key, string iv)
    {
        var keyBytes = Convert.FromBase64String(key);
        var ivBytes = Encoding.UTF8.GetBytes(iv);

        using var aesAlg = Aes.Create();
        aesAlg.Key = keyBytes;
        aesAlg.IV = ivBytes;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}