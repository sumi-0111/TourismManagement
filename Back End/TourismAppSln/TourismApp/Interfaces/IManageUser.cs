using System.Numerics;
using TourismApp.Models;
using TourismApp.Models.DTO;

namespace TourismApp.Interfaces
{
    public interface IManageUser
    {
        public Task<UserDTO> Login(UserDTO user);
        public Task<UserDTO> RegisterTravelAgent(TravelAgentDTO travelAgent);
        public Task<UserDTO> RegisterTraveller(TravellerDTO traveller);
    }
}
