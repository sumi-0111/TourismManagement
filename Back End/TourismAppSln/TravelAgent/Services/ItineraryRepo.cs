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
        public async Task<Itinerary?> Add(Itinerary item, IFormFile imageFile, IFormFile secondImageFile)
        {
            try
            {
                // Save the first image file to the specified location if it exists
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

                // Save the second image file to the specified location if it exists
                if (secondImageFile != null && secondImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "Food");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(secondImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await secondImageFile.CopyToAsync(stream);
                    }

                    item.FoodImage = fileName;
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

        public async Task<Itinerary?> Update(IteneraryImages item)
        {
            // Access images using item.ItineraryImageOne, item.ItineraryImageTwo, item.FoodImage
            // Convert them to Base64 strings and store them in the corresponding properties of the existing Itinerary object

            try
            {
                var existingItinerary = await _context.Itineraries.FindAsync(item.Itinerary.ItineraryId);
                if (existingItinerary != null)
                {
                    existingItinerary.PackageName = item.Itinerary.PackageName;
                    existingItinerary.Hotels = item.Itinerary.Hotels;
                    existingItinerary.FoodDetails = item.Itinerary.FoodDetails;
                    existingItinerary.DestinationDescription = item.Itinerary.DestinationDescription;
                    existingItinerary.DayandVisit = item.Itinerary.DayandVisit;
                    existingItinerary.ItineraryImageOne = item.ItineraryImageOne;
                    existingItinerary.ItineraryImageTwo = item.ItineraryImageTwo;
                    existingItinerary.FoodImage = item.FoodImage;

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

