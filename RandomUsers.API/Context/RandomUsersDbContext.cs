using Microsoft.EntityFrameworkCore;
using RandomUsers.API.Models;

namespace RandomUsers.API.Context
{
    public class RandomUsersDbContext : DbContext
    {
        public RandomUsersDbContext(DbContextOptions<RandomUsersDbContext> options)
        : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<LoginModel> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }

    }
}
