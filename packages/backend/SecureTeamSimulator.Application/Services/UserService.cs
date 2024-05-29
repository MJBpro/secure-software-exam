using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
using SecureTeamSimulator.Core.Gdpr;
using SecureTeamSimulator.Infrastructure.Database;

namespace SecureTeamSimulator.Application.Services;

public class UserService : IUserService
{

    private readonly AppDbContext _appContext;
    private readonly IAesKeyService _aesKeyService;

    public  UserService(AppDbContext appContext, IAesKeyService aesKeyService)
    {
        _appContext = appContext;
        _aesKeyService = aesKeyService;
    }

    public async Task AddUser(Guid id, string firstName, string lastName, string address, DateTime birthdate)
    {

        await _appContext.TodoItems.AddAsync(new User()
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            Birthdate = birthdate,
            CreatedAt = DateTime.UtcNow,
        });

        await _appContext.SaveChangesAsync();
    }

    public List<User> GetUsers()
    {
        return _appContext.TodoItems.ToList();
    }

    public User GetUserById(Guid id)
    {
        return _appContext.TodoItems.FirstOrDefault(x => x.Id == id);
    }
}