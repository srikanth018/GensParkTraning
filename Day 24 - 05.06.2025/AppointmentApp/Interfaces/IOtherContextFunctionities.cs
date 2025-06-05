using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Interfaces
{ 
    public interface IOtherContextFunctionities
    {
        public Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string specilaity);
    }
}