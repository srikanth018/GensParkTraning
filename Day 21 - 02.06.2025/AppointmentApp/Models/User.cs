using System.ComponentModel.DataAnnotations;

namespace AppointmentApp.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Password { get; set; }
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}