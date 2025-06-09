namespace QuizApp.Models
{
    public class CompletedQuiz
    {
        public string Id { get; set; } = string.Empty;
        public int TotalScore { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public Student? Student { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    }
}