using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<UserLoginResponse> Login(UserLoginRequest user);
    }
}