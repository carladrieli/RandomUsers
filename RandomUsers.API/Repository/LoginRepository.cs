using Microsoft.EntityFrameworkCore;
using RandomUsers.API.Context;
using RandomUsers.API.Models;

namespace RandomUsers.API.Repository
{
    public class LoginRepository
    {
        private readonly RandomUsersDbContext _context;

        public LoginRepository(RandomUsersDbContext context) 
        {
            _context = context;
        }

        public async Task<LoginModel> GetLoginByUsernameAsync(string username)
        {
            return await _context.Login.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
