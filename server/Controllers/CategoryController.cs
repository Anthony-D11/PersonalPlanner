using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController(PersonalPlannerContext context) : ControllerBase
    {
        private readonly PersonalPlannerContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var categories = await _context.Categories
            .Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Color = c.Color,
                TodoIds = c.Todos.Select(t => t.Id)
            })
            .ToListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _context.Categories
            .Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Color = c.Color,
                TodoIds = c.Todos.Select(t => t.Id)
            })
            .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategory(NewCategoryDTO newCategoryDTO)
        {
            if (newCategoryDTO == null)
            {
                return BadRequest();
            }
            Category newCategory = new Category
            {
                Name = newCategoryDTO.Name,
                Color = newCategoryDTO.Color
            };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = newCategory.Id },
                new CategoryDTO
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name,
                    Color = newCategory.Color
                }
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, NewCategoryDTO newCategoryDTO)
        {
            if (newCategoryDTO == null)
            {
                return BadRequest("Missing payload");
            }
            var existingCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = newCategoryDTO.Name;
            existingCategory.Color = newCategoryDTO.Color;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryDTO = await _context.Categories
            .Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Color = c.Color,
                TodoIds = c.Todos.Select(t => t.Id)
            })
            .FirstOrDefaultAsync(c => c.Id == id);

            if (categoryDTO == null)
            {
                return NotFound();
            }
            await RemoveCategoryFromTodo(categoryDTO.TodoIds);
            var existingCategory = await _context.Categories.FindAsync(id);

            _context.Categories.Remove(existingCategory!);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private async Task<IActionResult> RemoveCategoryFromTodo(IEnumerable<int> todoIds)
        {
            foreach (var todoId in todoIds)
            {
                var todo = await _context.Todos.FindAsync(todoId);
                if (todo != null)
                {
                    todo.CategoryId = null;
                    todo.Category = null;
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}