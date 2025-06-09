using QuizApp.DTOs;
using QuizApp.Interfaces;
using QuizApp.Mappers;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<string, Teacher> _teacherRepository;
        private readonly IRepository<string, User> _userRepository;

        public TeacherService(IRepository<string, Teacher> teacherRepository, IRepository<string, User> userRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
        }
        public async Task<Teacher> CreateTeacherAsync(CreateTeacherRequestDTO teacher)
        {
            var newTeacher = TeacherMappers.CreateTeacher(teacher);
            var newUser = newTeacher.User;
            if (newUser == null)
            {
                throw new InvalidOperationException("User data of the new teacher is null.");
            }
            newUser.Teacher = newTeacher;
            await _userRepository.Add(newUser);
            newTeacher.User = null;
            await _teacherRepository.Add(newTeacher);
            newTeacher.User = newUser;
            return newTeacher;
        }

        public Task<Teacher> DeleteTeacherAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Teacher> GetByEmailAsync(string email)
        {
            var teachers = await _teacherRepository.GetAll();
            var teacher = teachers.FirstOrDefault(t => t.User?.Email == email);
            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with email {email} not found.");
            }
            return teacher;
        }

        public Task<Teacher?> GetTeacherByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Teacher> UpdateTeacherAsync(string id, Teacher teacher)
        {
            throw new NotImplementedException();
        }
    }
}