using Microsoft.AspNetCore.Mvc;
using FeedBackApi.Interface;
using FeedBackApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedBackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IRepo<FeedBack> _feedbackRepo;
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(IRepo<FeedBack> feedbackRepo, ILogger<FeedbackController> logger)
        {
            _feedbackRepo = feedbackRepo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<FeedBack>> AddFeedback(FeedBack feedback)
        {
            try
            {
                var addedFeedback = await _feedbackRepo.Add(feedback);
                if (addedFeedback != null)
                {
                    return Ok(addedFeedback);
                }
                else
                {
                    return BadRequest("Failed to add feedback.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding feedback.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<FeedBack>>> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = await _feedbackRepo.GetAll();
                if (feedbacks != null)
                {
                    return Ok(feedbacks);
                }
                else
                {
                    return NotFound("No feedbacks found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving feedbacks.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
