using AppointmentApp.Misc;

namespace AppointmentApp.Models.DTOs
{
    public class AddDoctorRequest
    {
        [NameValidation]
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public float YearsOfExperience { get; set; }
        public ICollection<AddSpecialityRequest>? Specialities { get; set; }    
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
}
    
}