using System;

namespace SecureTeamSimulator.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string AuthId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Birthdate { get; set; }
        public UserRole Role { get; set; }
        public string EncryptionKey { get; set; } 
        public string EncryptionIV { get; set; }  
        public DateTime CreatedAt { get; set; }
    }
}