using AppointmentApp.Interfaces;
using AppointmentApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace AppointmentApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> AddAppointment([FromBody] AddAppointmentRequest appointmentRequest)
        {
            if (appointmentRequest == null)
            {
                return BadRequest("Appointment request cannot be null");
            }

            try
            {
                var appointment = await _appointmentService.Add(appointmentRequest);
                return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _appointmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }
        [HttpPost("{id}/cancel")]
        [Authorize(Policy = "ExperiencedDoctorOnly")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            try
            {
                var cancelledAppointment = await _appointmentService.CancelAppointment(id);
                return Ok(cancelledAppointment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Appointment with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}