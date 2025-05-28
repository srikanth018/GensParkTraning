using AppointmentApp.Contexts;
using AppointmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApp.Repositories
{ 
    public class DoctorSpecialityRepository : Repository<int, DoctorSpeciality>
    {
        public DoctorSpecialityRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<IEnumerable<DoctorSpeciality>>? GetAll()
        {
            var doctorSpecialities = await _clinicContext.DoctorSpecialities
                // .Include(ds => ds.Doctor)
                // .Include(ds => ds.Speciality)
                .ToListAsync();
            if (doctorSpecialities.Count == 0) throw new Exception("No Doctor Specialities in the database");
            return doctorSpecialities;
        }

        public override async Task<DoctorSpeciality>? GetById(int key)
        {
            var doctorSpeciality = await _clinicContext.DoctorSpecialities
                // .Include(ds => ds.Doctor)
                // .Include(ds => ds.Speciality)
                .FirstOrDefaultAsync(ds => ds.SerialNumber == key);
            if (doctorSpeciality == null) throw new Exception("No Doctor Speciality found for the provided id");
            return doctorSpeciality;
        }
    }
    
}