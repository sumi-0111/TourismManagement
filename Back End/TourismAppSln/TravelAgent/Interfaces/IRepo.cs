namespace TourPackage.Interfaces
{
    public interface IRepo<K,T>
    {
        public Task<T?> Add(T item , IFormFile formFile);
        Task<T?> Update(T item, IFormFile imageFile);
        public Task<T?> Delete(K key);
        public Task<T?> Get(K key);
        public Task<ICollection<T>?> GetAll();
    }
}
