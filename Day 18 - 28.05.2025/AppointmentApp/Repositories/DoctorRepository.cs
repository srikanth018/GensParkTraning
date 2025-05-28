using AppointmentApp.Contexts;
using AppointmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApp.Repositories
{
    public class DoctorRepository : Repository<int, Doctor>
    {
        public DoctorRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<IEnumerable<Doctor>>? GetAll()
        {
            var doctors = await _clinicContext.Doctors.ToListAsync();
            if (doctors.Count == 0) throw new Exception("No Doctors in the database");
            return doctors;
        }

        public override async Task<Doctor> GetById(int key)
        {
            var doctor = await _clinicContext.Doctors.FirstOrDefaultAsync(d => d.Id == key);
            if (doctor == null) throw new Exception("No Doctor found for the provided id");
            return doctor;
        }
    }
}