using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FirstApiApp.Models;

namespace FirstApiApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PatientController : ControllerBase
    {
        static List<Patient> patients = new List<Patient>()
        {
            new Patient { Id = 101, Name = "Arun", Age = 30},
            new Patient { Id = 102, Name = "Mouly", Age = 45 },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            if (patients.Count == 0)
            {
                return NotFound("No patients found");
            }
            return Ok(patients);
        }

        [HttpPost]
        public ActionResult<Patient> AddPatient(Patient patient)
        {
            int? id = (patients.Count > 0 && (patient.Id == null || patient.Id == 0)) ? patients.Max(p => p.Id) + 1 : 1;
            patient.Id = id;
            patients.Add(patient);
            return Created("", patient);
        }

        [HttpPut]
        public ActionResult<Patient> UpdatePatient(Patient patient)
        {
            var updatePatient = patients.FirstOrDefault(p => p.Id == patient.Id);
            if (updatePatient == null)
            {
                return NotFound("Patient not found");
            }

            _= (patient.Name == null || patient.Name.Trim() == "") ? updatePatient.Name = updatePatient.Name : updatePatient.Name = patient.Name;

            _= (patient.Age <= 0) ? updatePatient.Age = updatePatient.Age : updatePatient.Age = patient.Age;
            
            
            return Ok(updatePatient);
        }

        [HttpDelete]
        public ActionResult<Patient> DeletePatient(int id)
        {
            var deletePatient = patients.FirstOrDefault(p => p.Id == id);
            if (deletePatient == null)
            {
                return NotFound("Patient not found");
            }

            patients.Remove(deletePatient);
            return Ok(deletePatient);
        }
    }
}
