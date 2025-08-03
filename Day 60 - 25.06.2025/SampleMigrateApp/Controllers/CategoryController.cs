using Microsoft.AspNetCore.Mvc;
using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;
using SampleMigrateApp.Services;

namespace SampleMigrateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetCategoryList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int? page)
        {
            try
            {
                var categories = await _categoryService.GetCategoryListByPage(page);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Update(int id, [FromBody] EditCategoryDTO dto)
        {
            try
            {
                var updatedCategory = await _categoryService.EditCategory(id, dto);
                return Ok(updatedCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            try
            {
                var deletedCategory = await _categoryService.DeleteCategory(id);
                return Ok(deletedCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            if (createCategoryDTO == null || string.IsNullOrWhiteSpace(createCategoryDTO.Name))
            {
                return BadRequest(new { message = "Invalid category data." });
            }

            var name = createCategoryDTO.Name.Trim();

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new { message = "Category name cannot be empty." });
            }

            try
            {
                var newCategory = await _categoryService.CreateCategory(name);
                return CreatedAtAction(nameof(GetById), new { id = newCategory.CategoryId }, newCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}