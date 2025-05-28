using AppointmentApp.Contexts;
using AppointmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApp.Repositories
{
    public class AppointmentRepository : Repository<int, Appointment>
    {
        public AppointmentRepository(ClinicContext clinicContext) : base(clinicContext)
        {
        }

        public override async Task<IEnumerable<Appointment>>? GetAll()
        {
            var appointments = await _clinicContext.Appointments.ToListAsync();
            if (appointments.Count == 0) throw new Exception("No Appointments in the database");
            return appointments;
        }

        public override async Task<Appointment>? GetById(int key)
        {
            var appointment = await _clinicContext.Appointments.FirstOrDefaultAsync(a => a.Id == key);
            if (appointment == null) throw new Exception("No Appointment found for the provided id");
            return appointment;
        }
    }
}