namespace NotifyApp.Models
{
    public class FileUploadDto
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public IFormFile Content { get; set; } = null!;
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }
}