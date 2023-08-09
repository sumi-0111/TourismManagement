using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourismApp.Models;
using TourismApp.Models.DTO;
using TourismApp.Services;

namespace TourismApp.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TravelAgentController : ControllerBase
    {

        private readonly TravelAgentRepo _travelAgentRepo;
        public TravelAgentController(TravelAgentRepo travelAgentRepo)
        {
            _travelAgentRepo = travelAgentRepo;
            
        }

        [HttpGet]
        public ActionResult<IList<TravelAgent>> GetAllTravellers()
        { 
            var travellers = _travelAgentRepo.GetAll().Result;

            if (travellers == null)
            {
                return NotFound();
            }

            return Ok(travellers);
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<TravelAgent>>> GetAllTravelAgents()
        {
            try
            {
                var travelAgents = await _travelAgentRepo.GetAll();
                if (travelAgents != null)
                    return Ok(travelAgents);
                else
                    return NotFound(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500); 
            }
        }
    }
}

