using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DTOs;
using QuizApp.Interfaces;
using QuizApp.Misc;

namespace QuizApp.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/teachers")]
    [ApiVersion("1.0")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [CustomException]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherRequestDTO teacher)
        {
            var createdTeacher = await _teacherService.CreateTeacherAsync(teacher);
            return Ok(createdTeacher);
        }

        
    }
}