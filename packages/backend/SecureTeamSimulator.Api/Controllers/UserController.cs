using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecureTamSimulator.Api.Security.Policies.Roles;
using SecureTeamSimulator.Application.DTOs;
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
        /// <param name="createUserDto"></param>
        /// <returns>A confirmation message.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
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
            createUserDto.Address = encryptionService.Encrypt(createUserDto.Address, encryptionKey, encryptionIV);
            var encryptedBirthdate = encryptionService.Encrypt(createUserDto.BirthDate, encryptionKey, encryptionIV);
            var encryptedEmail = encryptionService.Encrypt(email, encryptionKey, encryptionIV);
            // Add user
            await userService.AddUserAsync(Guid.NewGuid(), createUserDto.FirstName, createUserDto.LastName, createUserDto.Address, encryptedBirthdate, authId, UserRole.Member, encryptionKey, encryptionIV, encryptedEmail);
            var roleId = GetRoleIdBasedOnEnum(UserRole.Member); 
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
        public async Task<IEnumerable<GetUserDto?>> GetUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return users;
        }

        /// <summary>
        /// Gets a user by ID. Accessible to members.
        /// </summary>
        /// <param name="authId"></param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("{authId}")]
        [Authorize(Policy = PolicyRoles.Member)]
        public async Task<IActionResult> GetUserById(string authId)
        {
            var userDto = await userService.GetUserByIdAsync(authId);
            if (userDto == null)
            {
                return NotFound(); // Return 404 if the user is not found
            }
            

            return Ok(userDto); // Return 200 with the user data
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
        /// <param name="authId"></param>
        /// <returns>A confirmation message.</returns>
        [HttpDelete("{authId}")]
        [Authorize(Policy = PolicyRoles.Admin)]
        public async Task<IActionResult> DeleteUser(string authId)
        {
            var user = await userService.GetUserByIdAsync(authId);
            if (user == null)
            {
                return NotFound();
            }
            await userService.DeleteUserAsync(authId);
            return NoContent();
        }
        /// <summary>
        /// Searches users by a term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns>A list of users that match the search criteria.</returns>
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchUsers(string searchTerm)
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
