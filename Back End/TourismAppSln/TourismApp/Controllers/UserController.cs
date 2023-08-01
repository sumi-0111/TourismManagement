using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourismApp.Interfaces;
using TourismApp.Models;
using TourismApp.Models.DTO;

namespace TourismApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CORS")]
    public class UserController : ControllerBase
    {
        private readonly IManageUser _manageUser;
        private readonly IRepo<int, TravelAgent> _travelAgentRepo;
        private readonly IRepo<int, Traveller> _travellerRepo;

        public UserController(IManageUser manageUser, IRepo<int,TravelAgent> travelAgentRepo, IRepo<int,Traveller> travellerRepo)
        {
            _manageUser = manageUser;
            _travelAgentRepo = travelAgentRepo;
            _travellerRepo = travellerRepo;
        }
        [HttpPost("TravelAgent")]
        [ProducesResponseType(typeof(ActionResult<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> RegisterTravelAgent(TravelAgentDTO intern)
        {
            var result = await _manageUser.RegisterTravelAgent(intern);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to register at this moment");
        }

        [HttpPost("Traveller")]
        [ProducesResponseType(typeof(ActionResult<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> RegisterTraveller(TravellerDTO intern)
        {
            var result = await _manageUser.RegisterTraveller(intern);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to register at this moment");
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(ActionResult<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(UserDTO user)
        {
            var result = await _manageUser.Login(user);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to login");
        }
    }
}
