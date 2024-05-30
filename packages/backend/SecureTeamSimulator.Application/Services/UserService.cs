using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
using SecureTeamSimulator.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SecureTeamSimulator.Application.Helpers;

namespace SecureTeamSimulator.Application.Services
{
    public class UserService(AppDbContext appContext) : IUserService
    {
        public async Task AddUserAsync(Guid id, string firstName, string lastName, string address, string birthdate, string authId, UserRole role, string key, string iv)
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
                Role = role,
                EncryptionKey = key,
                EncryptionIV = iv
            });

            await appContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User?>> GetAllUsersAsync()
        {
            return await appContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await appContext.Users.FirstOrDefaultAsync(x => x != null && x.Id == id);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await appContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                appContext.Users.Remove(user);
                await appContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await appContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Address = user.Address;
                existingUser.Birthdate = user.Birthdate;
                existingUser.Role = user.Role;

                appContext.Users.Update(existingUser);
                await appContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User?>> SearchUsersAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<User>();
            }

            searchTerm = searchTerm.ToLower();
            return await appContext.Users
                .Where(user => user.FirstName.ToLower().Contains(searchTerm) ||
                               user.LastName.ToLower().Contains(searchTerm) ||
                               user.Address.ToLower().Contains(searchTerm) ||
                               user.AuthId.ToLower().Contains(searchTerm))
                .ToListAsync();
        }
    }
}
