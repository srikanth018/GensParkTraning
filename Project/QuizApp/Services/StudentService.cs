using QuizApp.DTOs;
using QuizApp.Interfaces;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<string, Student> _studentRepositry;

        public StudentService(IRepository<string, User> userRepository, IRepository<string, Student> studentRepositry)
        {
            _userRepository = userRepository;
            _studentRepositry = studentRepositry;
        }
        public Task<Student> CreateStudentAsync(CreateStudentRequestDTO student)
        {
            throw new NotImplementedException();
        }

        public Task<Student> DeleteStudentAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Student?> GetStudentByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> UpdateStudentAsync(string id, Student student)
        {
            throw new NotImplementedException();
        }
    }
}