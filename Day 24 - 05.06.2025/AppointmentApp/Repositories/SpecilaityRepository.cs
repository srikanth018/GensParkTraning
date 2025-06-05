using AppointmentApp.Contexts;
using AppointmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApp.Repositories
{ 
    public class SpecialityRepository : Repository<int, Speciality>
    {
        public SpecialityRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<IEnumerable<Speciality>> GetAll()
        {
            var specialities = await _clinicContext.Specialities.ToListAsync();
            if (specialities.Count == 0) throw new Exception("No Specialities in the database");
            return specialities;
        }

        public override async Task<Speciality> GetById(int key)
        {
            var speciality = await _clinicContext.Specialities.FirstOrDefaultAsync(s => s.Id == key);
            if (speciality == null) throw new Exception("No Speciality found for the provided id");
            return speciality;
        }
    }
}