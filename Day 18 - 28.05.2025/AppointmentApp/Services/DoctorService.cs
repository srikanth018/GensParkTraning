using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using AppointmentApp.Repositories;

namespace AppointmentApp.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly SpecialityRepository _specialityRepository;
        private readonly DoctorSpecialityRepository _doctorSpecialityRepository;

        public DoctorService(DoctorRepository doctorRepository,
            SpecialityRepository specialityRepository,
            DoctorSpecialityRepository doctorSpecialityRepository)
        {
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
            _doctorSpecialityRepository = doctorSpecialityRepository;

        }
        public Task<Doctor> Add(AddDoctorRequest doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor), "Doctor cannot be null");
            }

            var newDoctor = new Doctor
            {
                Name = doctor.Name,
                Status = "Active",
                PhoneNumber = doctor.PhoneNumber,
                YearsOfExperience = doctor.YearsOfExperience,
            };

            var addedDoctor = _doctorRepository.Add(newDoctor);

            if (addedDoctor == null)
            {
                throw new Exception("Failed to add doctor.");
            }

            foreach (var specialityId in doctor.SpecialityIds)
            {
                var speciality = _specialityRepository.GetById(specialityId);
                if (speciality == null)
                {
                    throw new Exception($"Speciality with ID {specialityId} does not exist.");
                }

                _doctorSpecialityRepository.Add(new DoctorSpeciality
                {
                    DoctorId = addedDoctor.Id,
                    SpecialityId = specialityId
                }).Wait();
            }

            return addedDoctor;
        }

        public Task<Doctor> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetBySpeciality(int specialityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> Update(int id, UpdateDoctorRequest doctor)
        {
            throw new NotImplementedException();
        }

        
    }
}