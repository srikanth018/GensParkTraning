using Microsoft.AspNetCore.Mvc;
using AppointmentApp.Models;
using AppointmentApp.Interfaces;
using AppointmentApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> AddPatient([FromBody] AddPatientRequest patientRequest)
        {
            if (patientRequest == null)
            {
                return BadRequest("Patient request cannot be null");
            }

            try
            {
                var patient = await _patientService.Add(patientRequest);
                return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
    }
}
