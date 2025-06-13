using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Interfaces
{
    public interface IFileProcessingService
    {
        public Task<FileUploadReturnDTO> ProcessData(CsvUploadDto csvUploadDto);
    }
}