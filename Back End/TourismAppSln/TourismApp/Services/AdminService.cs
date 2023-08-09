using TourismApp.Interfaces;
using TourismApp.Models;
using TourismApp.Models.DTO;

namespace TourismApp.Services
{
    public class AdminService : IAdminService

    {
        private readonly IRepo<int, TravelAgent> _agentRepo;

        public AdminService(IRepo<int,TravelAgent> repo)
        {
            _agentRepo = repo;
        }
        public async Task<TravelAgent?> UpdateStatus(StatusDTO status)
        {
            try
            {
                var agent = await _agentRepo.Get(status.TravelAgentId);
                if (agent == null) { return null; }
                agent.TravelAgentStatus = status.TravelAgentStatus;
                var updatedAgent = await _agentRepo.Update(agent);
                if (updatedAgent == null) { return null; }
                return updatedAgent;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
