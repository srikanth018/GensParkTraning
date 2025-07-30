using Microsoft.AspNetCore.Mvc;
using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;
using SampleMigrateApp.Services;

namespace SampleMigrateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetAll()
        {
            var colors = await _colorService.GetColors();
            return Ok(colors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetById(int id)
        {
            try
            {
                var color = await _colorService.GetColorById(id);
                return Ok(color);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Color>> Create([FromBody] CreateColorDTO colorDTO)
        {
            var created = await _colorService.CreateColor(colorDTO);
            return CreatedAtAction(nameof(GetById), new { id = created.ColorId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Color>> Update(int id, [FromBody] EditColorDTO colorDTO)
        {
            try
            {
                var updated = await _colorService.EditColor(id, colorDTO);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Color>> Delete(int id)
        {
            try
            {
                var deleted = await _colorService.DeleteColor(id);
                return Ok(deleted);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
