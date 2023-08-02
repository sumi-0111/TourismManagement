using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Services
{
    public class PackageRepo : IRepo<int, Package>
    {
        private readonly TourPackageContext _context;
        private readonly ILogger<Package> _logger;
        private readonly IWebHostEnvironment _environment;

        public PackageRepo(TourPackageContext context, ILogger<Package> logger,IWebHostEnvironment environment)
        {
            _context=context;
            _logger=logger;
            _environment=environment;
        }
        public async Task<Package?> Add(Package item, IFormFile imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "Package");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    item.PackageImage = fileName;
                }

                _context.Packages.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Package?> Delete(int key)
        {
            try
            {
                var doc = await Get(key);
                if (doc != null)
                {
                    _context.Packages.Remove(doc);
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

        public async Task<Package?> Get(int key)
        {
            try
            {
                var doc = await _context.Packages.FirstOrDefaultAsync(i => i.PackageId == key);
                return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Package>?> GetAll()
        {
            try
            {
                var doc = await _context.Packages.ToListAsync();
                if (doc.Count > 0)
                    return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Package?> Update(Package item, IFormFile imageFile)
        {
            try
            {
                var existingDoctor = await _context.Packages.FindAsync(item.PackageId);
                if (existingDoctor != null)
                {
                    existingDoctor.TravelAgencyName = item.TravelAgencyName;
                    existingDoctor.PackageName = item.PackageName;
                    existingDoctor.Description = item.Description;
                    existingDoctor.Rate = item.Rate;
                    existingDoctor.DeparturePoint = item.DeparturePoint;
                    existingDoctor.ArrivalPoint = item.ArrivalPoint;
                    existingDoctor.AvailablityCount = item.AvailablityCount;
                    existingDoctor.TotalDays = item.TotalDays;
                    existingDoctor.Transportation = item.Transportation;

                    // Save the new image file to the specified location if it exists
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "Package");
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        existingDoctor.PackageImage = fileName;
                    }

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

