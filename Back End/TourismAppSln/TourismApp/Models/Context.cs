using Microsoft.EntityFrameworkCore;

namespace TourismApp.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions Options):base(Options) 
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TravelAgent> TravelAgents { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
    } 
}
