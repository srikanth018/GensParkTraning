using System.Data;
using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]



    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] AddDoctorRequest doctor)
        {
            if (doctor == null)
            {
                return BadRequest("Doctor cannot be null");
            }

            if (string.IsNullOrWhiteSpace(doctor.Name) || string.IsNullOrWhiteSpace(doctor.PhoneNumber))
            {
                return BadRequest("Doctor name and phone number are required");
            }
            if (doctor.YearsOfExperience < 0)
            {
                return BadRequest("Years of experience cannot be negative");
            }

            var addedDoctor = await _doctorService.Add(doctor);
            return Ok(addedDoctor);

        }

        [HttpGet]
        [Route("speciality")]
        public async Task<ActionResult<IEnumerable<DoctorsBySpecialityResponseDto>>> GetDoctorsBySpeciality([FromQuery] string speciality)
        {
            if (string.IsNullOrWhiteSpace(speciality))
            {
                return BadRequest("Speciality cannot be null or empty");
            }

            var doctors = await _doctorService.GetBySpeciality(speciality);
            if (doctors == null || !doctors.Any())
            {
                return NotFound($"No doctors found for speciality {speciality}");
            }

            return Ok(doctors);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid doctor ID");
            }

            var doctor = await _doctorService.GetById(id);
            if (doctor == null)
            {
                return NotFound($"Doctor with ID {id} not found");
            }

            return Ok(doctor);
        }

        [HttpGet]
        [Authorize(Roles ="Doctor")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAll();
            return Ok(doctors);
        }
        [HttpGet("name")]
        public async Task<IActionResult> GetDoctorsByName([FromQuery] string name)
        {
            

            var doctors = await _doctorService.GetByName(name);
            if (doctors == null || !doctors.Any())
            {
                return NotFound($"No doctors found with the name {name}");
            }

            return Ok(doctors);
        }
    }
}