using SampleMigrateApp.DTOs;
using SampleMigrateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMigrateApp.Services
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News?> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(CreateNewsDTO news);
        Task<bool> UpdateNewsAsync(int newsId,UpdateNewsDTO news);
        Task<bool> DeleteNewsAsync(int id);
        Task<byte[]> ExportNewsAsync();
    }
}
