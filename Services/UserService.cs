using TastyKitchens.API.Data;
using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;

namespace TastyKitchens.API.Services;

public class UserService
{
    public User GetUserByEmail(string email)
    {
        return FakeDb.Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public User UpdateProfile(string email, string phoneNumber, string address)
    {
        var user = GetUserByEmail(email);
        if (user == null) return null;

        user.PhoneNumber = phoneNumber;
        user.Address = address;

        FakeDb.SaveUsersToFile();
        return user;
    }
}
