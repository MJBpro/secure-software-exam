using Microsoft.AspNetCore.Authorization;

namespace SecureTamSimulator.Api.Security.Policies.Roles;

public class RoleRequirement(IEnumerable<string> allowedRoles) : IAuthorizationRequirement
{
    public IEnumerable<string> AllowedRoles { get; } = allowedRoles;
}