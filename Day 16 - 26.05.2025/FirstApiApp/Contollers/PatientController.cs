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
            new Patient { Id = 101, Name = "Arun", Age = 30, Gender = "Male", PhoneNumber = "9834023519", BloodType = "O+" },
            new Patient { Id = 102, Name = "Divya", Age = 45, Gender = "Female", PhoneNumber = "9876543210", BloodType = "A-" },
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

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            var patient = patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }
            return Ok(patient);
        }

        [HttpPost]
        public ActionResult<Patient> AddPatient([FromBody] Patient patient)
        {
            int? id = (patients.Count > 0 && (patient.Id == null || patient.Id == 0)) ? patients.Max(p => p.Id) + 1 : 1;
            patient.Id = id;

            if (patient.PhoneNumber.Length != 10)
            {
                return BadRequest("Phone number must be exactly 10 digits");
            }
            patients.Add(patient);
            return Created("", patient);
        }

        [HttpPut("{id}")]
        public ActionResult<Patient> UpdatePatient(int id, [FromBody] Patient patient)
        {
            var updatePatient = patients.FirstOrDefault(p => p.Id == id);
            if (updatePatient == null)
            {
                return NotFound("Patient not found");
            }

            if (!string.IsNullOrWhiteSpace(patient.Name) && patient.Name != "string")
                updatePatient.Name = patient.Name;

            if (patient.Age > 0 && patient.Age != 0)
                updatePatient.Age = patient.Age;

            if (!string.IsNullOrWhiteSpace(patient.Gender) && patient.Gender != "string")
                updatePatient.Gender = patient.Gender;

            if (!string.IsNullOrWhiteSpace(patient.PhoneNumber) && patient.PhoneNumber.Length == 10 && patient.PhoneNumber != "string")
                updatePatient.PhoneNumber = patient.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(patient.BloodType) && patient.BloodType != "string")
                updatePatient.BloodType = patient.BloodType;

            return Ok(updatePatient);
        }

        [HttpDelete("{id}")]
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
