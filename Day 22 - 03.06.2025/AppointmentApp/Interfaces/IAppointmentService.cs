using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment> Add(AddAppointmentRequest appointmentRequest);
        Task<Appointment?> GetById(int id);
        Task<Appointment> CancelAppointment(int id);
    }
}