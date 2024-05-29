using Microsoft.AspNetCore.Mvc;
using SecureTeamSimulator.Application.Services;
using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;

namespace SecureTamSimulator.Api.Controllers
{
    [Route("user")]
    public class UserController(IUserService userService, IEncryptionService encryptionService)
        : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            // Encrypt sensitive user data
            user.FirstName = encryptionService.Encrypt(user.FirstName);
            user.LastName = encryptionService.Encrypt(user.LastName);
            user.Address = encryptionService.Encrypt(user.Address);
            string encryptedBirthdate = encryptionService.Encrypt(user.Birthdate.ToString()); // Using "o" for round-trip format

            await userService.AddUser(Guid.NewGuid(), user.FirstName, user.LastName, user.Address, encryptedBirthdate);

            return Ok(new
            {
                Message = "User was added!"
            });
        }


        [HttpGet("all")]
        public List<User> GetUsers()
        {
            var users = userService.GetUsers();
            // Decrypt sensitive user data
            foreach (var user in users)
            {
                user.FirstName = encryptionService.Decrypt(user.FirstName);
                user.LastName = encryptionService.Decrypt(user.LastName);
                user.Address = encryptionService.Decrypt(user.Address);
                user.Birthdate = encryptionService.Decrypt(user.Birthdate);
            }
            return users;
        }

        [HttpGet("{id}")]
        public User GetUserById(string id)
        {
            var user = userService.GetUserById(Guid.Parse(id));
            // Decrypt sensitive user data
            user.FirstName = encryptionService.Decrypt(user.FirstName);
            user.LastName = encryptionService.Decrypt(user.LastName);
            user.Address = encryptionService.Decrypt(user.Address);
            user.Birthdate = encryptionService.Decrypt(user.Birthdate);

            return user;
        }
    }
}
