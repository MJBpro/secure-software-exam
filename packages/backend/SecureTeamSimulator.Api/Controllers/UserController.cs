using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureTamSimulator.Api.Security.Policies.Roles;
using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
using SecureTeamSimulator.Core.Security.Outgoing;
namespace SecureTamSimulator.Api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController(
        IUserService userService,
        IEncryptionService encryptionService,
        IUserContextService userContextService)
        : Controller
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>A confirmation message.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
        
            user.Address = encryptionService.Encrypt(user.Address);
            string encryptedBirthdate = encryptionService.Encrypt(user.Birthdate);

            // Get Auth0 ID from claims
            string authId = userContextService.GetAuthId(); // Assuming this method is available

            await userService.AddUserAsync(Guid.NewGuid(), user.FirstName, user.LastName, user.Address, encryptedBirthdate, authId, user.Role);

            return Ok(new
            {
                Message = "User was added!"
            });
        }

        /// <summary>
        /// Gets all users. Only accessible to admins.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet("all")]
        [Authorize(Policy = PolicyRoles.Admin)]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return users;
        }

        /// <summary>
        /// Gets a user by ID. Accessible to members.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyRoles.Member)]
        public async Task<User> GetUserById(string id)
        {
            var user = await userService.GetUserByIdAsync(Guid.Parse(id));
            // Decrypt sensitive user data
           
            user.Address = encryptionService.Decrypt(user.Address);
            user.Birthdate = encryptionService.Decrypt(user.Birthdate);

            return user;
        }

        /// <summary>
        /// Gets the claims of the current user.
        /// </summary>
        /// <returns>The claims of the current user.</returns>
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
        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A confirmation message.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyRoles.Admin)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userService.GetUserByIdAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }
            await userService.DeleteUserAsync(Guid.Parse(id));
            return NoContent();
        }
        /// <summary>
        /// Searches users by a term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns>A list of users that match the search criteria.</returns>
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers(string searchTerm)
        {
            var users = await userService.SearchUsersAsync(searchTerm);
            // Decrypt sensitive user data
            foreach (var user in users)
            {
                user.FirstName = encryptionService.Decrypt(user.FirstName);
                user.LastName = encryptionService.Decrypt(user.LastName);
                user.Address = encryptionService.Decrypt(user.Address);
            }
            return Ok(users);
        }
    }
}
