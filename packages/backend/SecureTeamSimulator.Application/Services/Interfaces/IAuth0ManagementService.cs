namespace SecureTeamSimulator.Application.Services.Interfaces;

public interface IAuth0ManagementService
{
    Task AssignRoleToUserAsync(string userId, string roleId);
}