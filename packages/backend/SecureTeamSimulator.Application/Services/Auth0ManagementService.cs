using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using RestSharp;
using SecureTeamSimulator.Application.Services.Interfaces;

namespace SecureTeamSimulator.Application.Services;

public class Auth0ManagementService
    : IAuth0ManagementService
{
    private readonly RestClient client;

    public Auth0ManagementService()
    {
       
        client = new RestClient();
    }
    private async Task<string?> GetAccessTokenAsync()
    {

        var tokenRequest = new RestRequest("https://pbsw.eu.auth0.com/oauth/token", Method.Post);
        tokenRequest.AddHeader("content-type", "application/json");

        var tokenRequestBody = new
        {
            // For Demo purposes, should be in KeyVault.
            client_id = "KP7pesJ4boA8GLaLwIoYYHWovoNEXhxl",
            client_secret = "6pRGAaGX6nRvs2h8a6qqc45cF4fLZFV6-S26cXNtX7d4oWx68PxxtxuyNRntyiT0",
            audience = "https://pbsw.eu.auth0.com/api/v2/",
            grant_type = "client_credentials"
        };

        tokenRequest.AddJsonBody(tokenRequestBody);

        var tokenResponse = await client.ExecuteAsync(tokenRequest);

        if (!tokenResponse.IsSuccessful)
        {
            throw new HttpRequestException($"Error fetching token: {tokenResponse.StatusCode} - {tokenResponse.Content}");
        }

        var tokenContent = JsonSerializer.Deserialize<JsonElement>(tokenResponse.Content);
        return tokenContent.GetProperty("access_token").GetString();
    }
    public async Task AssignRoleToUserAsync(string userId, string roleId)
    {
        var accessToken = await GetAccessTokenAsync();
        
       var request = new RestRequest($"https://pbsw.eu.auth0.com/api/v2/users/{userId}/roles", Method.Post);
       request.AddHeader("content-type", "application/json");
       request.AddHeader("authorization", $"Bearer {accessToken}");
       request.AddHeader("cache-control", "no-cache");

       var payload = new { roles = new[] { roleId } };
       request.AddJsonBody(payload);

       var response = await client.ExecuteAsync(request);

       if (!response.IsSuccessful)
       {
           throw new HttpRequestException($"Error assigning role: {response.StatusCode} - {response.Content}");
       }
    }
}