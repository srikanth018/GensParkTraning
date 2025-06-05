using NotifyApp.Interfaces;
using NotifyApp.Misc;
using NotifyApp.Models;

namespace NotifyApp.Services
{
    public class FileHandlingService : IFileHandlingService
    {
        private readonly string _storagePath;
        public FileHandlingService(IConfiguration configuration)
        {
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }
        public async Task<string> UploadFileAsync(FileUploadDto file)
        {
            var filePath = Path.Combine(_storagePath, file.FileName);

            Directory.CreateDirectory(_storagePath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.Content.CopyToAsync(stream);
            }

            file.ContentType = FileHelper.GetContentTypes(file.FileName);

            return file.FileName;
        }

        public async Task<byte[]> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine(_storagePath, fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", fileName);
            }
            return await File.ReadAllBytesAsync(filePath);
        }

    }
}