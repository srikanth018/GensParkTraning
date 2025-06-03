using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;

namespace AppointmentApp.Interfaces
{ 
    public interface IPatientService
    {
        Task<Patient> Add(AddPatientRequest patientRequest);
        Task<Patient?> GetById(int id);
    }
}