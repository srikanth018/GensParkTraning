using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AppointmentApp.Interfaces;
using AppointmentApp.Models;

namespace AppointmentApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        IRepository<int, Appointment> _appointmentRepo;
        public AppointmentService(IRepository<int, Appointment> appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        public Appointment? AddAppointment(Appointment appointment)
        {
            try
            {
                var output = _appointmentRepo.Add(appointment);
                if(output != null)
                {
                    return output;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        public List<Appointment>? SearchAppointments(AppointmentSearchModel appointmentSearchModel)
        {
            try
            {
                var appointment = _appointmentRepo.GetAll();
                appointment = SearchByName(appointment, appointmentSearchModel.PatientName);
                appointment = SearchByAge(appointment, appointmentSearchModel.AgeRange);
                appointment = SearchByDate(appointment, appointmentSearchModel.AppointmentDate);

                if (appointment != null && appointment.Count > 0)
                {
                    return appointment.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null; 
        }
        public ICollection<Appointment> SearchByName(ICollection<Appointment> appointments, string? Name)
        {
            if(Name == null)
            {
                return appointments;
            }
            var appointment = appointments.Where(x => x.PatientName.Contains(Name)).ToList();
            return appointment;
        }

        public ICollection<Appointment> SearchByAge(ICollection<Appointment> appointments, Range<int>? Age)
        {
            if (Age == null)
            {
                return appointments;
            }
            var appointment = appointments.Where(x => x.PatientAge >= Age.Min && x.PatientAge<=Age.Max).ToList();
            return appointment;
        }
        public ICollection<Appointment> SearchByDate(ICollection<Appointment> appointments, DateTime? AppointmentDate)
        {
            if (AppointmentDate == null)
            {
                return appointments;
            }
            var appointment = appointments.Where(x => x.AppointmentDate.Equals(AppointmentDate)).ToList();
            return appointment;
        }
    }
}
