using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IRepo<int, Hotel> _hotelRepo;

        public HotelController(IRepo<int,Hotel> hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }
        [HttpPost]
        public async Task<ActionResult<Hotel>> AddHotel([FromForm] Hotel hotel, [FromForm] IFormFile imageFile)
        {
            var result = await _hotelRepo.Add(hotel, imageFile);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add hotel.");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Hotel>> UpdateHotel(int id, [FromForm] Hotel hotel, [FromForm] IFormFile imageFile)
        {
            if (id != hotel.HotelId)
            {
                return BadRequest("Hotel ID mismatch.");
            }

            var result = await _hotelRepo.Update(hotel, imageFile);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Hotel not found.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            var result = await _hotelRepo.Delete(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Hotel not found.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var result = await _hotelRepo.Get(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Hotel not found.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetAllHotels()
        {
            var result = await _hotelRepo.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No hotels found.");
        } 
    }
}
