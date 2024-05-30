using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecureTamSimulator.Api.Security.Policies.Roles;
using SecureTeamSimulator.Application.Helpers;
using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
namespace SecureTamSimulator.Api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController(
        IUserService userService,
        IEncryptionService encryptionService,
        IAuth0ManagementService auth0ManagementService)
        : Controller
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>A confirmation message.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            // Get Auth0 ID and email from claims
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var authId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (authId == null)
                return BadRequest("Auth ID not found in claims");

            if (email == null)
                return BadRequest("Email not found in claims");

            // Generate encryption key and IV
            var encryptionKey = HashHelper.GenerateKey(authId, email);
            var encryptionIV = HashHelper.GenerateIV(authId, email);

            // Encrypt sensitive user data
            user.Address = encryptionService.Encrypt(user.Address, encryptionKey, encryptionIV);
            var encryptedBirthdate = encryptionService.Encrypt(user.Birthdate, encryptionKey, encryptionIV);

            // Add user
            await userService.AddUserAsync(Guid.NewGuid(), user.FirstName, user.LastName, user.Address, encryptedBirthdate, authId, user.Role, encryptionKey, encryptionIV);
            var roleId = GetRoleIdBasedOnEnum(user.Role); 
            await auth0ManagementService.AssignRoleToUserAsync(authId, roleId);
            return Ok(new
            {
                Message = "User was added!"
            });
        }
        private string GetRoleIdBasedOnEnum(UserRole role)
        {
            return role switch
            {
                UserRole.Admin => "rol_wWuM2wqzpt3m2bw7",
                UserRole.Member => "rol_wUY0TxFsacB4iyex",
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };
        }
        /// <summary>
        /// Gets all users. Only accessible to admins.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet("all")]
        [Authorize(Policy = PolicyRoles.Admin)]
        public async Task<IEnumerable<User?>> GetUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return users;
        }

        /// <summary>
        /// Gets a user by ID. Accessible to members.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("{id:guid}")]
        [Authorize(Policy = PolicyRoles.Member)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // Return 404 if the user is not found
            }

            // Check if EncryptionKey and EncryptionIV are not null
            if (string.IsNullOrEmpty(user.EncryptionKey) || string.IsNullOrEmpty(user.EncryptionIV))
            {
                return StatusCode(500, "User encryption data is missing."); // Return 500 if encryption data is missing
            }

            // Decrypt sensitive user data
            try
            {
                var encryptionKey = user.EncryptionKey;
                var encryptionIV = user.EncryptionIV;
                user.Address = encryptionService.Decrypt(user.Address, encryptionKey, encryptionIV);
                user.Birthdate = encryptionService.Decrypt(user.Birthdate, encryptionKey, encryptionIV);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error decrypting user data: {ex.Message}"); // Return 500 if decryption fails
            }

            return Ok(user); // Return 200 with the user data
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
            if (users.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}
