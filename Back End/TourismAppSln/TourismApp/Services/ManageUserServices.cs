using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using TourismApp.Interfaces;
using TourismApp.Models;
using TourismApp.Models.DTO;

namespace TourismApp.Services
{
    public class ManageUserServices : IManageUser
    {
        private readonly IRepo<string, User> _userRepo;
        private readonly IGenerateToken _tokenService;
        private readonly IRepo<int, TravelAgent> _travelAgentRepo;
        private readonly IRepo<int, Traveller> _travellerRepo;

        public ManageUserServices(IRepo<string,User> userRepo,IGenerateToken tokenService,IRepo<int,TravelAgent> travelAgent, IRepo<int,Traveller> travellerRepo)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _travelAgentRepo=travelAgent;
            _travellerRepo = travellerRepo;
        }

        public async Task<TravelAgentDTO> ApprovedAgent(TravelAgentDTO agentStatus)
        {
            var agent = await _travelAgentRepo.Get(agentStatus.TravelAgentId);

            if (agent != null)
            {
                agent.TravelAgentStatus = "Approved";
                await _travelAgentRepo.Update(agent);
                return new TravelAgentDTO
                {
                    TravelAgentId = agent.TravelAgentId,
                    TravelAgentStatus = agent.TravelAgentStatus
                };
            }
            return null;
        }

        public   async Task<TravelAgent> ApprovedAgenta(TravelAgent user)
        {
            var agent = await _travelAgentRepo.Get(user.TravelAgentId);

            if (agent != null)
            {
                agent.TravelAgentStatus = "Approved";
                await _travelAgentRepo.Update(agent);
                return new TravelAgent
                {
                    TravelAgentId = agent.TravelAgentId,
                    TravelAgentStatus = agent.TravelAgentStatus
                };
            }
            return null;
        }

        public async Task<TravelAgentDTO> DisapproveAgent(TravelAgentDTO agentStatus)
        {
            var agent = await _travelAgentRepo.Get(agentStatus.TravelAgentId);

            if (agent != null)
            {
                agent.TravelAgentStatus = "Not Approved";
                await _travelAgentRepo.Update(agent);
                return new TravelAgentDTO
                {
                    TravelAgentId = agent.TravelAgentId,
                    TravelAgentStatus = agent.TravelAgentStatus
                };
            }
            return null;
        }

        public Task<TravelAgent> DisApprovedAgenta(TravelAgent user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> Login(UserDTO user)
        {
            var userData = await _userRepo.Get(user.Email);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.PasswordKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordClear));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.PasswordHash[i])
                        return null;
                }
                user = new UserDTO();
                user.UserId = userData.UserId;
                user.Email=userData.Email;
                user.Role = userData.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }

     

        public async Task<UserDTO> RegisterTravelAgent(TravelAgentDTO travelAgent)
        {
            UserDTO user = null;
            var hmac = new HMACSHA512();

            travelAgent.User = new User(); // Instantiate the User object

            travelAgent.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(travelAgent.PasswordClear));
            travelAgent.User.PasswordKey = hmac.Key;
            travelAgent.User.Email = travelAgent.TravelAgentEmail;
            travelAgent.User.Role = "Travel Agent";
            travelAgent.TravelAgentStatus = "Pending";

            var userResult = await _userRepo.Add(travelAgent.User);
            var travelAgentResult = await _travelAgentRepo.Add(travelAgent);

            if (userResult != null && travelAgentResult != null)
            {
                user = new UserDTO();
                user.UserId = travelAgentResult.TravelAgentId;
                user.Email = travelAgentResult.TravelAgentEmail;
                user.UserId = userResult.UserId;
                user.Email = userResult.Email;
                user.Role = userResult.Role;
                user.Token = _tokenService.GenerateToken(user);
            }

            return user;
        }

        public async Task<UserDTO> RegisterTraveller(TravellerDTO traveller)
        {
            UserDTO user = null;
            var hmac = new HMACSHA512();

            traveller.User = new User(); // Instantiate the User object

            traveller.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(traveller.PasswordClear));
            traveller.User.PasswordKey = hmac.Key;
            traveller.User.Role = "Traveller";
            traveller.User.Email = traveller.TravellerEmail;

            var userResult = await _userRepo.Add(traveller.User);
            var travellerResult = await _travellerRepo.Add(traveller);

            if (userResult != null && travellerResult != null)
            {
                user = new UserDTO();
                user.UserId = travellerResult.TravellerId;
                user.Role = userResult.Role;
                user.Email = travellerResult.TravellerEmail;
                user.Token = _tokenService.GenerateToken(user); 
            }

            return user;
        }
    }
}
