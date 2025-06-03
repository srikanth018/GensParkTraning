using AppointmentApp.Models;

namespace AppointmentApp.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}