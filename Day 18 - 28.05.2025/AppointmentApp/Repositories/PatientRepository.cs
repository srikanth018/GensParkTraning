using AppointmentApp.Contexts;
using AppointmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApp.Repositories
{
    public class PatientRepository : Repository<int, Patient>
    {
        public PatientRepository(ClinicContext clinicContext) : base(clinicContext)
        {

        }
        public override async Task<IEnumerable<Patient>>? GetAll()
        {
            var patients = await _clinicContext.Patients.ToListAsync();
            if (patients.Count == 0) throw new Exception("No Patients in the database");
            return patients;
        }
        public override async Task<Patient> GetById(int key)
        {
            var patient = await _clinicContext.Patients.FirstOrDefaultAsync(p => p.Id == key);
            if(patient == null) throw new Exception("No Patients for the provide id");
            return patient;
        }
    }
}