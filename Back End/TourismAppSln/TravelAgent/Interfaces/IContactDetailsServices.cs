using TourPackage.Models;

namespace TourPackage.Interfaces
{
    public interface IContactDetailsServices
    {
        public Task<ContactDetails> AddContactDetails(ContactDetails item);
        public Task<ContactDetails> UpdateContactDetails(ContactDetails item);
        public Task<ContactDetails> GetContactDetails(int id);
        public Task<ContactDetails> DeleteContactDetails(int id);
        public Task<ICollection<ContactDetails>> GetAllContactDetails();
    }
}
