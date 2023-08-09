using TourismApp.Models;
using TourismApp.Models.DTO;

namespace TourismApp.Interfaces
{
    public interface IAdminService
    {
        public Task<TravelAgent?> UpdateStatus(StatusDTO status);

    }
}
