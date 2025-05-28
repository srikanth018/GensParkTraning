using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetByName(string name);
        Task<IEnumerable<Doctor>> GetByStatus(string status);
        Task<IEnumerable<Doctor>> GetBySpeciality(int specialityId);
        Task<Doctor> GetById(int id);
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> Add(AddDoctorRequest doctor);
        Task<Doctor> Update(int id, UpdateDoctorRequest doctor);
        Task<Doctor> Delete(int id);
    }
}