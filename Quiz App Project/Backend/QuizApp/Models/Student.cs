namespace QuizApp.Models
{
    public class Student
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string HighestQualification { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public User? User { get; set; }
        public ICollection<CompletedQuiz>? CompletedQuizzes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreditPoints { get; set; } 
    }
}