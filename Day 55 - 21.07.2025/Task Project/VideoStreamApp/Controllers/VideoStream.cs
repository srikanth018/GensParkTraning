using Microsoft.AspNetCore.Mvc;
using VideoStreamApp.Models;
using VideoStreamApp.Services;

namespace VideoStreamApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoStreamController : ControllerBase
    {
        private readonly VideoService _videoService;

        public VideoStreamController(VideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {
            var videos = await _videoService.GetAllVideosAsync();
            return Ok(videos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoById(Guid id)
        {
            var video = await _videoService.GetVideoByIdAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo( UploadVideoDTO videoData)
        {
            System.Console.WriteLine("Received video upload request for: " + videoData.Title);
            if (videoData.VideoFile == null || videoData.VideoFile.Length == 0)
            {
                return BadRequest("Video file is required.");
            }
            await _videoService.UploadVideoAsync(videoData);
            return Ok(new { message = "Video uploaded successfully." });
        }
    }
}