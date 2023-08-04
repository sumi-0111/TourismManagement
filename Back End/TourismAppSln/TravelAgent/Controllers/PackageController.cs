using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IRepo<int, Package> _packageRepo;

        public PackageController(IRepo<int,Package> packagRepo)
        {
            _packageRepo= packagRepo;
        }


        [HttpPost]
        public async Task<IActionResult> AddTourPackage([FromForm] Package tourPackage, IFormFile imageFile)
        {
            try
            {
                var addedPackage = await _packageRepo.Add(tourPackage, imageFile);
                if (addedPackage != null)
                {
                    return CreatedAtAction("AddTourPackage", addedPackage);
                }
                return BadRequest("Failed to add tour package.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Package>> UpdateTourPackage(int id, [FromForm] Package tourPackage, [FromForm] IFormFile imageFile)
        {
            if (id != tourPackage.PackageId)
            {
                return BadRequest("TourPackage ID mismatch.");
            }

            var result = await _packageRepo.Update(tourPackage, imageFile);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("TourPackage not found.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Package>> DeleteTourPackage(int id)
        {
            var result = await _packageRepo.Delete(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("TourPackage not found.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetTourPackage(int id)
        {
            var result = await _packageRepo.Get(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("TourPackage not found.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetAllTourPackages()
        {
            var result = await _packageRepo.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No tour packages found.");
        }
    }

}
   
