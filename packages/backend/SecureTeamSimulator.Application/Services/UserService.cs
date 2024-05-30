using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
using SecureTeamSimulator.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SecureTeamSimulator.Application.DTOs;

namespace SecureTeamSimulator.Application.Services
{
    public class UserService(AppDbContext appContext, IEncryptionService encryptionService) : IUserService
    {
        public async Task AddUserAsync(Guid id, string firstName, string lastName, string address, string birthdate, string authId, UserRole role, string key, string iv, string email)
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

        public async Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
        {
            var users = await appContext.Users.ToListAsync();

            var userDtos = users.Select(user =>
            {
                // Decrypt sensitive user data
                var encryptionKey = user.EncryptionKey;
                var encryptionIV = user.EncryptionIV;

                var decryptedAddress = encryptionService.Decrypt(user.Address, encryptionKey, encryptionIV);
                var decryptedBirthdate = encryptionService.Decrypt(user.Birthdate, encryptionKey, encryptionIV);

                // Map to GetUserDto
                return new GetUserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = decryptedAddress,
                    BirthDate = decryptedBirthdate
                };
            });

            return userDtos;
        }


        public async Task<GetUserDto?> GetUserByIdAsync(string authId)
        {
            var user = await appContext.Users.FirstOrDefaultAsync(x => x != null && x.AuthId == authId);
            if (user == null)
            {
                return null;
            }

            // Decrypt sensitive user data
            var encryptionKey = user.EncryptionKey;
            var encryptionIV = user.EncryptionIV;
            user.Address = encryptionService.Decrypt(user.Address, encryptionKey, encryptionIV);
            user.Birthdate = encryptionService.Decrypt(user.Birthdate, encryptionKey, encryptionIV);

            // Map to GetUserDto
            return new GetUserDto
            {
                Address = user.Address,
                BirthDate = user.Birthdate,
                LastName = user.LastName,
                FirstName = user.FirstName
            };
        }
        public async Task DeleteUserAsync(string authId)
        {
            var user = await appContext.Users.FirstOrDefaultAsync(x => x != null && x.AuthId == authId);
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

        public async Task<IEnumerable<GetUserDto?>> SearchUsersAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<GetUserDto>();
            }

            searchTerm = searchTerm.ToLower();

            var users = await appContext.Users
                .Where(user => user.FirstName.ToLower().Contains(searchTerm) ||
                               user.LastName.ToLower().Contains(searchTerm) ||
                               user.Address.ToLower().Contains(searchTerm))
                .ToListAsync();

            var userDtos = users.Select(user =>
            {
                // Decrypt sensitive user data
                var encryptionKey = user.EncryptionKey;
                var encryptionIV = user.EncryptionIV;

                var decryptedAddress = encryptionService.Decrypt(user.Address, encryptionKey, encryptionIV);
                var decryptedBirthdate = encryptionService.Decrypt(user.Birthdate, encryptionKey, encryptionIV);

                // Map to GetUserDto
                return new GetUserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = decryptedAddress,
                    BirthDate = decryptedBirthdate
                };
            });

            return userDtos;
        }

    }
}
