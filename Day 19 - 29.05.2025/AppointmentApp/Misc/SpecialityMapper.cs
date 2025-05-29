using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Misc
{ 
    public class SpecialityMapper
    {
        public Speciality? MapSpecialityAddRequestDoctor(AddSpecialityRequest addRequestDto)
        {
            Speciality speciality = new();
            speciality.Name = addRequestDto.Name;
            speciality.Status = "Active";
            return speciality;
        }

        public DoctorSpeciality MapDoctorSpecility(int doctorId, int specialityId)
        {
            DoctorSpeciality doctorSpeciality = new();
            doctorSpeciality.DoctorId = doctorId;
            doctorSpeciality.SpecialityId = specialityId;
            return doctorSpeciality;
        }
    }
}