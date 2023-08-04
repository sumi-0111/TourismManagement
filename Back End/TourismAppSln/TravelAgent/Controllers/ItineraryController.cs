using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItineraryController : ControllerBase
    {
        private readonly IRepo<int, Itinerary> _itineraryRepo;

        public ItineraryController(IRepo<int,Itinerary> itineraryRepo)
        {
            _itineraryRepo = itineraryRepo;
        }
        [HttpPost]
        public async Task<ActionResult<Itinerary>> AddItinerary([FromForm] Itinerary itinerary, [FromForm] IFormFile imageFile)
        {
            var result = await _itineraryRepo.Add(itinerary, imageFile);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add itinerary.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Itinerary>> UpdateItinerary(int id, [FromForm] Itinerary itinerary, [FromForm] IFormFile imageFile)
        {
            if (id != itinerary.ItineraryId)
            {
                return BadRequest("Itinerary ID mismatch.");
            }

            var result = await _itineraryRepo.Update(itinerary, imageFile);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Itinerary not found.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Itinerary>> DeleteItinerary(int id)
        {
            var deletedItinerary = await _itineraryRepo.Delete(id);
            if (deletedItinerary != null)
            {
                return Ok(deletedItinerary);
            }
            return NotFound("Itinerary not found.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Itinerary>> GetItinerary(int id)
        {
            var itinerary = await _itineraryRepo.Get(id);
            if (itinerary != null)
            {
                return Ok(itinerary);
            }
            return NotFound("Itinerary not found.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Itinerary>>> GetAllItineraries()
        {
            var itineraries = await _itineraryRepo.GetAll();
            if (itineraries != null && itineraries.Count > 0)
            {
                return Ok(itineraries);
            }
            return NotFound("No itineraries found.");
        }
    }
}
