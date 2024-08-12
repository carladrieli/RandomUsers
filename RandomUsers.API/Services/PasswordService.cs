using Microsoft.AspNetCore.Identity;
using RandomUsers.API.Models;

namespace RandomUsers.API.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<LoginModel> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<LoginModel>();
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var providedPasswordHashed = _passwordHasher.HashPassword(null, providedPassword);
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;

        }
    }
}
