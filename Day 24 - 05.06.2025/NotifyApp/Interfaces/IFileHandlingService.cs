using NotifyApp.Models;

namespace NotifyApp.Interfaces
{
    public interface IFileHandlingService
    {
       Task<string> UploadFileAsync(FileUploadDto file);
       Task<byte[]> GetFileAsync(string fileName); 
    }
}