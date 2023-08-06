using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Services;

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }



        [HttpPost("packageCreate")]
        public async Task<ActionResult<Package>> AddTourPackage(Package tourPackage)
        {
            var result = await _packageService.AddPackage(tourPackage);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add tour package.");
        }



        [HttpPut("updatePackage")]
        public async Task<ActionResult<Package>> UpdateTourPackage(int id, Package tourPackage)
        {
            if (id != tourPackage.PackageId)
            {
                return BadRequest("TourPackage ID mismatch.");
            }

            var result = await _packageService.UpdatePackage(tourPackage);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("TourPackage not found.");
        }

        [HttpDelete("deletePackage")]
        public async Task<ActionResult<Package>> DeleteTourPackage(int id)
        {
            var result = await _packageService.DeletePackage(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("TourPackage not found.");
        }

        [HttpGet("getPackageById")]
        public async Task<ActionResult<Package>> GetTourPackage(int id)
        {
            var result = await _packageService.GetPackageById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("TourPackage not found.");
        }

        [HttpGet("getAllPackages")]
        public async Task<ActionResult<IEnumerable<Package>>> GetAllTourPackages()
        {
            var result = await _packageService.GetAllPackages();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No tour packages found.");
        }
    }

}
   
