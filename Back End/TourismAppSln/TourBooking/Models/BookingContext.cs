using Microsoft.EntityFrameworkCore;

namespace TourBooking.Models
{
    public class BookingContext:DbContext
    {
        public BookingContext(DbContextOptions option):base(option)
        {
            
        }
        public DbSet<Booking> Bookings { get; set; }
    }
}
