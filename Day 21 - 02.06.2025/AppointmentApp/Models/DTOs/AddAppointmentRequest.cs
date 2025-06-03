namespace AppointmentApp.Models.DTOs
{ 
    public class AddAppointmentRequest
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? Status { get; set; } = "Pending";
    }
}