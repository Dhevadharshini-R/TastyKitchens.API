namespace TastyKitchens.API.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // User, Admin, SuperAdmin
    public string PhoneNumber { get; set; } = "";
public string Address { get; set; } = "";
}
