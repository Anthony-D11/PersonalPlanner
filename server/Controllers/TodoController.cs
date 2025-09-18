using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoController(PersonalPlannerContext context) : ControllerBase
    {
        private readonly PersonalPlannerContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<TodoDTO>>> GetTodos()
        {
            var todos = await _context.Todos
            .Select(item => new TodoDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ActiveDate = item.ActiveDate,
                DueDate = item.DueDate,
                Completed = item.Completed,
                TagIds = item.TodosTags.Where(t => t.TodoId == item.Id).Select(t => t.TagId),
                CategoryId = item.CategoryId
            })
            .ToListAsync();
            return Ok(todos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDTO>> GetTodoById(int id)
        {
            var todo = await _context.Todos
            .Select(item => new TodoDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ActiveDate = item.ActiveDate,
                DueDate = item.DueDate,
                Completed = item.Completed,
                TagIds = item.TodosTags.Where(t => t.TodoId == item.Id).Select(t => t.TagId),
                CategoryId = item.CategoryId
            })
            .FirstOrDefaultAsync(c => c.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }
        [HttpPost]
        public async Task<ActionResult<TodoDTO>> AddTodo(NewTodoDTO newTodoDTO)
        {
            if (newTodoDTO == null)
            {
                return BadRequest();
            }
            Todo newTodo = new Todo
            {
                Title = newTodoDTO.Title,
                Description = newTodoDTO.Description,
                ActiveDate = newTodoDTO.ActiveDate,
                DueDate = newTodoDTO.DueDate,
                Completed = newTodoDTO.Completed,
                CategoryId = newTodoDTO.CategoryId
            };
            foreach (var tagId in newTodoDTO.TagIds)
            {
                newTodo.TodosTags.Add(new TodoTag { TagId = tagId });
            }
            _context.Todos.Add(newTodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoById),
                new { id = newTodo.Id },
                new TodoDTO
                {
                    Id = newTodo.Id,
                    Title = newTodo.Title,
                    Description = newTodo.Description,
                    ActiveDate = newTodo.ActiveDate,
                    DueDate = newTodo.DueDate,
                    Completed = newTodo.Completed,
                    TagIds = newTodo.TodosTags.Select(item => item.TagId),
                    CategoryId = newTodo.CategoryId
                }
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, NewTodoDTO newTodoDTO)
        {
            if (newTodoDTO == null)
            {
                return BadRequest("Missing payload");
            }
            var existingTodo = await _context.Todos
            .FirstOrDefaultAsync(c => c.Id == id);

            if (existingTodo == null)
            {
                return NotFound();
            }

            existingTodo.Title = newTodoDTO.Title;
            existingTodo.Description = newTodoDTO.Description;
            existingTodo.ActiveDate = newTodoDTO.ActiveDate;
            existingTodo.DueDate = newTodoDTO.DueDate;
            existingTodo.Completed = newTodoDTO.Completed;
            existingTodo.CategoryId = newTodoDTO.CategoryId;

            _context.TodosTags.RemoveRange(_context.TodosTags.Where(t => t.TodoId == id));

            existingTodo.TodosTags = [];
            foreach (var tagId in newTodoDTO.TagIds)
            {
                existingTodo.TodosTags.Add(new TodoTag { TagId = tagId });
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var existingTodo = await _context.Todos.FindAsync(id);
            if (existingTodo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(existingTodo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}