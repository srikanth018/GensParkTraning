using NotifyApp.Models;

namespace NotifyApp.Interfaces
{

    public interface IUserService
    {
        Task<AppUser> GetOrCreateUserAsync(string email, string name);
    }

}