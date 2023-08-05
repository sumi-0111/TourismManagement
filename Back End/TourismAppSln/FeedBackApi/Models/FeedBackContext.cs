using Microsoft.EntityFrameworkCore;

namespace FeedBackApi.Models
{
    public class FeedBackContext:DbContext
    {
        public FeedBackContext(DbContextOptions Options) : base(Options)
        {

        }
        public DbSet<FeedBack> FeedBacks { get; set; }
    }
}
