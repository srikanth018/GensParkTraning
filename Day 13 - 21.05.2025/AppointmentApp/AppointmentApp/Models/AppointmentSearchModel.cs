using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApp.Models
{
    public class AppointmentSearchModel
    {
        public string? PatientName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public Range<int>? AgeRange { get; set; }
        public string? Reason { get;  set; }
    }
    public class Range<T>
    {
        public T? Min { get; set; }
        public T? Max { get; set; }
    }
}
