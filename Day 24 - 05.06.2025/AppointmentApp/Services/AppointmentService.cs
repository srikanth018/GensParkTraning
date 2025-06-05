using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using AutoMapper;

namespace AppointmentApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<int, Appointment> _appointmentRepository;
        private readonly IRepository<int, Doctor> _doctorRepository;
        private readonly IRepository<int, Patient> _patientRepository;

        public AppointmentService(IRepository<int, Appointment> appointmentRepository,
            IRepository<int, Doctor> doctorRepository,
            IRepository<int, Patient> patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<Appointment> Add(AddAppointmentRequest appointmentRequest)
        {
            if (appointmentRequest == null)
            {
                throw new ArgumentNullException(nameof(appointmentRequest), "Appointment request cannot be null");
            }

            var appointment = new Appointment
            {
                PatientId = appointmentRequest.PatientId,
                DoctorId = appointmentRequest.DoctorId,
                AppointmentDate = appointmentRequest.AppointmentDate,
                Status = "Scheduled",
                Reason = appointmentRequest.Description,
            };
            var addedAppointment = await _appointmentRepository.Add(appointment);
            var doctor = await _doctorRepository.GetById(appointmentRequest.DoctorId);
            addedAppointment.Doctor = doctor;
            var patient = await _patientRepository.GetById(appointmentRequest.PatientId);
            addedAppointment.Patient = patient;
            return addedAppointment; 
        }

        public async Task<Appointment> CancelAppointment(int id)
        {
            var existingAppointment = await _appointmentRepository.GetById(id);
            if (existingAppointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            existingAppointment.Status = "Cancelled";
            return await _appointmentRepository.Update(id, existingAppointment);
        }

        public async Task<Appointment?> GetById(int id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            var doctor = await _doctorRepository.GetById(appointment?.DoctorId ?? 0);
            var patient = await _patientRepository.GetById(appointment?.PatientId ?? 0);
            if (appointment != null)
            {
                appointment.Doctor = doctor;
                appointment.Patient = patient;
            }
            
            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            return appointment;
        }
        
    }
}