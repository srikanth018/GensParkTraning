using QuizApp.DTOs;
using QuizApp.Misc;
using QuizApp.Models;

namespace QuizApp.Mappers
{
    public static class TeacherMappers
    {
        public static Teacher CreateTeacher(CreateTeacherRequestDTO teacherRequest)
        {
            var teacher = new Teacher
            {
                Id = Generators.GenerateID("TE"),
                Name = teacherRequest.Name,
                Email = teacherRequest.Email,
                PhoneNumber = teacherRequest.PhoneNumber,
                Status = "Active",
                User = new User
                {
                    Email = teacherRequest.Email,
                    Password = Generators.GenerateHashedPassword(teacherRequest.Password),
                    Role = "Teacher"
                }
            };
            return teacher;
        }
    }
}