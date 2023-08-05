using Microsoft.EntityFrameworkCore;

namespace TravellerFeedBackAPI.Models
{
    public class FeedBackContext:DbContext
    {
        public FeedBackContext(DbContextOptions Options) : base(Options)
        {

        } 
        public DbSet<UserFeedBack> UserFeedBacks { get; set; }
    }
}