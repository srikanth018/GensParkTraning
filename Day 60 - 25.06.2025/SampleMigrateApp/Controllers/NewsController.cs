using Microsoft.AspNetCore.Mvc;
using SampleMigrateApp.DTOs;
using SampleMigrateApp.Models;
using SampleMigrateApp.Services;

namespace SampleMigrateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetAll()
        {
            var newsList = await _newsService.GetAllNewsAsync();
            return Ok(newsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetById(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
                return NotFound(new { message = $"News with ID {id} not found." });
            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<News>> Create([FromBody] CreateNewsDTO news)
        {
            var created = await _newsService.CreateNewsAsync(news);
            return CreatedAtAction(nameof(GetById), new { id = created.NewsId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateNewsDTO news)
        {
            var updated = await _newsService.UpdateNewsAsync(id, news);
            if (!updated)
                return NotFound(new { message = $"News with ID {id} not found." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _newsService.DeleteNewsAsync(id);
            if (!deleted)
                return NotFound(new { message = $"News with ID {id} not found." });

            return NoContent();
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var content = await _newsService.ExportNewsAsync();
            var fileName = $"NewsExport_{DateTime.Now:yyyyMMddHHmmss}.csv";

            return File(content, "text/csv", fileName);
        }
    }
}
