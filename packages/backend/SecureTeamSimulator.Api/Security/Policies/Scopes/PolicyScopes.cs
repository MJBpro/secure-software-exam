namespace SecureTamSimulator.Api.Security.Policies.Scopes;

public static class PolicyScopes
{
    public const string ReadClaims = "read:claims";
    public const string WriteSignup = "write:user-signup";
    public const string ReadsUserTermsContext = "read:user-terms-context";
}