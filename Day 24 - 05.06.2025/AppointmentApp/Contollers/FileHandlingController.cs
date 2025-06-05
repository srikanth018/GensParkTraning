using AppointmentApp.Interfaces;
using AppointmentApp.Misc;
using AppointmentApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class FileHandlingController : ControllerBase
    {
        private readonly IFileHandlingService _fileHandlingService;

        public FileHandlingController(IFileHandlingService fileHandlingService)
        {
            _fileHandlingService = fileHandlingService;
        }

        [HttpPost("UploadFile")]
        [CustomExceptionFilters]
        public async Task<ActionResult> Upload([FromForm] FileUploadDto file)
        {
            if (file == null || file.File.Length == 0)
                return BadRequest("File is empty.");

            var fileName = await _fileHandlingService.UploadFileAsync(file);
            return Ok(new { FileName = fileName });
        }

        [HttpGet("{fileName}")]
        [CustomExceptionFilters]
        public async Task<ActionResult> GetFile(string fileName)
        {
            var fileBytes = await _fileHandlingService.GetFileAsync(fileName);
            var contentType = FileContentTypes.GetContentTypes(fileName);
            return File(fileBytes, contentType);
        }
    }
}