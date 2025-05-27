using AppointmentApp.Models;
using System.Collections.Generic;
namespace AppointmentApp.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment, int>
    {
        IEnumerable<Appointment>? SearchAppointments(int? patientId, DateTime? appointmentDate, int? doctorId);
        Appointment? RescheduleAppointment(int id, DateTime newDate);
    }

}