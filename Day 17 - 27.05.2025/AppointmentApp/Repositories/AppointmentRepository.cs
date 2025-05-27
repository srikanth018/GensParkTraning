using AppointmentApp.Models;

namespace AppointmentApp.Repositories
{

    public class AppointmentRepository : IAppointmentRepository
    {
        private List<Appointment> appointments = new List<Appointment>();


        public Appointment Add(Appointment appointment)
        {
            int? newid = (appointments.Count > 0 && (appointment.Id == null || appointment.Id == 0)) ? appointments.Max(a => a.Id) + 1 : 101;
            appointment.Id = newid;
            appointments.Add(appointment);
            return appointment;
        }

        public Appointment? Delete(int id)
        {
            var deleteAppointment = appointments.FirstOrDefault(a => a.Id == id);
            if (deleteAppointment == null) return null;
            appointments.Remove(deleteAppointment);
            return deleteAppointment;
        }

        public IEnumerable<Appointment>? GetAll()
        {
            if (appointments.Count == 0) return null;
            return appointments;
        }

        public Appointment? GetById(int id)
        {
            return appointments.FirstOrDefault(a => a.Id == id);
        }

        public Appointment? RescheduleAppointment(int id, DateTime newDate)
        {
            var appointment = appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null) return null;
            appointment.AppointmentDate = newDate;
            return appointment;
        }

        public IEnumerable<Appointment>? SearchAppointments(int? patientId, DateTime? appointmentDate, int? doctorId)
        {
            if (appointments.Count == 0) return null;
            var SearchAppointments = appointments;
            if (patientId != null || patientId != 0)
                SearchAppointments = appointments.Where(x => x.PatientId == patientId).ToList();
            if (appointmentDate != null)
                SearchAppointments = SearchAppointments.Where(x => x.AppointmentDate == appointmentDate).ToList();
            if (doctorId != null || doctorId != 0)
                SearchAppointments = SearchAppointments.Where(x => x.DoctorId == doctorId).ToList();
            return SearchAppointments;
        }


        public Appointment? Update(Appointment appointment)
        {
            var updateAppointment = appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (updateAppointment == null) return null;
            if (appointment.AppointmentDate == null)
            {
                appointment.AppointmentDate = updateAppointment.AppointmentDate;
            }
            if (appointment.DoctorId == null || appointment.DoctorId == 0)
            {
                appointment.DoctorId = updateAppointment.DoctorId;
            }
            if (!string.IsNullOrEmpty(appointment.Reason))
            {
                appointment.Reason = updateAppointment.Reason;
            }
            if (!string.IsNullOrEmpty(appointment.Status))
            {
                appointment.Status = updateAppointment.Status;
            }
            return updateAppointment;
        }
    }

}

