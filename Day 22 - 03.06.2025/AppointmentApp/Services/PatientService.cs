using AppointmentApp.Interfaces;
using AppointmentApp.Misc;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using AutoMapper;

namespace AppointmentApp.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<string, User> _userRepository;

        public PatientService(IRepository<int, Patient> patientRepository, UserPatientMapper userPatientMapper,
            IMapper mapper,
            IRepository<string, User> userRepository
            )
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Patient> Add(AddPatientRequest patientRequest)
        {

            if (patientRequest == null)
            {
                throw new ArgumentNullException(nameof(patientRequest), "Patient request cannot be null");
            }

            var user = _mapper.Map<AddPatientRequest, User>(patientRequest);
            var encryptModel = new EncryptModel
            {
                Data = patientRequest.Password,
            };
            var hashedPassword = Hashing.HashPassword(encryptModel);
            user.Password = hashedPassword.HashedData;
            user.Role = "Patient";

            user = await _userRepository.Add(user);

            if (user == null)
                throw new Exception("Could not add user");

            var patient = new Patient
            {
                Name = patientRequest.Name,
                PhoneNumber = patientRequest.PhoneNumber,
                Email = patientRequest.Email,
            };

            return await _patientRepository.Add(patient);
        }

        public async Task<Patient?> GetById(int id)
        {
            return await _patientRepository.GetById(id);
        }
    }
}