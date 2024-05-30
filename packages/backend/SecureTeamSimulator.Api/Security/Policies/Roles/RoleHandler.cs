using Microsoft.AspNetCore.Authorization;

namespace SecureTamSimulator.Api.Security.Policies.Roles;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly IDictionary<string, IList<string>> _roleHierarchy = new Dictionary<string, IList<string>>
    {
        { "Admin", new List<string> { "Admin", "Member" } },
        { "Member", new List<string> { "Member" } }
    };

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var userRoles = context.User.Claims
            .Where(c => c.Type == "http://localhost:8082/roles")
            .Select(c => c.Value)
            .ToList();

        foreach (var role in userRoles)
        {
            if (_roleHierarchy.TryGetValue(role, out var roles) && roles.Any(r => requirement.AllowedRoles.Contains(r)))
            {
                context.Succeed(requirement);
                break;
            }
        }

        return Task.CompletedTask;
    }
}