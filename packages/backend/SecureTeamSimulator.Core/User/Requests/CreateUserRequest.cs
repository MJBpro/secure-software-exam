using System.ComponentModel.DataAnnotations;

namespace SecureTeamSimulator.Core.User.Requests;

public class CreateUserRequest
{
    [Required] public string Name { get; set; } = null!;
    [Required] public string Email { get; set; } = null!;
}