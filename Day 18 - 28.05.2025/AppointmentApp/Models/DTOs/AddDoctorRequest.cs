namespace AppointmentApp.Models.DTOs
{
    public class AddDoctorRequest
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public float YearsOfExperience { get; set; }
        public List<int> SpecialityIds { get; set; } = new List<int>();
    }
    
}