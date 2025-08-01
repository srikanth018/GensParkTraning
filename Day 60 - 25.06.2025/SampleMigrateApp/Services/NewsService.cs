using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;
using SampleMigrateApp.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMigrateApp.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<int, News> _newsRepository;

        public NewsService(IRepository<int, News> newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _newsRepository.GetAll();
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await _newsRepository.Get(id);
        }

        public async Task<News> CreateNewsAsync(CreateNewsDTO news)
        {
            Console.WriteLine(news.Content);
            Console.WriteLine(news.Image);
            Console.WriteLine(news.ShortDescription);
            Console.WriteLine(news.Title);
            
            var newsEntity = new News
            {
                Title = news.Title,
                ShortDescription = news.ShortDescription,
                Image = news.Image,
                Content = news.Content,
                CreatedDate = DateTime.UtcNow,
                Status = 1,
                UserId = news.UserId
            };

            await _newsRepository.Add(newsEntity);
            return newsEntity;
        }

        public async Task<bool> UpdateNewsAsync(int newsId, UpdateNewsDTO news)
        {
            var existing = await _newsRepository.Get(newsId);
            if (existing == null)
                return false;

            existing.Title = news.Title ?? existing.Title;
            existing.ShortDescription = news.ShortDescription ?? existing.ShortDescription;
            existing.Image = news.Image ?? existing.Image;
            existing.Content = news.Content ?? existing.Content;
            existing.Status = news.Status ?? existing.Status;
            existing.UserId = news.UserId ?? existing.UserId;

            await _newsRepository.Update(existing.NewsId, existing);
            return true;
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            var news = await _newsRepository.Get(id);
            if (news == null)
                return false;

            await _newsRepository.Delete(id);
            return true;
        }

        public async Task<byte[]> ExportNewsAsync()
        {
            var newsList = await _newsRepository.GetAll();
            var sb = new StringBuilder();
            sb.AppendLine("NewsId,Title,ShortDescription,CreatedDate,Status");

            foreach (var news in newsList)
            {
                sb.AppendLine($"{news.NewsId},{news.Title},{news.ShortDescription},{news.CreatedDate},{news.Status}");
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
