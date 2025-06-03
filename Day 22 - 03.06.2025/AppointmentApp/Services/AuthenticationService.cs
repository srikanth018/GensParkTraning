using AppointmentApp.Interfaces;
using AppointmentApp.Misc;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IRepository<string, User> _userRepository;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IDoctorService _doctorService;
        private readonly IRepository<string, Doctor> _doctorRepository;

        public AuthenticationService(ITokenService tokenService,
                                    IRepository<string, User> userRepository,
                                    ILogger<AuthenticationService> logger,
                                    IDoctorService doctorService)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _logger = logger;
            _doctorService = doctorService;
        }
        public async Task<UserLoginResponse> Login(UserLoginRequest user)
        {
            var dbUser = await _userRepository.GetById(user.Username);
            if (dbUser == null)
            {
                _logger.LogCritical("User not found");
                throw new Exception("No such user");
            }
            var encryptedData = Hashing.VerifyPassword(new EncryptModel
            {
                Data = user.Password,
                HashedData = dbUser.Password,
            });

            if (!encryptedData)
            {
                _logger.LogError("Invalid login attempt");
                throw new Exception("Invalid password");
            }
            if(dbUser.Role == "Doctor")
            {
                var doctor = await _doctorService.GetDoctorByEmail(user.Username);
                if (doctor == null)
                {
                    _logger.LogError("Doctor details not found for user {Username}", user.Username);
                    throw new Exception("Doctor details not found");
                }
                dbUser.Doctor = doctor;
            }
            
            var token = await _tokenService.GenerateToken(dbUser);
            return new UserLoginResponse
            {
                Username = user.Username,
                Token = token,
            };
        }
    }
}