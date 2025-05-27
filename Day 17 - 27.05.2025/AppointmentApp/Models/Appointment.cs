using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentApp.Models
{
    public class Appointment
    {
        public int? Id { get; set; }
        public int? PatientId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? DoctorId { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; } // e.g., "Scheduled", "Completed", "Cancelled"

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }
    }
}