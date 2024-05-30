using System.Security.Cryptography;
using System.Text;

namespace SecureTeamSimulator.Application.Helpers;

public static class HashHelper
{
    public static string GenerateKey(string authId, string email)
    {
        using var sha256Hash = SHA256.Create();
        // Combine authId and email
        var combined = authId + email;

        // Compute hash
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(combined));

        // Convert to Base64 string
        return Convert.ToBase64String(bytes);
    }

    public static string GenerateIV(string authId, string email)
    {
        using var sha256Hash = SHA256.Create();
        // Combine email and authId in reverse order for IV
        var combined = email + authId;

        // Compute hash
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(combined));

        // Convert to Base64 string
        // Take only first 16 bytes for IV (128 bits)
        return Convert.ToBase64String(bytes).Substring(0, 16);
    }
}