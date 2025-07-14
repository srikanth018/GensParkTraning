using Azure.Storage.Blobs;

namespace BlobStorage.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClinet;
        private readonly KeyVaultService _keyVaultService;
        public BlobStorageService(IConfiguration configuration, KeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService ?? throw new ArgumentNullException(nameof(keyVaultService));
            var sasUrl = GetStorageAccountConnectionString().GetAwaiter().GetResult();
            _containerClinet = new BlobContainerClient(new Uri(sasUrl));
        }

        private async Task<string> GetStorageAccountConnectionString()
        {
            return await _keyVaultService.GetSecretAsync("file-SaSURL");
        }

        public async Task UploadFile(Stream fileStream, string fileName)
        {
            var blobClient = _containerClinet.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            var blobClient = _containerClinet?.GetBlobClient(fileName);
            if (await blobClient.ExistsAsync())
            {
                var downloadInfor = await blobClient.DownloadStreamingAsync();
                return downloadInfor.Value.Content;
            }
            return null;
        }
    }
}