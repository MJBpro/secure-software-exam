using Microsoft.AspNetCore.Authorization;

namespace SecureTamSimulator.Api.Security.Policies.Scopes;

public class ScopeHandler : AuthorizationHandler<ScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        ScopeRequirement requirement)
    {
        // Extract the scopes from the token
        var scopesClaim = context.User.FindFirst(c => c.Type == "scope");
        if (scopesClaim == null) return Task.CompletedTask;
        
        var scopes = scopesClaim.Value.Split(' ');

        // Succeed if the scope array contains the required scope
        if (scopes.Any(s => s == requirement.Scope))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}