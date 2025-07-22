namespace VideoStreamApp.Models
{
    public class VideoData
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BlobUrl { get; set; } = string.Empty;
        public DateTime UploadAt { get; set; }
    }
}