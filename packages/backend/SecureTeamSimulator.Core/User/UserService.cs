using SecureTeamSimulator.Core.Gdpr;
using SecureTeamSimulator.Core.User.Requests;

namespace SecureTeamSimulator.Core.User;

public class UserService (IAesKeyService aesKeyService)
{
    public async Task<Guid> CreateUserAsync(CreateUserRequest request, CancellationToken ct)
    {
        var user = new User(request);

       // await baseRepository.StoreAsync(user, ct);

        return user.Id;
    }
    
    public async Task DeleteUserAsync(Guid id, CancellationToken ct)
    {
        var user = await GetUserAsync(id, ct);
        
        
       // await baseRepository.StoreAsync(user, ct);
    }
    
    public async Task<byte[]> GdprDeleteUserAsync(Guid id, CancellationToken ct)
    {
        var user = await GetUserAsync(id, ct);
        
       // user.GdprDelete();
        
        var aesKey = await aesKeyService.GetAesKeyAsync(id.ToString());
        if (aesKey is null)
            throw new ArgumentNullException(nameof(aesKey));
        
       // await baseRepository.StoreAsync(user, ct);

        return aesKey;
    }

    private async Task<User> GetUserAsync(Guid id, CancellationToken ct)
    {
       // var user = await baseRepository.GetAggregateAsync<User>(id, ct);
      ////     throw new ArgumentNullException(nameof(user));

       // return user;
       return null;
    }
}