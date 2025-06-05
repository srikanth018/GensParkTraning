using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Repositories;
namespace AppointmentApp.Services
{
    public class DoctorSpecilaityService : IDoctorSepcialityService
    {
        private readonly IRepository<int,DoctorSpeciality> _doctorSpecialityRepository;
        public DoctorSpecilaityService(IRepository<int,DoctorSpeciality> doctorSpecialityRepository)
        {
            _doctorSpecialityRepository = doctorSpecialityRepository;
        }
        public async Task<DoctorSpeciality> Add(DoctorSpeciality doctorSpeciality)
        {
            if (doctorSpeciality == null)
            {
                throw new ArgumentNullException(nameof(doctorSpeciality), "DoctorSpeciality cannot be null");
            }

            return await _doctorSpecialityRepository.Add(doctorSpeciality);
        }

        public Task<DoctorSpeciality> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DoctorSpeciality>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DoctorSpeciality>> GetByDoctorId(int doctorId)
        {

            var doctorSpecialities = await _doctorSpecialityRepository.GetAll();
            return doctorSpecialities.Where(ds => ds.DoctorId == doctorId);
        }

        public Task<DoctorSpeciality> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DoctorSpeciality>> GetBySpecialityId(int specialityId)
        {
            throw new NotImplementedException();
        }

        public Task<DoctorSpeciality> Update(int id, DoctorSpeciality doctorSpeciality)
        {
            throw new NotImplementedException();
        }
    }
}