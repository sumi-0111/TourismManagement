using ImagesTourism.Models;

namespace ImagesTourism.Interfaces
{
    public interface ITourImageServices
    {
        public Task<ImageTourism> AddTourImage(int packageId, IFormFile image, string name);
        public Task<ICollection<ImageTourism>> GetAllTourImage();
        public Task<ImageTourism> GetTourImage(int id);
    }
}
