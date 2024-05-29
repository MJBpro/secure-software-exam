using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
using SecureTeamSimulator.Core.Gdpr;
using SecureTeamSimulator.Core.Security.Outgoing;
using SecureTeamSimulator.Infrastructure.Database;

namespace SecureTeamSimulator.Application.Services;

public class UserService(AppDbContext appContext ,  IUserContextService userContextService
    ) : IUserService
{
    public async Task AddUser(Guid id, string firstName, string lastName, string address, string birthdate, string authId, UserRole role)
    {
        await appContext.Users.AddAsync(new User()
        {
            Id = id,
            AuthId = authId,
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            Birthdate = birthdate,
            CreatedAt = DateTime.UtcNow,
        });

        await appContext.SaveChangesAsync();
    }

    public List<User> GetUsers()
    {
        return appContext.Users.ToList();
    }

    public User GetUserById(Guid id)
    {
        return appContext.Users.FirstOrDefault(x => x.Id == id);
    }
}