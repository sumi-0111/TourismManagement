//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using TourPackage.Interfaces;
//using TourPackage.Models;
//using TourPackage.Services;

//namespace TourPackage.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class HotelController : ControllerBase
//    {
//        private readonly IRepo<int, Hotel> _hotelRepo;

//        public HotelController(IRepo<int,Hotel> hotelRepo)
//        {
//            _hotelRepo = hotelRepo;
//        }
//        [HttpPost]
//        public async Task<ActionResult<Hotel>> AddContactDetails(Hotel contactDetails)
//        {
//            var result = await _hotelRepo.Add(contactDetails);
//            if (result != null)
//            {
//                return Ok(result);
//            }
//            return BadRequest("Failed to add contact details.");
//        }

//        [HttpPut("{id}")]
//        public async Task<ActionResult<Hotel>> UpdateContactDetails(int id, Hotel contactDetails)
//        {
//            if (id != contactDetails.HotelId)
//            {
//                return BadRequest("ContactDetails ID mismatch.");
//            }

//            var result = await _hotelRepo.Update(contactDetails);
//            if (result != null)
//            {
//                return Ok(result);
//            }
//            return NotFound("ContactDetails not found.");
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
//        {
//            var result = await _hotelRepo.Delete(id);
//            if (result != null)
//            {
//                return Ok(result);
//            }
//            return NotFound("Hotel not found.");
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Hotel>> GetHotel(int id)
//        {
//            var result = await _hotelRepo.Get(id);
//            if (result != null)
//            {
//                return Ok(result);
//            }
//            return NotFound("Hotel not found.");
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Hotel>>> GetAllHotels()
//        {
//            var result = await _hotelRepo.GetAll();
//            if (result != null)
//            {
//                return Ok(result);
//            }
//            return NotFound("No hotels found.");
//        } 
//    }
//}
