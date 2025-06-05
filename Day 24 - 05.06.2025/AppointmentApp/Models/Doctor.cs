
using AppointmentApp.Misc;

namespace AppointmentApp.Models
{
    public class Doctor
    {
        public int? Id { get; set; }
        
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public float YearsOfExperience { get; set; }

        public ICollection<DoctorSpeciality>? DoctorSpecialities { get; set; }
        public ICollection<Appointment>? Appointmnets { get; set; }
        public User? User { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}