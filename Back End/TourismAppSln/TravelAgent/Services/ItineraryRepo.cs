using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Services
{
    public class ItineraryRepo : IRepo<int, Itinerary>
    {
        private readonly TourPackageContext _context;
        private readonly ILogger _logger; 

        public ItineraryRepo(TourPackageContext tourPackageContext, ILogger<Itinerary> logger)
        {
            _context=tourPackageContext;
            _logger= logger;
        }
        public async Task<Itinerary?> Add(Itinerary item)
        {
            try
            {
                _context.Itineraries.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Itinerary?> Delete(int key)
        {
            try
            {
                var doc = await Get(key);
                if (doc != null)
                {
                    _context.Itineraries.Remove(doc);
                    await _context.SaveChangesAsync();
                    return doc;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Itinerary?> Get(int key)
        {
            try
            {
                var doc = await _context.Itineraries.FirstOrDefaultAsync(i => i.ItineraryId == key);
                return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Itinerary>?> GetAll()
        {
            try
            {
                var doc = await _context.Itineraries.ToListAsync();
                if (doc.Count > 0)
                    return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Itinerary?> Update(Itinerary item)
        {
            try
            {
                var existingDoctor = await _context.Itineraries.FindAsync(item.ItineraryId);
                if (existingDoctor != null)
                {
                    existingDoctor.DayandVisit = item.DayandVisit;
                    existingDoctor.DestinationName = item.DestinationName;


                    await _context.SaveChangesAsync();

                    return existingDoctor;
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


