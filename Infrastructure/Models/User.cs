using Infrastructure.Models;

namespace Infrastructure.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiresAt { get; set; }
}