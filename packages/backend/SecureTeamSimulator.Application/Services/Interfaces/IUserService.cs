using SecureTeamSimulator.Core.Entities;

namespace SecureTeamSimulator.Application.Services.Interfaces;

public interface IUserService
{
    Task AddUser(Guid id, string firstName, string lastName, string address, string birthdate, string authId, UserRole role);
    List<User> GetUsers();
    
    User GetUserById(Guid id);

}