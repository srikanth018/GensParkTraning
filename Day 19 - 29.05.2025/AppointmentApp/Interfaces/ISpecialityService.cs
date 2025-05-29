using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Interfaces
{ 
    public interface ISpecialityService
    {
        Task<IEnumerable<Speciality>> GetByName(string name);
        Task<IEnumerable<Speciality>> GetByStatus(string status);
        Task<Speciality> GetById(int id);
        Task<IEnumerable<Speciality>> GetAll();
        Task<Speciality> Add(AddSpecialityRequest speciality);
        Task<Speciality> Update(int id, UpdateSpecialityRequest speciality);
        Task<Speciality> Delete(int id);
    }
}