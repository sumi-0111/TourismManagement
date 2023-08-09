using System.ComponentModel.DataAnnotations;

namespace TourBooking.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int packageId { get; set; }
        public int? AddTravelerCount { get; set; }
        public string? BookingName { get; set; }
        public string? BookingMail { get; set; }
        public double Amount { get; set; }
        public double TotalAmount { get; set; }
    }
}
