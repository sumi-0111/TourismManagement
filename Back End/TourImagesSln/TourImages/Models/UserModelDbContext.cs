using Microsoft.EntityFrameworkCore;

namespace TourImages.Models
{
    public class UserModelDbContext : DbContext
    {
        public UserModelDbContext(DbContextOptions<UserModelDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserModel> Users { get; set; }

    }
}