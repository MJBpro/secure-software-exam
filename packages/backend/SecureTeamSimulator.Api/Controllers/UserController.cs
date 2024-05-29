using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureTamSimulator.Api.Security.Policies.Roles;
using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
using SecureTeamSimulator.Core.Security.Outgoing;

namespace SecureTamSimulator.Api.Controllers
{
    [Route("user")]
    public class UserController(
        IUserService userService,
        IEncryptionService encryptionService,
        IUserContextService userContextService)
        : Controller
    {

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            // Encrypt sensitive user data
            user.FirstName = encryptionService.Encrypt(user.FirstName);
            user.LastName = encryptionService.Encrypt(user.LastName);
            user.Address = encryptionService.Encrypt(user.Address);
            string encryptedBirthdate = encryptionService.Encrypt(user.Birthdate); // Using "o" for round-trip format

            // Get Auth0 ID from claims

            await userService.AddUser(Guid.NewGuid(), user.FirstName, user.LastName, user.Address, encryptedBirthdate, user.AuthId, user.Role);

            return Ok(new
            {
                Message = "User was added!"
            });
        }

        [HttpGet("all")]
        [Authorize(Policy = PolicyRoles.Admin)]
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
        [Authorize(Policy = PolicyRoles.Member)]
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

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMyClaims()
        {
            var claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }).ToList();

            return Ok(claims);
        }
    }
}
