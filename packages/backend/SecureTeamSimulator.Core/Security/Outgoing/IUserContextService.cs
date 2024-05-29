namespace SecureTeamSimulator.Core.Security.Outgoing;

public interface IUserContextService
{
    Guid GetUserId();
    string GetAuthId();
    string GetEmail();
  
}