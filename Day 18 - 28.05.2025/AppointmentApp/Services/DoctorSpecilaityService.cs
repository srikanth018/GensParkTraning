using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Repositories;
namespace AppointmentApp.Services
{
    public class DoctorSpecilaityService : IDoctorSepcialityService
    {
        private readonly DoctorSpecialityRepository _doctorSpecialityRepository;
        public DoctorSpecilaityService(DoctorSpecialityRepository doctorSpecialityRepository)
        {
            _doctorSpecialityRepository = doctorSpecialityRepository;
        }
        public Task<DoctorSpeciality> Add(DoctorSpeciality doctorSpeciality)
        {
            if (doctorSpeciality == null)
            {
                throw new ArgumentNullException(nameof(doctorSpeciality), "DoctorSpeciality cannot be null");
            }

            return _doctorSpecialityRepository.Add(doctorSpeciality);
        }

        public Task<DoctorSpeciality> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DoctorSpeciality>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DoctorSpeciality>> GetByDoctorId(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DoctorSpeciality>> GetByDoctorIdAndSpecialityId(int doctorId, int specialityId)
        {
            throw new NotImplementedException();
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