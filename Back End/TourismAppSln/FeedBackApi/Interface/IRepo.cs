namespace FeedBackApi.Interface
{
    public interface IRepo<T>
    {
        public Task<T?> Add(T item);
      
        public Task<ICollection<T>?> GetAll();
    }
}
