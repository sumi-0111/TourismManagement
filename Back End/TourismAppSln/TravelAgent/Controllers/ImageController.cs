using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Octokit.Internal;
using Refit; 

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public ImageController(IWebHostEnvironment environment)
        {
            this.environment= environment;
        }
        [HttpPut("UploadImage")]
        private async Task<IActionResult> UploadImage(IFormFile formFile, string productcode)
        {
            ApiResponse response = new ApiResponse();
            try
            {

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;

            }
            return Ok(response);
        }
    }
}
