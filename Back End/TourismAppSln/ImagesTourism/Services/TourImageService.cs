using Azure.Storage.Blobs;
using ImagesTourism.Interfaces;
using ImagesTourism.Models;

namespace ImagesTourism.Services
{
    public class TourImageService : ITourImageServices
    {
        private readonly IRepo<int, ImageTourism> _tourImageRepo;

        public TourImageService(IRepo<int, ImageTourism> tourImageRepo)
        {
            _tourImageRepo = tourImageRepo;
        }

        public async Task<ImageTourism> AddTourImage(int packageId, IFormFile image, string name)
        {
            // Connect to Azurite Blob Storage
            string connectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:8888/devstoreaccount1;";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("samples-workitems");

            // Create the container if it doesn't exist
            containerClient.CreateIfNotExists();

            // Generate a unique blob name
            string uniqueBlobName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);

            // Upload the image to Azure Blob Storage
            BlobClient blobClient = containerClient.GetBlobClient(uniqueBlobName);
            using (var stream = image.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            // Add the current image information to the database
            ImageTourism tourImage = new ImageTourism
            {
                Name = name,
                ImagePath = blobClient.Uri.ToString(),
                PackageId = packageId
            };

            return await _tourImageRepo.Add(tourImage);
        }

        public async Task<ICollection<ImageTourism>> GetAllTourImage()
        {
            return await _tourImageRepo.GetAll();
        }

        public async Task<ImageTourism> GetTourImage(int id)
        {
            return await _tourImageRepo.Get(id);
        }
    }
}

