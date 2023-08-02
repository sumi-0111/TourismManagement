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

        public ContactDetailsRepo(TourPackageContext context,ILogger<ContactDetails> logger)
        {
            _context=context;
            _logger=logger;
        }
        public async Task<ContactDetails?> Add(ContactDetails item)
        {
            try
            {
                _context.Contacts.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
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

        public async Task<ContactDetails?> Update(ContactDetails item)
        {

            try
            {
                var existingDoctor = await _context.Contacts.FindAsync(item.ContactId);
                if (existingDoctor != null)
                {
                    existingDoctor.Package = item.Package;
                    existingDoctor.TravelAgentName = item.TravelAgentName;
                    existingDoctor.Email = item.Email;
                    existingDoctor.Phone = item.Phone;
                    existingDoctor.Email = item.Email;
                  

                    await _context.SaveChangesAsync(); // Save the changes to the database

                    return existingDoctor;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}

