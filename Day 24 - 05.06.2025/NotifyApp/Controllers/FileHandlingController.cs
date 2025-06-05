using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotifyApp.Hubs;
using NotifyApp.Interfaces;
using NotifyApp.Models;

namespace NotifyApp.Controllers
{
    [ApiController]
    [Route("file")]
    public class FileHandlingController : ControllerBase
    {
        private readonly IFileHandlingService _fileHandlingService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FileHandlingController(IFileHandlingService fileHandlingService, IHubContext<NotificationHub> hubContext)
        {
            _fileHandlingService = fileHandlingService;
            _hubContext = hubContext;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto file)
        {
            if (file == null || file.Content == null || file.Content.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }



            var fileName = await _fileHandlingService.UploadFileAsync(file);

        Console.WriteLine($"[Notify] Sending file notification: {fileName}");

                await _hubContext.Clients.All.SendAsync("FileUpload", fileName, "New file has added. Everyone can download it now.");
            
            return Ok(new { FileName = fileName });
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> ReadFile(string fileName)
        {
            var fileBytes = await _fileHandlingService.GetFileAsync(fileName);
            if (fileBytes == null)
            {
                return NotFound("File not found.");
            }

            return File(fileBytes, "text/plain", fileName);
        }
    }
}