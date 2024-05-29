using System.Security.Claims;
using SecureTamSimulator.Api.Security.Policies.Roles;
using SecureTeamSimulator.Core.Security.Outgoing;

namespace SecureTamSimulator.Api.Security;

public class UserContextService(
    IHttpContextAccessor httpContextAccessor) : IUserContextService
{
    public Guid GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type.EndsWith("/user_id"))?.Value;

        if (userId is not null) return new Guid(userId);

        if (IsClientCredentials())
        {
            return default;
        }

        return default;
    }

    public string GetAuthId()
    {
        var auth0Id = httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (auth0Id is not null) return auth0Id;

        throw new NullReferenceException("Auth0 id is null");
    }

    public string GetEmail()
    {
        var email = httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (email is not null) return email;

        throw new NullReferenceException("Email is null");
    }

    public bool IsClientCredentials()
    {
        var isClientCredentials = httpContextAccessor.HttpContext?.User.Claims
            .Any(c => c is { Type: "gty", Value: "client-credentials" });

        return isClientCredentials ?? false;
    }
    
    
}