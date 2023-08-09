using TourBooking.Models;

namespace TourBooking.Interfaces
{
    public interface IManageBooking
    {
        public Task<Booking?> AddBooking(Booking booking);
        public Task<Booking?> UpdateBooking(Booking booking);
        public Task<Booking?> DeleteBooking(int bookingId);
        public Task<Booking?> GetById(int bookingId);
        public Task<ICollection<Booking>?> GetAll();
    }
}
