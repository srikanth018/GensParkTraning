using AppointmentApp.Models;

namespace AppointmentApp.Interfaces
{
    public interface IDoctorSepcialityService
    {
        Task<IEnumerable<DoctorSpeciality>> GetByDoctorId(int doctorId);
        Task<IEnumerable<DoctorSpeciality>> GetBySpecialityId(int specialityId);
        Task<DoctorSpeciality> GetById(int id);
        Task<IEnumerable<DoctorSpeciality>> GetAll();
        Task<DoctorSpeciality> Add(DoctorSpeciality doctorSpeciality);
        Task<DoctorSpeciality> Update(int id, DoctorSpeciality doctorSpeciality);
        Task<DoctorSpeciality> Delete(int id);
        
    }
}