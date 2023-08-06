using TourPackage.Models;

namespace TourPackage.Interfaces
{
    public interface IPackageService
    {
        public Task<Package?> AddPackage(Package package);
        public Task<Package?> DeletePackage(int id);
        public Task<Package?> UpdatePackage(Package package);
        public Task<Package?> GetPackageById(int id);
        public Task<ICollection<Package>?> GetAllPackages();
    }
}
