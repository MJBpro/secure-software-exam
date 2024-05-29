namespace SecureTeamSimulator.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; } 
    public string Birthdate { get; set; }  // "RedTeam" or "BlueTeam
    public DateTime CreatedAt { get; set; } 
    
    public string AuthId { get; set; }

}