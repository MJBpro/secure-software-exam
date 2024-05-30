namespace SecureTeamSimulator.Application.Services.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText, string key, string iv);
        string Decrypt(string plainText, string key, string iv);
    }
}