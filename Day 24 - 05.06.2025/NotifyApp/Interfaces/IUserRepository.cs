using NotifyApp.Models;

namespace NotifyApp.Interfaces;

public interface IUserRepository
{
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser> AddAsync(AppUser user);
}
