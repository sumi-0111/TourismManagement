using System.Threading.Tasks;
using Moq;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Services;
using Xunit;

namespace TestPackageTour
{
    public class PackageTesting
    {
        [Fact]
        public async Task AddPackage_ValidPackage_ReturnsPackage()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Package>>();
            var packageService = new PackageServices(mockRepo.Object);
            var package = new Package { PackageId = 1, PackageName = "Test Package" };

            mockRepo.Setup(repo => repo.Add(It.IsAny<Package>())).ReturnsAsync(package);

            // Act
            var result = await packageService.AddPackage(package);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(package.PackageId, result.PackageId);
        }
        [Fact]
        public async Task GetAllPackages_ReturnsListOfPackages()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Package>>();
            var packageService = new PackageServices(mockRepo.Object);

            var packages = new List<Package>
            {
                new Package { PackageId = 1, PackageName = "Package 1" },
                new Package { PackageId = 2, PackageName = "Package 2" },
                new Package { PackageId = 3, PackageName = "Package 3" }
            };

            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(packages);

            // Act
            var result = await packageService.GetAllPackages();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(packages.Count, result.Count);
        }
        [Fact]
        public async Task GetPackageById_ValidId_ReturnsPackage()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Package>>();
            var packageService = new PackageServices(mockRepo.Object);

            var package = new Package { PackageId = 1, PackageName = "Test Package" };

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(package);

            // Act
            var result = await packageService.GetPackageById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(package.PackageId, result.PackageId);
        }

        [Fact]
        public async Task DeletePackage_ExistingId_ReturnsDeletedPackage()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Package>>();
            var packageService = new PackageServices(mockRepo.Object);

            var packageId = 1;
            var package = new Package { PackageId = packageId, PackageName = "Test Package" };

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(package);
            mockRepo.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(package);

            // Act
            var result = await packageService.DeletePackage(packageId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(packageId, result.PackageId);
        }
        [Fact]
        public async Task UpdatePackage_ValidPackage_ReturnsUpdatedPackage()
        {
            // Arrange
            var mockRepo = new Mock<IRepo<int, Package>>();
            var packageService = new PackageServices(mockRepo.Object);

            var packageId = 1;
            var originalPackage = new Package { PackageId = packageId, PackageName = "Test Package" };
            var updatedPackage = new Package { PackageId = packageId, PackageName = "Updated Package" };

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(originalPackage);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Package>())).ReturnsAsync(updatedPackage);

            // Act
            var result = await packageService.UpdatePackage(updatedPackage);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedPackage.PackageName, result.PackageName);
        }

    }
}
