namespace AppointmentApp.Models
{
    public class Appointment
    {
        public int? Id { get; set; }
        public string? PatientName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? DoctorId { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; } // e.g., "Scheduled", "Completed", "Cancelled"
        public DateTime? CreatedAt { get; set; }
        
    }
}