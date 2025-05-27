using AppointmentApp.Models;
using AppointmentApp.Repositories;

namespace AppointmentApp.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public IEnumerable<Appointment>? GetAllAppointments()
        {
            return _appointmentRepository.GetAll();
        }

        public Appointment? GetAppointmentById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public Appointment AddAppointment(Appointment appointment)
        {
            return _appointmentRepository.Add(appointment);
        }

        public Appointment? UpdateAppointment(Appointment appointment)
        {
            return _appointmentRepository.Update(appointment);
        }

        public Appointment? DeleteAppointment(int id)
        {
            return _appointmentRepository.Delete(id);
        }

        public IEnumerable<Appointment>? SearchAppointments(int? patientId, DateTime? appointmentDate, int? doctorId)
        {
            return _appointmentRepository.SearchAppointments(patientId, appointmentDate, doctorId);
        }
        
        public Appointment? RescheduleAppointment(int id, DateTime newDate)
        {
            return _appointmentRepository.RescheduleAppointment(id, newDate);
        }
    }
}