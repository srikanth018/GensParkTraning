namespace VideoStreamApp.Models
{
    public class UploadVideoDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile VideoFile { get; set; } = null!;

    }
}