using System.Security.Claims;
using Asp.Versioning;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.DTOs;
using QuizApp.Interfaces;
using QuizApp.Misc;

namespace QuizApp.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/quizzes")]
    [ApiVersion("1.0")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly ITeacherService _teacherService;
        private readonly IQuizTemplateService _quizTemplateService;

        public QuizController(IQuizService quizService, ITeacherService teacherService,
                              IQuizTemplateService quizTemplateService)
        {
            _quizService = quizService;
            _teacherService = teacherService;
            _quizTemplateService = quizTemplateService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequestDTO quiz)
        {
            var emailClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (emailClaim == null)
            {
                return Unauthorized("User email claim not found.");
            }
            quiz.UploadedBy = emailClaim.Value;
            var createdQuiz = await _quizService.CreateQuizAsync(quiz);
            return Ok(createdQuiz);
        }

        [HttpGet("template")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Teacher")]
        public IActionResult DownloadQuizTemplate([FromQuery] int questionCount = 5, [FromQuery] int optionCount = 4)
        {
            var file = _quizTemplateService.GenerateQuizTemplate(questionCount, optionCount);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "QuizTemplate.xlsx");
        }
        [HttpPost("bulk-upload")]
        [MapToApiVersion("1.0")]
        // [Authorize(Roles = "Teacher")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> BulkUploadQuiz([FromForm] FileUploadDTO file)
        {
            if (file == null || file.File.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = file.File.OpenReadStream();
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();

            var quizDto = Generators.ParseQuizFromWorksheet(worksheet);

            var createdQuiz = await _quizService.CreateQuizAsync(quizDto);

            return Ok(new { createdQuiz.Id, createdQuiz.Title });
        }

    }
}