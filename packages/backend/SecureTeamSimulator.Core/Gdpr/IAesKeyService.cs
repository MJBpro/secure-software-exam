namespace SecureTeamSimulator.Core.Gdpr;

public interface IAesKeyService
{
    Task<byte[]?> GetAesKeyAsync(string identifier);
    Task<byte[]> GenerateAndStoreAesKeyAsync(string identifier);
    Task DeleteAesKeyAsync(string identifier);
}