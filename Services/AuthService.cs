using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Data;
using BCrypt.Net;

namespace TastyKitchens.API.Services
{
    public class AuthService
    {
        public User Register(RegisterDto dto)
        {
            var exists = FakeDb.Users.Any(u => u.Email == dto.Email);
            if (exists) return null;

            var newId = FakeDb.Users.Any() ? FakeDb.Users.Max(u => u.Id) + 1 : 1;

            var user = new User
            {
                Id = newId,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),

                // 🔥 ROLE SUPPORT (ADDED)
                Role = string.IsNullOrEmpty(dto.Role) ? "User" : dto.Role
            };

            FakeDb.Users.Add(user);

            // 🔥 SAVE USERS TO FILE
            FakeDb.SaveUsers();

            return user;
        }

        public User Login(LoginDto dto)
        {
            var user = FakeDb.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null) return null;

            var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!valid) return null;

            return user;
        }
    }
}