using Microsoft.EntityFrameworkCore;

namespace TourPackage.Models
{
    public class TourPackageContext:DbContext
    {
        public TourPackageContext(DbContextOptions option):base(option)
        {
            
        }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<ViaRoute> VViaRoutes { get; set; } 
        public DbSet<Image>  Images { get; set; }
        public DbSet<ContactDetails>  Contacts { get; set; }
    }
}
