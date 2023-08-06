using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Services;

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItineraryController : ControllerBase
    {
        private readonly IItineraryService _itineraryService;

        public ItineraryController(IItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
        }

        [HttpPost("createItinerary")]
        public async Task<ActionResult<Itinerary>> AddItinerary(Itinerary itinerary)
        {
            var addedItinerary = await _itineraryService.AddItinerary(itinerary);
            if (addedItinerary != null)
            {
                return Ok(addedItinerary);
            }
            return BadRequest("Failed to add itinerary.");
        }


        [HttpPut("updateItinerary")]
        public async Task<ActionResult<Itinerary>> UpdateItinerary(int id, Itinerary itinerary)
        {
            if (id != itinerary.ItineraryId)
            {
                return BadRequest("Itinerary ID mismatch.");
            }

            var updatedItinerary = await _itineraryService.UpdateItinerary(itinerary);
            if (updatedItinerary != null)
            {
                return Ok(updatedItinerary);
            }
            return NotFound("Itinerary not found.");
        }

        [HttpDelete("deleteItinerary")]
        public async Task<ActionResult<Itinerary>> DeleteItinerary(int id)
        {
            var deletedItinerary = await _itineraryService.DeleteItinerary(id);
            if (deletedItinerary != null)
            {
                return Ok(deletedItinerary);
            }
            return NotFound("Itinerary not found.");
        }

        [HttpGet("getItineraryById")]
        public async Task<ActionResult<Itinerary>> GetItinerary(int id)
        {
            var itinerary = await _itineraryService.GetItineraryById(id);
            if (itinerary != null)
            {
                return Ok(itinerary);
            }
            return NotFound("Itinerary not found.");
        }


        [HttpGet("getAllItinerary")]
        public async Task<ActionResult<IEnumerable<Itinerary>>> GetAllItineraries()
        {
            var itineraries = await _itineraryService.GetAllItineraries();
            if (itineraries != null && itineraries.Count > 0)
            {
                return Ok(itineraries);
            }
            return NotFound("No itineraries found.");
        }
    }
}
