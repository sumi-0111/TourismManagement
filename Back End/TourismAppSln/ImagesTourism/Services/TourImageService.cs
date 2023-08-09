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
            string connectionString = "BlobEndpoint=http://127.0.0.1:8888/devstoreaccount1;SharedAccessSignature=?sv=2021-10-04&ss=btqf&srt=sco&st=2023-08-09T04%3A57%3A19Z&se=2023-08-10T04%3A57%3A19Z&sp=rwdxftlacup&sig=45N8R%2F7b46dah21ZVOqhWK%2FUHdCNCTRQb%2B0RLciWrNs%3D"; BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

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

