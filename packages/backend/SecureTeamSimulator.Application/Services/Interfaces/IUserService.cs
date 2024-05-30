using SecureTeamSimulator.Core.Entities;


namespace SecureTeamSimulator.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task AddUserAsync(Guid id, string firstName, string lastName, string address, string birthdate, string authId, UserRole role);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);

    }
}