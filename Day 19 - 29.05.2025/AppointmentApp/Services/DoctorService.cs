using AppointmentApp.Interfaces;
using AppointmentApp.Misc;
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
        private readonly DoctorMapper doctorMapper;
        private readonly SpecialityMapper specialityMapper;
        private readonly IOtherContextFunctionities _otherContextFunctionities;

        public DoctorService(IRepository<int, Doctor> doctorRepository,
            IRepository<int, Speciality> specialityRepository,
            IRepository<int, DoctorSpeciality> doctorSpecialityRepository,
            IOtherContextFunctionities otherContextFunctionities)
        {
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
            _doctorSpecialityRepository = doctorSpecialityRepository;
            doctorMapper = new DoctorMapper();
            specialityMapper = new SpecialityMapper();
            _otherContextFunctionities = otherContextFunctionities;
        }
        // public async Task<Doctor> Add(AddDoctorRequest doctor)
        // {
        //     if (doctor == null)
        //         throw new ArgumentNullException(nameof(doctor));

        //     var newDoctor = new Doctor
        //     {
        //         Name = doctor.Name,
        //         Status = "Active",
        //         PhoneNumber = doctor.PhoneNumber,
        //         YearsOfExperience = doctor.YearsOfExperience
        //     };

        //     var addedDoctor = await _doctorRepository.Add(newDoctor);

        //     if (addedDoctor == null || addedDoctor.Id == null)
        //         throw new Exception("Failed to add doctor.");

        //     foreach (var specialityId in doctor.SpecialityIds)
        //     {
        //         var speciality = await _specialityRepository.GetById(specialityId);
        //         if (speciality == null)
        //             throw new Exception($"Speciality with ID {specialityId} does not exist.");

        //         var doctorSpeciality = new DoctorSpeciality
        //         {
        //             DoctorId = addedDoctor.Id.Value,
        //             SpecialityId = specialityId
        //         };

        //         await _doctorSpecialityRepository.Add(doctorSpeciality);
        //     }

        //     return addedDoctor;
        // }

public async Task<Doctor> Add(AddDoctorRequest doctor)
        {
            try
            {
                var newDoctor = doctorMapper.MapDoctorAddRequestDoctor(doctor);
                newDoctor = await _doctorRepository.Add(newDoctor);
                if (newDoctor == null)
                    throw new Exception("Could not add doctor");
                if (doctor.Specialities.Count() > 0)
                {
                    int[] specialities = await MapAndAddSpeciality(doctor);
                    for (int i = 0; i < specialities.Length; i++)
                    {
                        var doctorSpeciality = specialityMapper.MapDoctorSpecility((int)newDoctor.Id, specialities[i]);
                        doctorSpeciality = await _doctorSpecialityRepository.Add(doctorSpeciality);
                    }
                }
                return newDoctor;
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        private async Task<int[]> MapAndAddSpeciality(AddDoctorRequest doctor)
        {
            int[] specialityIds = new int[doctor.Specialities.Count()];
            IEnumerable<Speciality> existingSpecialities = null;
            try
            {
                existingSpecialities = await _specialityRepository.GetAll();
            }
            catch (Exception e)
            {

            }
            int count = 0;
            foreach (var item in doctor.Specialities)
            {
                Speciality speciality = null;
                if (existingSpecialities != null)
                    speciality = existingSpecialities.FirstOrDefault(s => s.Name.ToLower() == item.Name.ToLower());
                if (speciality == null)
                {
                    speciality = specialityMapper.MapSpecialityAddRequestDoctor(item);
                    speciality = await _specialityRepository.Add(speciality);
                }
                specialityIds[count] = speciality.Id;
                count++;
            }
            return specialityIds;
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

        public async Task<IEnumerable<DoctorsBySpecialityResponseDto>> GetBySpeciality(string speciality)
        {
            if (string.IsNullOrWhiteSpace(speciality))
                throw new ArgumentException("Speciality cannot be null or empty.", nameof(speciality));

            return await _otherContextFunctionities.GetDoctorsBySpeciality(speciality);
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