using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApp.Models
{
    public class Appointment : IEquatable<Appointment>
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public Appointment(string patientName, int patientAge, DateTime appointmentDate, string reason)
        {
            PatientName = patientName;
            PatientAge = patientAge;
            AppointmentDate = appointmentDate;
            Reason = reason;
        }
        public Appointment() { }

        public bool Equals(Appointment? other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }
    }
}
