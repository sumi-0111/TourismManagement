using Microsoft.AspNetCore.Mvc;
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

        public PackageRepo(TourPackageContext context, ILogger<Package> logger)
        {
            _context=context;
            _logger=logger;
        } 
        public async Task<Package?> Add(Package item)
        {
            try
            {
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

        public async Task<Package?> Update(Package item)
        {
            try
            {
                var existingDoctor = await _context.Packages.FindAsync(item.PackageId);
                if (existingDoctor != null)
                {
                    existingDoctor.PackageName = existingDoctor.PackageName;
                    existingDoctor.TravelAgencyName = item.TravelAgencyName;
                    existingDoctor.Description = item.Description;
                    existingDoctor.Rate = item.Rate;
                    existingDoctor.Destination = item.Destination;
                    existingDoctor.DeparturePoint = item.DeparturePoint;
                    existingDoctor.StartDate = item.StartDate;
                    existingDoctor.EndDate = item.EndDate;
                    existingDoctor.ArrivalPoint = item.ArrivalPoint;
                    existingDoctor.AvailablityCount = item.AvailablityCount;
                    existingDoctor.Transportation = item.Transportation;


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

