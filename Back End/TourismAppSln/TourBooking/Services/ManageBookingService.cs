using TourBooking.Interfaces;
using TourBooking.Models;

namespace TourBooking.Services
{
    public class ManageBookingService : IManageBooking
    {
        private readonly IRepo<int, Booking> _bookingRepo;

           public ManageBookingService(IRepo<int, Booking> bookingRepo)
            {
                   _bookingRepo = bookingRepo;
            }

        public async Task<Booking?> AddBooking(Booking booking)
        {
            booking.TotalAmount = (double)(booking.Amount * booking.AddTravelerCount);
            booking = await _bookingRepo.Add(booking);
            return booking;
        }

            public async Task<Booking?> DeleteBooking(int bookingId)
            {

                        var booking = await _bookingRepo.Get(bookingId);
                        if (booking != null)
                        {
                            return await _bookingRepo.Delete(bookingId);
                        }
                        return null;      
            }

            public async Task<ICollection<Booking>?> GetAll()
            {
            return await _bookingRepo.GetAll();
            }

        public async Task<Booking?> GetById(int bookingId)
        {
            return await _bookingRepo.Get(bookingId);
        }

        public async Task<Booking?> UpdateBooking(Booking booking)
        {
            var existingBooking = await _bookingRepo.Get(booking.BookingId);
                       if (existingBooking != null)
                      {
                         if (existingBooking.Amount != booking.Amount || existingBooking.AddTravelerCount != booking.AddTravelerCount)
                          {
                             int totalTravelersCount = (int)(booking.AddTravelerCount + 1);
                             booking.TotalAmount = booking.Amount * totalTravelersCount;
                           }

                           existingBooking.Amount = booking.Amount;
                           existingBooking.AddTravelerCount = booking.AddTravelerCount;
                           existingBooking.TotalAmount = booking.TotalAmount;

                       await _bookingRepo.Update(existingBooking);
                        }
                        return existingBooking;        }
        }
    }
