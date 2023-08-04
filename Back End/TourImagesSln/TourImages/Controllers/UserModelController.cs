using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourImages.Models;

namespace TourImages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserModelController : ControllerBase
    {
        private readonly UserModelDbContext _context;

        public UserModelController(UserModelDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
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


        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] UserModel model)
        {
            if (model.Image != null)
            {
                // Connect to Azurite Blob Storage
                // string connectionString = "UseDevelopmentStorage=true"; // Azurite connection string
                //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                string connectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:8888/devstoreaccount1;";
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("samples-workitems");

                // Generate a unique blob name
                string uniqueBlobName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);

                // Upload the image to Azure Blob Storage
                BlobClient blobClient = containerClient.GetBlobClient(uniqueBlobName);
                using (var stream = model.Image.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                // Set the image path to the Blob Storage URL
                model.ImagePath = blobClient.Uri.ToString();
            }

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
}
