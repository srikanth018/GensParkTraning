using QuizApp.DTOs;
using QuizApp.Interfaces;
using QuizApp.Mappers;
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
        public async Task<Student> CreateStudentAsync(CreateStudentRequestDTO student)
        {
            var newStudent = StudentMappers.CreateStudentMapper(student);
            var newUser = newStudent.User;
            if (newUser == null)
            {
                throw new InvalidOperationException("User data of the new teacher is null.");
            }
            await _userRepository.Add(newUser);
            newStudent.User = null;
            await _studentRepositry.Add(newStudent);
            newStudent.User = newUser;
            return newStudent;
        }

        public async Task<Student> DeleteStudentAsync(string id)
        {
            var deleteStudent = await _studentRepositry.GetById(id);
            if (deleteStudent == null)
            {
                throw new KeyNotFoundException($"student not found with the provided id - {id} for delete");
            }
            return await _studentRepositry.Delete(deleteStudent);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepositry.GetAll();
        }

        public async Task<Student> GetByEmailAsync(string email)
        {
            var students = await _studentRepositry.GetAll();
            var student = students.FirstOrDefault(s => s.Email == email);
            if (student == null) throw new InvalidOperationException($"Student with this {email} is not available");
            return student;
        }

        public async Task<Student?> GetStudentByIdAsync(string id)
        {
            return await _studentRepositry.GetById(id);
        }

        public async Task<Student> UpdateStudentAsync(string id, StudentUpdateRequestDto studentDto)
        {
            var existingstudent = await _studentRepositry.GetById(id);
            if (existingstudent == null)
            {
                throw new KeyNotFoundException($"student not found with the provided id - {id} for update");
            }
            var student = StudentMappers.StudentUpdateMApper(studentDto);
            var updateStudent = await _studentRepositry.Update(id, student);
            return updateStudent;
        }
    }
}