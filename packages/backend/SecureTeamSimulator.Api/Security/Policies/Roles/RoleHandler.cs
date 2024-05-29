using Microsoft.AspNetCore.Authorization;

namespace SecureTamSimulator.Api.Security.Policies.Roles;

public class RoleHandler(IConfiguration configuration) : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement roleRequirement)
    {
        
        var organizationRole = context.User.Claims
            .FirstOrDefault(c => c.Type.EndsWith("/organization_role"))?.Value;
        
        if (organizationRole != null && roleRequirement.Roles.Contains(organizationRole.ToLower()))
        {
            context.Succeed(roleRequirement);
            return Task.CompletedTask;
        }

        context.Fail(new AuthorizationFailureReason(this, "User is not Authorized for this action"));
        return Task.CompletedTask;
    }
}