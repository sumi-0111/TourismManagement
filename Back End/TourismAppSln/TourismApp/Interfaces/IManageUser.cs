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
        public Task<TravelAgentDTO> ApprovedAgent(TravelAgentDTO user);

        public Task<TravelAgent> ApprovedAgenta(TravelAgent user);

        public Task<TravelAgent> DisApprovedAgenta(TravelAgent user);

        public Task<TravelAgentDTO> DisapproveAgent(TravelAgentDTO user);

    }
}
