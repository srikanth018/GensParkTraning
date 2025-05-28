using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using AppointmentApp.Repositories;

namespace AppointmentApp.Services
{
    public class SpecialityService : ISpecialityService
    {
        private IRepository<int,Speciality> _specialityRepository;
        public SpecialityService(IRepository<int,Speciality> specialityRepository)
        {
            _specialityRepository = specialityRepository ;
        }
        public Task<Speciality> Add(AddSpecialityRequest speciality)
        {
            throw new NotImplementedException();
        }

        public Task<Speciality> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Speciality>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Speciality> GetById(int id)
        {
            return  await _specialityRepository.GetById(id);
        }

        public Task<IEnumerable<Speciality>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Speciality>> GetByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Task<Speciality> Update(int id, UpdateSpecialityRequest speciality)
        {
            throw new NotImplementedException();
        }
    }
}