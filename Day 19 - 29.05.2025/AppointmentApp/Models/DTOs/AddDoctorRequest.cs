namespace AppointmentApp.Models.DTOs
{
    public class AddDoctorRequest
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public float YearsOfExperience { get; set; }
        public ICollection<AddSpecialityRequest>? Specialities { get; set; }    
}
    
}