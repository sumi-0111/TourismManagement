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
        private readonly IRepo<int, Itinerary> _itineraryRepo;

        public ItineraryController(IRepo<int,Itinerary> itineraryRepo)
        {
            _itineraryRepo = itineraryRepo;
        }
        [HttpPost]
        public async Task<ActionResult<Itinerary>> AddContactDetails(Itinerary contactDetails)
        {
            var result = await _itineraryRepo.Add(contactDetails);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add contact details.");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Itinerary>> UpdateContactDetails(int id, Itinerary contactDetails)
        {
            if (id != contactDetails.ItineraryId)
            {
                return BadRequest("ContactDetails ID mismatch.");
            }

            var result = await _itineraryRepo.Update(contactDetails);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("ContactDetails not found.");
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
