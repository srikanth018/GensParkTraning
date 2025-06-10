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

        public async Task<Teacher> DeleteTeacherAsync(string id)
        {
            var deleteTeacher = await _teacherRepository.GetById(id);
            if (deleteTeacher == null)
            {
                throw new KeyNotFoundException($"Teacher not found with the provided id - {id} for delete");
            }
            return await _teacherRepository.Delete(deleteTeacher);
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAll();
        }

        public async Task<Teacher> GetByEmailAsync(string email)
        {
            var teachers = await _teacherRepository.GetAll();
            var teacher = teachers.FirstOrDefault(t => t.Email == email);
            if (teacher == null)
            {
                throw new KeyNotFoundException($"Teacher with email {email} not found.");
            }
            return teacher;
        }

        public async Task<Teacher?> GetTeacherByIdAsync(string id)
        {
            return await _teacherRepository.GetById(id);
        }

        public Task<Teacher> UpdateTeacherAsync(string id, Teacher teacher)
        {
            var existingTeacher = _teacherRepository.GetById(id);
            if (existingTeacher == null)
            {
                throw new KeyNotFoundException($"Teacher not found with the provided id - {id} for update");
            }
            return _teacherRepository.Update(id, teacher);
        }
    }
}