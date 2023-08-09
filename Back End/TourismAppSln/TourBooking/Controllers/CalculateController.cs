using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Interfaces;
using TourBooking.Models;

namespace TourBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        private readonly ICalculateService _bookingService;

        public CalculateController(ICalculateService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("calculate-total")]
        public IActionResult CalculateTotal([FromBody] Booking booking)
        {
            double totalAmount = _bookingService.CalculateTotalAmount(booking.Amount, booking.AddTravelerCount);

            return Ok(new { TotalAmount = totalAmount });
        }
    }
}
