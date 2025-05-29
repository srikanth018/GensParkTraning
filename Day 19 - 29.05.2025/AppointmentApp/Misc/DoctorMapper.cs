using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Misc
{
    public class DoctorMapper
    {
        public Doctor? MapDoctorAddRequestDoctor(AddDoctorRequest addRequestDto)
        {
            Doctor doctor = new();
            doctor.Name = addRequestDto.Name;
            doctor.YearsOfExperience = addRequestDto.YearsOfExperience;
            return doctor;
        }
    }
}