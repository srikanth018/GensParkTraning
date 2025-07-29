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

        public async Task<News> CreateNewsAsync(News news)
        {
            await _newsRepository.Add(news);
            return news;
        }

        public async Task<bool> UpdateNewsAsync(News news)
        {
            var existing = await _newsRepository.Get(news.NewsId);
            if (existing == null)
                return false;

            await _newsRepository.Update(existing.NewsId, news);
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
