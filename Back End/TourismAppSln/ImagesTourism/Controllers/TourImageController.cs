using Azure.Storage.Blobs;
using ImagesTourism.Interfaces;
using ImagesTourism.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImagesTourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourImageController : ControllerBase
    {
        private readonly ITourImageServices _tourImageService;

        public TourImageController(ITourImageServices tourImageService)
        {
            _tourImageService = tourImageService;
        }
        [HttpGet("GettingImages")]
        public async Task<ActionResult<IEnumerable<ImageTourism>>> GetTourImages()
        {
            var tourImages = await _tourImageService.GetAllTourImage();
            return Ok(tourImages);
        }
        /* [HttpPost]
         public async Task<IActionResult> UploadImage([FromForm] UserModel model)
         {
             if (model.Image != null)
             {
                 string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                 string uniqueFileName = model.Image.FileName;
                 string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                 using (var stream = new FileStream(filePath, FileMode.Create))
                 {
                     await model.Image.CopyToAsync(stream);
                 }

                 model.ImagePath = "wwwroot/images" + uniqueFileName;
             }

             _context.Users.Add(model);
             await _context.SaveChangesAsync();

             return Ok();
         }*/


        [HttpPost("PostingImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImages(int packageId, [FromForm] ImageTourism model)
        {
            if (model.Image != null && model.Image.Count > 0)
            {
                foreach (var image in model.Image)
                {
                    if (image != null)
                    {
                        // Assuming the business logic for uploading and adding the image to the database is handled in the TourImageService.
                        // Pass the packageId and image model to the TourImageService to perform the necessary operations.
                        await _tourImageService.AddTourImage(packageId, image, model.Name);
                    }
                }
            }

            return Ok();
        }
    }
}

