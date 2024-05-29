using SecureTeamSimulator.Core.User.Requests;

namespace SecureTeamSimulator.Core.User;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string CprNumber { get;  private set; }
    
    public User(CreateUserRequest createUserRequest) { }
    

    
}