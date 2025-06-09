using QuizApp.DTOs;
using QuizApp.Models;

namespace QuizApp.Interfaces
{
    public interface ITeacherService
    {
        Task<Teacher> CreateTeacherAsync(CreateTeacherRequestDTO teacher);
        Task<Teacher?> GetTeacherByIdAsync(string id);
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher> UpdateTeacherAsync(string id, Teacher teacher);
        Task<Teacher> DeleteTeacherAsync(string id);
        Task<Teacher> GetByEmailAsync(string email);
    }
}