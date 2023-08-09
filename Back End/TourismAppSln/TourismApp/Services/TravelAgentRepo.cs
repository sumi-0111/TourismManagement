using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using TourismApp.Interfaces;
using TourismApp.Models;

namespace TourismApp.Services
{
    public class TravelAgentRepo : IRepo<int, TravelAgent>
    {
        private readonly Context _context;
        private readonly ILogger<TravelAgent> _logger;

        public TravelAgentRepo(Context context, ILogger<TravelAgent> logger)
        {
            _context = context;
            _logger=logger;
        }
        public async Task<TravelAgent?> Add(TravelAgent item)
        {
            try
            {
                _context.TravelAgents.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<TravelAgent?> Delete(int key)
        {
            try
            {
                var  travelAgent = await Get(key);
                if (travelAgent != null)
                {
                    _context.TravelAgents.Remove(travelAgent);
                    await _context.SaveChangesAsync();
                    return travelAgent;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<TravelAgent?> Get(int key)
        {
            try
            {
                var travelAgent = await _context.TravelAgents.FirstOrDefaultAsync(i => i.TravelAgentId == key);
                return travelAgent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<TravelAgent>?> GetAll()
        {
            try
            {
                var travelAgents = await _context.TravelAgents.Include(i => i.User).ToListAsync();
                if (travelAgents.Count > 0)
                    return travelAgents;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<TravelAgent?> Update(TravelAgent item)
        {
            try
            {
                var travelAgent = await Get(item.TravelAgentId);
                if (travelAgent != null)
                {
                    travelAgent.TravelAgentName = item.TravelAgentName;
                    travelAgent.TravelAgentEmail = item.TravelAgentEmail;
                    travelAgent.TravelAgentPhoneNo = item.TravelAgentPhoneNo;
                    travelAgent.CompanyName = item.CompanyName;
                    travelAgent.CompanyAddress = item.CompanyAddress;
                    travelAgent.TravelAgentStatus = item.TravelAgentStatus;
                    travelAgent.TravelAgentPhoneNo = item.TravelAgentPhoneNo;
                    await _context.SaveChangesAsync();

                    return travelAgent;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
