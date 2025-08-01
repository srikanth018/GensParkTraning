namespace SampleMigrateApp.DTOs
{
    public class CreateNewsDTO
    {
        public int? UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public int? Status { get; set; }
    }
}