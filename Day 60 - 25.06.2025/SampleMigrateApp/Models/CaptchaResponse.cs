using Newtonsoft.Json;

namespace SampleMigrateApp.Models
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("error-codes")]
        public List<string>? ErrorCodes { get; set; }
    }
}