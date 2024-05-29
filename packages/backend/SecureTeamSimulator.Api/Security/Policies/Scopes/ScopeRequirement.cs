using Microsoft.AspNetCore.Authorization;

namespace SecureTamSimulator.Api.Security.Policies.Scopes;

public class ScopeRequirement(string scope) : IAuthorizationRequirement
{
    public string Scope { get; } = scope ?? throw new ArgumentNullException(nameof(scope));
}