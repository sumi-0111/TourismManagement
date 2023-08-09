using TourismApp.Models;
using TourismApp.Models.DTO;

namespace TourismApp.Interfaces
{
    public interface IGenerateToken
    {
        public string GenerateToken(UserDTO user);

    }
}
