using QuizApp.DTOs;
using QuizApp.Models;

namespace QuizApp.Interfaces
{ 
    public interface IStudentService
    {
        Task<Student> CreateStudentAsync(CreateStudentRequestDTO student);
        Task<Student?> GetStudentByIdAsync(string id);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> UpdateStudentAsync(string id, Student student);
        Task<Student> DeleteStudentAsync(string id);
        Task<Student> GetByEmailAsync(string email);
    }
}