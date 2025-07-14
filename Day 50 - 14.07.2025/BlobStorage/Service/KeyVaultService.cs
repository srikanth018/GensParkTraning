

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BlobStorage.Services
{
    public class KeyVaultService
    {
        private readonly IConfiguration _configuration;
        private string _keyVaultUrl;
        public KeyVaultService(IConfiguration configuration)
        {
            _configuration = configuration;
            _keyVaultUrl = configuration["AzureBlob:KeyVaultUrl"] ?? string.Empty;
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            var client = new SecretClient(new Uri(_keyVaultUrl), new DefaultAzureCredential());
            KeyVaultSecret secret = await client.GetSecretAsync(secretName);
            return secret.Value;
        }

        
    }
}