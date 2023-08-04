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
        private readonly IWebHostEnvironment _environment;

        public ItineraryRepo(TourPackageContext tourPackageContext, ILogger<Itinerary> logger,IWebHostEnvironment environment)
        {
            _context=tourPackageContext;
            _logger= logger;
            _environment=environment;
        }
        public async Task<Itinerary?> Add(Itinerary item, IFormFile imageFile)
        {
            try
            {
                // Save the image file to the specified location if it exists
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "Itinerary");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    item.ItineraryImage = fileName;
                }

                _context.Itineraries.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
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

        public async Task<Itinerary?> Update(Itinerary item, IFormFile imageFile)
        {
            try
            {
                var existingItinerary = await _context.Itineraries.FindAsync(item.ItineraryId);
                if (existingItinerary != null)
                {
                    existingItinerary.PackageName = item.PackageName;
                    existingItinerary.DayandVisit = item.DayandVisit;
                    existingItinerary.DestinationDescription = item.DestinationDescription;
                    existingItinerary.FoodDetails = item.FoodDetails;

                    // Save the new image file to the specified location if it exists
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "Itinerary");
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        existingItinerary.ItineraryImage = fileName;
                    }

                    await _context.SaveChangesAsync();

                    return existingItinerary;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

    }
}

