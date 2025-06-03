namespace AppointmentApp.Models
{
    public class Patient
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }

        public string? Gender { get; set; }

        public string? PhoneNumber { get; set; }

        public string? BloodType { get; set; }
        public string? Status { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
        public User? User { get; set; }
        
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}
