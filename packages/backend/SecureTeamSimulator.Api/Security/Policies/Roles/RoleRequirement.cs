using Microsoft.AspNetCore.Authorization;

namespace SecureTamSimulator.Api.Security.Policies.Roles;

public abstract class RoleRequirement(IEnumerable<string> roles) : IAuthorizationRequirement
{
    public IEnumerable<string> Roles { get; } = roles;
}