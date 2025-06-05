using AppointmentApp.Interfaces;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Services
{
    public class FileHandlingService : IFileHandlingService
    {
        private readonly string _folderPath;

        public FileHandlingService()
        {
            _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
        }
        public async Task<string> UploadFileAsync(FileUploadDto file)
        {
            var filePath = Path.Combine(_folderPath, file.File.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.File.CopyToAsync(stream);
            }
            return $"{file.File.FileName} -> File Uploaded Sucessfully";
        }

        public async Task<byte[]> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine(_folderPath, fileName);

            return await File.ReadAllBytesAsync(filePath);
        }
    }
}