namespace SecureTeamSimulator.Core.Security.Incoming;

public interface IAuthManagementRepository
{
    Task SetUserMetadataAsync(string auth0Id, Dictionary<string, object> metadata);
}