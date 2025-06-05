using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Interfaces
{
    public interface IFileHandlingService
    {
       Task<string> UploadFileAsync(FileUploadDto file);
        Task<byte[]> GetFileAsync(string fileName); 
    }
}