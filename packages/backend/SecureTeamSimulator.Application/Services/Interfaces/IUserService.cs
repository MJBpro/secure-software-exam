using SecureTeamSimulator.Application.DTOs;
using SecureTeamSimulator.Core.Entities;


namespace SecureTeamSimulator.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDto?>> GetAllUsersAsync();
        Task<GetUserDto?> GetUserByIdAsync(string authId);
        Task AddUserAsync(Guid id, string firstName, string lastName, string address, string birthdate, string authId, UserRole role, string key, string iv, string email);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string authId);
        
        Task<IEnumerable<GetUserDto?>> SearchUsersAsync(string searchTerm);

    }
}