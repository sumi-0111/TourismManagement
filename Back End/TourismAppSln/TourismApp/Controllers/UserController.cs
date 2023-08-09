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
        private readonly IAdminService _adminService;

        public UserController(IManageUser manageUser, IRepo<int,TravelAgent> travelAgentRepo, IRepo<int,Traveller> travellerRepo,IAdminService adminService)
        {
            _manageUser = manageUser;
            _travelAgentRepo = travelAgentRepo;
            _travellerRepo = travellerRepo;
            _adminService = adminService;
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
        [HttpPost("approve")]
        [ProducesResponseType(typeof(ActionResult<TravelAgentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TravelAgentDTO>> ApproveAgent(TravelAgentDTO agentStatus)
        {
            try
            {
                var result = await _manageUser.ApprovedAgent(agentStatus);

                if (result != null)
                    return Ok(result);

                return BadRequest("Unable to approve agent");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred during agent approval: {ex.Message}");
            }
        }
        [HttpPost("disapprove")]
        [ProducesResponseType(typeof(ActionResult<TravelAgentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TravelAgentDTO>> DisapproveAgent(TravelAgentDTO agentStatus)
        {
            try
            {
                var result = await _manageUser.DisapproveAgent(agentStatus);

                if (result != null)
                    return Ok(result);

                return BadRequest("Unable to disapprove agent");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred during agent disapproval: {ex.Message}");
            }
        }

        //[HttpPost("demo")]
        //[ProducesResponseType(typeof(ActionResult<TravelAgent>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<TravelAgent>> ApprovedAgenta(TravelAgent agentStatus)
        //{
        //    try
        //    {
        //        var result = await _manageUser.ApprovedAgenta(agentStatus);

        //        if (result != null)
        //            return Ok(result);

        //        return BadRequest("Unable to approve agent");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"An error occurred during agent approval: {ex.Message}");
        //    }
        //}
        //[HttpPost("demo")]
        //[ProducesResponseType(typeof(ActionResult<TravelAgent>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<TravelAgent>> DisApprovedAgenta(TravelAgent agentStatus)
        //{
        //    try
        //    {
        //        var result = await _manageUser.DisApprovedAgenta(agentStatus);

        //        if (result != null)
        //            return Ok(result);

        //        return BadRequest("Unable to Disapprove agent");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"An error occurred during agent disapproval: {ex.Message}");
        //    }
        //}

        [HttpPut]
        [ProducesResponseType(typeof(TravelAgent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        public async Task<ActionResult<TravelAgent?>> UpdateAgentStatus(StatusDTO status)
        {
            try
            {
                var agent = await _adminService.UpdateStatus(status);
                if (agent != null)
                {
                    return Ok(agent);
                }
                return BadRequest("Not updated!");
            }
            catch (Exception)
            {
                return BadRequest("Backend error!");
            }
        }
    } 
}
