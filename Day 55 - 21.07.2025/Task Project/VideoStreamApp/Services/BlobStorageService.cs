using Azure.Storage.Blobs;

namespace VideoStreamApp.Services
{
    public class BlobStorageService
    {
        private readonly string? _connectionString;
        private readonly string? _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            var blobStorageConfig = configuration.GetSection("AzureBlobStorage");
            _connectionString = blobStorageConfig["ConnectionString"];
            _containerName = blobStorageConfig["ContainerName"];
        }
        public async Task<string> UploadVideoAsync(IFormFile videoFile)
        {
            if (string.IsNullOrEmpty(_connectionString) || string.IsNullOrEmpty(_containerName))
            {
                throw new InvalidOperationException("Blob storage configuration is not set.");
            }

            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobName = $"{Guid.NewGuid()}_{videoFile.FileName}";
            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = videoFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }

    }
}