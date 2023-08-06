using TourPackage.Interfaces;
using TourPackage.Models;

namespace TourPackage.Services
{
    public class PackageServices : IPackageService
    {
        private readonly IRepo<int, Package> _packageRepo;

        public PackageServices(IRepo<int, Package> packageRepo)
        {
            _packageRepo = packageRepo;
        }
        public async Task<Package?> AddPackage(Package package)
        {
            try
            {
                return await _packageRepo.Add(package);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding package: " + ex.Message);
                return null;
            }
        }

        public async Task<Package?> DeletePackage(int id)
        {
            try
            {
                var package = await _packageRepo.Get(id);
                if (package != null)
                {
                    return await _packageRepo.Delete(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting package: " + ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Package>?> GetAllPackages()
        {
            try
            {
                var packages = await _packageRepo.GetAll();
                return packages;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting all packages: " + ex.Message);
                return null;
            }
        }

        public async Task<Package?> GetPackageById(int id)
        {
            try
            {
                return await _packageRepo.Get(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting package: " + ex.Message);
                return null;
            }
        }

        public async  Task<Package?> UpdatePackage(Package package)
        {
            try
            {
                return await _packageRepo.Update(package);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating package: " + ex.Message);
                return null;
            }
        }
    }
}
