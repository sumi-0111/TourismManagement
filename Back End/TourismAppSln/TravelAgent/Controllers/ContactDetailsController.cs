using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Services;

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailsController : ControllerBase
    {

        private readonly IContactService<int, ContactDetails> _contactDetailsServices; 

        public ContactDetailsController(IContactService<int,ContactDetails> contactDetailsServices)
        {
            _contactDetailsServices = contactDetailsServices;


    }
        [HttpPost]
        public async Task<ActionResult<ContactDetails>> AddContactDetails(ContactDetails contactDetails)
        {
            try
            {
                var result = await _contactDetailsServices.AddContactDetails(contactDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add contact details. Error: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<ContactDetails>> UpdateContactDetails(int id, ContactDetails contactDetails)
        {
            try
            {
                if (id != contactDetails.ContactId)
                {
                    return BadRequest("ContactDetails ID mismatch.");
                }

                var result = await _contactDetailsServices.UpdateContactDetails(contactDetails);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("ContactDetails not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update contact details. Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactDetails>> DeleteContactDetails(int id)
        {
            try
            {
                var result = await _contactDetailsServices.DeleteContactDetails(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("ContactDetails not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete contact details. Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDetails>> GetContactDetails(int id)
        {
            var result = await _contactDetailsServices.GetContactDetails(id);
            if (result != null) 
            {
                return Ok(result);
            }
            return NotFound("ContactDetails not found.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDetails>>> GetAllContactDetails()
        {
            var result = await _contactDetailsServices.GetAllContactDetails();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No contact details found.");
        }

    }
}
