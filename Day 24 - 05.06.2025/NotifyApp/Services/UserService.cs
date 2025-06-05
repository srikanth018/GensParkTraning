using NotifyApp.Interfaces;
using NotifyApp.Models;
using NotifyApp.Repositories;

namespace NotifyApp.Services
{
    public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository) => _repository = repository;

    public async Task<AppUser> GetOrCreateUserAsync(string email, string name)
{
    var user = await _repository.GetByEmailAsync(email);
    if (user == null)
    {
        user = new AppUser
        {
            Email = email,
            Name = name,
            Role = "User"
        };
        await _repository.AddAsync(user);
    }
    return user;
}

}
}