using Microsoft.AspNetCore.Http;

namespace Blobapi.Models
{
    public class UploadRequestDto
    {
        public IFormFile File { get; set; }
    }
}