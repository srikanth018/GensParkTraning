using AppointmentApp.Contexts;
using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApp.Repositories
{
    public class UserRepository : Repository<string, User>
    {
        public UserRepository(ClinicContext clinicContext) : base(clinicContext)
        {
            
        }
        public override async Task<IEnumerable<User>>? GetAll()
        {
            var users = await _clinicContext.Users.ToListAsync();
            if (users.Count == 0) throw new Exception("No Users in the database");
            return users;
        }

        public override Task<User> GetById(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Username cannot be null or empty.", nameof(key));

            var user = _clinicContext.Users.FirstOrDefault(u => u.Username == key);
            if (user == null)
                throw new Exception("No User found for the provided username");

            return Task.FromResult(user);
        }
    }
}