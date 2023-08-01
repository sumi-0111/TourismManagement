using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TourismApp.Interfaces;
using TourismApp.Models;

namespace TourismApp.Services
{
    public class TravellerRepo : IRepo<int, Traveller>
    {
        private readonly Context _context;
        private readonly ILogger<Traveller> _logger;

        public TravellerRepo(Context context, ILogger<Traveller> logger)
        {
            _context=context;
            _logger=logger;
        }
        public async Task<Traveller?> Add(Traveller item)
        {
            try
            {
                _context.Travellers.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Traveller?> Delete(int key)
        {

            try
            {
                var traveller = await Get(key);
                if (traveller != null)
                {
                    _context.Travellers.Remove(traveller);
                    await _context.SaveChangesAsync();
                    return traveller;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public  async Task<Traveller?> Get(int key)
        {
            try
            {
                var traveller = await _context.Travellers.Include(i => i.User).FirstOrDefaultAsync(i => i.TravellerId == key);
                return traveller;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Traveller>?> GetAll()
        {
            try
            {
                var traveller = await _context.Travellers.Include(i => i.User).ToListAsync();
                if (traveller.Count > 0)
                    return traveller;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Traveller?> Update(Traveller item)
        {
            try
            {
                var traveller = await Get(item.TravellerId);
                if (traveller != null)
                {
                    traveller.TravellerId = item.TravellerId;
                    traveller.TravellerName = item.TravellerName;
                    traveller.User = item.User;
                    traveller.TravellerEmail = item.TravellerEmail;
                    traveller.TravellerPhoneNo = item.TravellerPhoneNo;
                    traveller.TravellerAddress = item.TravellerAddress;
                    traveller.TravellerGender = item.TravellerGender;
                    return traveller;
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
