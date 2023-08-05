using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravellerFeedBackAPI.Interface;
using TravellerFeedBackAPI.Models;

namespace TravellerFeedBackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IRepo<int, UserFeedBack> _repo;

        public FeedBackController(IRepo<int,UserFeedBack> repo)
        {
            _repo = repo;
        }
        [HttpPost]
        public async Task<ActionResult<UserFeedBack>> AddContactDetails(UserFeedBack contactDetails)
        {
            var result = await _repo.Add(contactDetails);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add contact details.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserFeedBack>> UpdateContactDetails(int id, UserFeedBack contactDetails)
        {
            if (id != contactDetails.FeedbackID)
            {
                return BadRequest("ContactDetails ID mismatch.");
            }

            var result = await _repo.Update(contactDetails);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("ContactDetails not found.");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserFeedBack>> DeleteItinerary(int id)
        {
            var deletedItinerary = await _repo.Delete(id);
            if (deletedItinerary != null)
            {
                return Ok(deletedItinerary);
            }
            return NotFound("Itinerary not found.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserFeedBack>> GetItinerary(int id)
        {
            var itinerary = await _repo.Get(id);
            if (itinerary != null)
            {
                return Ok(itinerary);
            }
            return NotFound("Itinerary not found.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserFeedBack>>> GetAllItineraries()
        {
            var itineraries = await _repo.GetAll();
            if (itineraries != null && itineraries.Count > 0)
            {
                return Ok(itineraries);
            }
            return NotFound("No itineraries found.");
        }
    }
}
