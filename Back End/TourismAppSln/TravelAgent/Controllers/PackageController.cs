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
        private readonly IRepo<int, Package> _packageRepo;

        public PackageController(IRepo<int,Package> packagRepo)
        {
            _packageRepo= packagRepo;
        }



        [HttpPost]
        public async Task<ActionResult<Package>> AddContactDetails(Package contactDetails)
        {
            var result = await _packageRepo.Add(contactDetails);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add contact details.");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Package>> UpdateContactDetails(int id, Package contactDetails)
        {
            if (id != contactDetails.PackageId)
            {
                return BadRequest("ContactDetails ID mismatch.");
            }

            var result = await _packageRepo.Update(contactDetails);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("ContactDetails not found.");
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
   
