using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Services
{
    public class ContactDetailsRepo : IRepo<int, ContactDetails>
    {
        private readonly TourPackageContext _context;
        private readonly ILogger<ContactDetails> _logger;
        private readonly IWebHostEnvironment _environment;

        public ContactDetailsRepo(TourPackageContext context,ILogger<ContactDetails> logger,IWebHostEnvironment environment)
        {
            _context=context;
            _logger=logger;
            _environment=environment;
        }
        public async Task<ContactDetails?> Add([FromForm] ContactDetails item, IFormFile imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "ContactDetails");
                    var fileName = imageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    item.MapImage = fileName;
                }
                else
                {
                    // Handle the case when no image file is provided, or the file is invalid.
                    // You can choose to throw an exception, return an error response, or set a default image, etc.
                    throw new ArgumentException("Invalid image file.");
                }

                _context.Contacts.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<ContactDetails?> Delete(int key)
        {
            try
            {
                var doc = await Get(key);
                if (doc != null)
                {
                    _context.Contacts.Remove(doc);
                    await _context.SaveChangesAsync();
                    return doc;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ContactDetails?> Get(int key)
        {
            try
            {
                var doc = await _context.Contacts.FirstOrDefaultAsync(i => i.ContactId == key);
                return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<ContactDetails>?> GetAll()
        {
            try
            {
                var doc = await _context.Contacts.ToListAsync();
                if (doc.Count > 0)
                    return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ContactDetails?> Update(ContactDetails item, IFormFile imageFile)
        {
            try
            {
                var existingContact = await _context.Contacts.FindAsync(item.ContactId);
                if (existingContact != null)
                {
                    existingContact.Package = item.Package;
                    existingContact.TravelAgentName = item.TravelAgentName;
                    existingContact.Email = item.Email;
                    existingContact.Phone = item.Phone;

                    // Save the new image file to the specified location if it exists
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "ContactDetails");
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        existingContact.MapImage = fileName;
                    }

                    await _context.SaveChangesAsync();

                    return existingContact;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}

