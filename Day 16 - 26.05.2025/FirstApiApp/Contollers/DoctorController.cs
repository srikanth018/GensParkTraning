using System.Data;
using FirstApiApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]



    public class DoctorController : ControllerBase
    {
        static List<Doctor> doctors = new List<Doctor>()
        {
            new Doctor { Id = 101, Name = "Sri" },
            new Doctor { Id = 102, Name = "Kanth" },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            if (doctors.Count == 0)
            {
                return NotFound("No doctors found");
            }
            return Ok(doctors);
        }


        [HttpPost]
        public ActionResult<Doctor> AddDoctor(Doctor doctor)
        {
            
            int? id = (doctors.Count > 0 && (doctor.Id == null || doctor.Id == 0)) ?  doctors.Max(d => d.Id) + 1 : 101;
            
            
            doctor.Id = id;
            
            doctors.Add(doctor);
            return Created("", doctor);
        }

        [HttpPut]

        public ActionResult<Doctor> UpdateDoctor(Doctor doctor)
        {
            var updateDoctor = doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (updateDoctor == null)
            {
                return NotFound(doctor);
            }
            updateDoctor.Name = doctor.Name;
            return Ok(updateDoctor);
        }

        [HttpDelete]
        public ActionResult<Doctor> DeleteDoctor(int id)
        {
            var deleteDoctor = doctors.FirstOrDefault(d => d.Id == id);
            if (deleteDoctor == null)
            {
                return NotFound(deleteDoctor);
            }
            doctors.Remove(deleteDoctor);
            return Ok(deleteDoctor);
        }
    }
}