using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Services
{
    public class HotelRepo : IRepo<int, Hotel>
    {
        private readonly TourPackageContext _context;
        private readonly ILogger<Hotel> _logger;
        private readonly IWebHostEnvironment _environment;

        public HotelRepo(TourPackageContext context, ILogger<Hotel> logger,IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment= environment;
        }
        public async Task<Hotel?> Add(Hotel item, IFormFile imageFile)
        {
            try
            {
                // Save the image file to the specified location if it exists
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "Hotel");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    item.HotelImage = fileName;
                }

                _context.Hotels.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Hotel?> Delete(int key)
        {
            try
            {
                var doc = await Get(key);
                if (doc != null)
                {
                    _context.Hotels.Remove(doc);
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

        public async Task<Hotel?> Get(int key)
        {
            try
            {
                var doc = await _context.Hotels.FirstOrDefaultAsync(i => i.HotelId == key);
                return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Hotel>?> GetAll()
        {
            try
            {
                var doc = await _context.Hotels.ToListAsync();
                if (doc.Count > 0)
                    return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Hotel?> Update(Hotel item, IFormFile imageFile)
        {
            try
            {
                var existingHotel = await _context.Hotels.FindAsync(item.HotelId);
                if (existingHotel != null)
                {
                    existingHotel.HotelName = item.HotelName;
                    existingHotel.Itinerary = item.Itinerary;
                    existingHotel.HotelRating = item.HotelRating;
                    existingHotel.RoomType = item.RoomType;
                    existingHotel.HotelFood = item.HotelFood;

                    // Save the new image file to the specified location if it exists
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "Hotel");
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        existingHotel.HotelImage = fileName;
                    }

                    await _context.SaveChangesAsync();

                    return existingHotel;
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
