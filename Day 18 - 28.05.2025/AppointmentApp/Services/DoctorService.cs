using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using AppointmentApp.Repositories;

namespace AppointmentApp.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<int, Doctor> _doctorRepository;
        private readonly IRepository<int, Speciality> _specialityRepository;
        private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;

        public DoctorService(IRepository<int, Doctor> doctorRepository,
            IRepository<int, Speciality> specialityRepository,
            IRepository<int, DoctorSpeciality> doctorSpecialityRepository)
        {
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
            _doctorSpecialityRepository = doctorSpecialityRepository;

        }
        public async Task<Doctor> Add(AddDoctorRequest doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            var newDoctor = new Doctor
            {
                Name = doctor.Name,
                Status = "Active",
                PhoneNumber = doctor.PhoneNumber,
                YearsOfExperience = doctor.YearsOfExperience
            };

            var addedDoctor = await _doctorRepository.Add(newDoctor);

            if (addedDoctor == null || addedDoctor.Id == null)
                throw new Exception("Failed to add doctor.");

            foreach (var specialityId in doctor.SpecialityIds)
            {
                var speciality = await _specialityRepository.GetById(specialityId);
                if (speciality == null)
                    throw new Exception($"Speciality with ID {specialityId} does not exist.");

                var doctorSpeciality = new DoctorSpeciality
                {
                    DoctorId = addedDoctor.Id.Value,
                    SpecialityId = specialityId
                };

                await _doctorSpecialityRepository.Add(doctorSpeciality);
            }

            return addedDoctor;
        }

        public async Task<Doctor> Delete(int id)
        {

            return await _doctorRepository.Delete(id);
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _doctorRepository.GetAll();
        }

        public async Task<Doctor> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid doctor ID.", nameof(id));

            return await _doctorRepository.GetById(id);
        }

        public async Task<IEnumerable<Doctor>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Doctor name cannot be null or empty.", nameof(name));

            var doctors = await _doctorRepository.GetAll();
            return doctors.Where(d => d.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public Task<IEnumerable<Doctor>> GetBySpeciality(int specialityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Doctor>> GetByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> Update(int id, UpdateDoctorRequest doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            if (id <= 0)
                throw new ArgumentException("Invalid doctor ID.", nameof(id));

            var existingDoctor = await _doctorRepository.GetById(id);
            if (existingDoctor == null)
                throw new Exception($"Doctor with ID {id} does not exist.");

            existingDoctor.Name = doctor.Name ?? existingDoctor.Name;
            existingDoctor.PhoneNumber = doctor.PhoneNumber ?? existingDoctor.PhoneNumber;
            existingDoctor.YearsOfExperience = doctor.YearsOfExperience >= 0 ? doctor.YearsOfExperience : existingDoctor.YearsOfExperience;

            return await _doctorRepository.Update(id, existingDoctor);
        }


    }
}