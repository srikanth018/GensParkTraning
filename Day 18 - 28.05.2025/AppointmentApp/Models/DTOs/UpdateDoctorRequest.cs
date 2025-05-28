namespace AppointmentApp.Models.DTOs
{ 
    public class UpdateDoctorRequest
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public float YearsOfExperience { get; set; }
        public List<int>? SpecialityIds { get; set; }
    }
    
}