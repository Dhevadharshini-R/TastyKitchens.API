using TastyKitchens.API.Models;
using TastyKitchens.API.Helpers;

namespace TastyKitchens.API.Services;

public class UserService
{
    private readonly string usersPath;

    public UserService()
    {
        usersPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "users.json");
    }

    public User GetUserByEmail(string email)
    {
        var users = FileHelper.ReadFromFile<User>(usersPath);
        return users.FirstOrDefault(u => u.Email == email);
    }

    public User UpdateProfile(string email, string phone, string address)
    {
        var users = FileHelper.ReadFromFile<User>(usersPath);

        var user = users.FirstOrDefault(u => u.Email == email);

        if (user == null)
            return null;

        user.PhoneNumber = phone;
        user.Address = address;

        FileHelper.WriteToFile(usersPath, users);

        return user;
    }
}