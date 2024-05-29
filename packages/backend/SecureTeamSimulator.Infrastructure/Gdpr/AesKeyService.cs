using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using SecureTeamSimulator.Core.Gdpr;

namespace SecureTeamSimulator.Infrastructure.Gdpr;

public class AesKeyService(IConfiguration configuration) : IAesKeyService
{
    public async Task<byte[]?> GetAesKeyAsync(string identifier)
    {
        var secretName = GenerateSecretName(identifier);
        var base64Key = configuration[$"AesKeys:{secretName}"];
    
        if (string.IsNullOrEmpty(base64Key))
        {
            return null;
        }

        var aesKey = Convert.FromBase64String(base64Key);

        return aesKey;
    }

    public Task<byte[]> GenerateAndStoreAesKeyAsync(string identifier)
    {
        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.GenerateKey();
        var base64Key = Convert.ToBase64String(aes.Key);

        var secretName = GenerateSecretName(identifier);
        // For demo purposes, store key in appsettings (not secure for production)
        // In production, use a secure vault like Azure Key Vault
        var section = configuration.GetSection("AesKeys");
        section[secretName] = base64Key;

        return Task.FromResult(aes.Key);
    }

    public Task DeleteAesKeyAsync(string identifier)
    {
        var secretName = GenerateSecretName(identifier);
        var section = configuration.GetSection("AesKeys");
        section[secretName] = null;
        return Task.CompletedTask;
    }

    private string GenerateSecretName(string identifier)
    {
        return $"aesKey-{identifier}";
    }
}