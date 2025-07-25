using Microsoft.AspNetCore.Mvc;
using VideoStreamApp.Models;
using VideoStreamApp.Repositories;

namespace VideoStreamApp.Services
{
    public class VideoService
    {
        private readonly VideoRepository _videoRepository;
        private readonly BlobStorageService _blobStorageService;

        public VideoService(VideoRepository videoRepository, BlobStorageService blobStorageService)
        {
            _videoRepository = videoRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task<IEnumerable<VideoData>> GetAllVideosAsync()
        {
            return await _videoRepository.GetAllAsync();
        }

        public async Task<VideoData?> GetVideoByIdAsync(Guid id)
        {
            return await _videoRepository.GetByIdAsync(id);
        }

        public async Task UploadVideoAsync([FromForm] UploadVideoDTO videoData)
        {

            if (videoData == null)
            {
                throw new ArgumentNullException(nameof(videoData), "Video data cannot be null.");
            }
            Console.WriteLine("Uploading video: " + videoData.Title);

            var video = new VideoData
            {
                Id = Guid.NewGuid(),
                Title = videoData.Title,
                Description = videoData.Description,
                UploadAt = DateTime.UtcNow,
                BlobUrl = await _blobStorageService.UploadVideoAsync(videoData.VideoFile)
            };
            Console.WriteLine("Video uploaded to Blob Storage: " + video.BlobUrl);
            await _videoRepository.AddAsync(video);
        }
    }
}