using TourPackage.Models;

namespace TourPackage.Interfaces
{
    public interface IContactService<K,T>
    {
        public Task<T> AddContactDetails(T item);
        public Task<T> UpdateContactDetails(T item);
        public Task<T> GetContactDetails(K id);
        public Task<T> DeleteContactDetails(K id);
        public Task<ICollection<T>> GetAllContactDetails();
    }
}
