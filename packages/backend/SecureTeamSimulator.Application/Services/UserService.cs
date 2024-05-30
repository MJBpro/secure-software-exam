using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
using SecureTeamSimulator.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SecureTeamSimulator.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appContext;

        public UserService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task AddUserAsync(Guid id, string firstName, string lastName, string address, string birthdate, string authId, UserRole role)
        {
            await _appContext.Users.AddAsync(new User()
            {
                Id = id,
                AuthId = authId,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Birthdate = birthdate,
                CreatedAt = DateTime.UtcNow,
                Role = role
            });

            await _appContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _appContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _appContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                _appContext.Users.Remove(user);
                await _appContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _appContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Address = user.Address;
                existingUser.Birthdate = user.Birthdate;
                existingUser.Role = user.Role;

                _appContext.Users.Update(existingUser);
                await _appContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<User>();
            }

            searchTerm = searchTerm.ToLower();
            return await _appContext.Users
                .Where(user => user.FirstName.ToLower().Contains(searchTerm) ||
                               user.LastName.ToLower().Contains(searchTerm) ||
                               user.Address.ToLower().Contains(searchTerm) ||
                               user.AuthId.ToLower().Contains(searchTerm))
                .ToListAsync();
        }
    }
}
