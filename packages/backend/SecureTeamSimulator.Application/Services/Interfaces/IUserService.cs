using SecureTeamSimulator.Core.Entities;


namespace SecureTeamSimulator.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User?>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string authId);
        Task AddUserAsync(Guid id, string firstName, string lastName, string address, string birthdate, string authId, UserRole role, string key, string iv);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string authId);
        
        Task<IEnumerable<User?>> SearchUsersAsync(string searchTerm);

    }
}