using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Interfaces;
using TourBooking.Models;
using TourBooking.Services;

namespace TourBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IManageBooking _manageBookingService;

        public BookingController(IManageBooking manageBookingService)
        {
            _manageBookingService = manageBookingService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Booking>?> AddBooking(Booking booking)
        {
            try
            {
                var result = await _manageBookingService.AddBooking(booking);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to add booking.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred during doctor registration: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Booking>?> UpdateBooking(int id, Booking booking)
        {
            try
            {
                if (id != booking.BookingId)
                {
                    return BadRequest("Invalid booking ID.");
                }

                var existingBooking = await _manageBookingService.GetById(id);
                if (existingBooking == null)
                {
                    return NotFound();
                }

                var result = await _manageBookingService.UpdateBooking(booking);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to update booking.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred during doctor registration: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Booking>?> DeleteBooking(int id)
        {
            try
            {
                var existingBooking = await _manageBookingService.GetById(id);
                if (existingBooking == null)
                {
                    return NotFound();
                }

                var result = await _manageBookingService.DeleteBooking(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to delete booking.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred during doctor registration: {ex.Message}");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<Booking>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<Booking>?>?> GetAllBookings()
        {
            try
            {
                var bookings = await _manageBookingService.GetAll();
                if (bookings != null)
                {
                    return Ok(bookings);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred during doctor registration: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Booking>?> GetBooking(int id)
        {
            try
            {
                var booking = await _manageBookingService.GetById(id);
                if (booking != null)
                {
                    return Ok(booking);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred during doctor registration: {ex.Message}");
            }
        }

    }
}
