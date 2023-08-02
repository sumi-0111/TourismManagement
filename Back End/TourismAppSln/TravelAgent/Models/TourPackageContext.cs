using Microsoft.EntityFrameworkCore;

namespace TourPackage.Models
{
    public class TourPackageContext:DbContext
    {
        public TourPackageContext(DbContextOptions option):base(option)
        {
            
        }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; } 
        public DbSet<Image>  Images { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<ContactDetails>  Contacts { get; set; }
    }
}
