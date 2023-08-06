using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Services
{
    public class ContactDetailsService : IContactService<int,ContactDetails>
    {
        private readonly IRepo<int, ContactDetails> _contactDetailsRepo;
        private readonly ILogger<ContactDetails> _logger;

        public ContactDetailsService(IRepo<int, ContactDetails> contactDetailsRepo, ILogger<ContactDetails> logger)
        {
            _contactDetailsRepo = contactDetailsRepo;
            _logger = logger;
        }

        public async Task<ContactDetails?> AddContactDetails(ContactDetails contactDetails)
        {
            try
            {
                return await _contactDetailsRepo.Add(contactDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<ContactDetails> GetContactDetails(int contactId)
        {
            try
            {
                return await _contactDetailsRepo.Get(contactId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<ICollection<ContactDetails>?> GetAllContactDetails()
        {
            try
            {
                return await _contactDetailsRepo.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<ContactDetails?> UpdateContactDetails(ContactDetails contactDetails)
        {
            try
            {
                return await _contactDetailsRepo.Update(contactDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<ContactDetails?> DeleteContactDetails(int contactId)
        {
            try
            {
                return await _contactDetailsRepo.Delete(contactId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
