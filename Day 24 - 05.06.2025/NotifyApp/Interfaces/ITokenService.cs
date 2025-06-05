using NotifyApp.Models;

namespace NotifyApp.Interfaces
{
    public interface ITokenService
{
    string GenerateToken(AppUser user);
}
}