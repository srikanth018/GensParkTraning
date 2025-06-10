using QuizApp.DTOs;
using QuizApp.Models;

namespace QuizApp.Interfaces
{
    public interface IQuizService
    {
        Task<Quiz> CreateQuizAsync(CreateQuizRequestDTO quiz);
        Task<Quiz?> GetQuizByIdAsync(string id);
        Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> DeleteQuizAsync(string id);
        Task<IEnumerable<Quiz>> GetQuizzesByTeacherEmailAsync(string email);
    }
}