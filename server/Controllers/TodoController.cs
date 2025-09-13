using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using server.Data;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoController(PersonalPlannerContext context) : ControllerBase
    {
        private readonly PersonalPlannerContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            var TodoList = await _context.Todos.ToListAsync();
            return Ok(TodoList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var Todo = await _context.Todos.FindAsync(id);
            if (Todo == null)
            {
                return NotFound();
            }
            return Ok(Todo);
        }
        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo(Todo todo)
        {
            if (todo == null)
            {
                return BadRequest();
            }
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoById),
                new { id = todo.Id },
                todo
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, Todo Todo)
        {
            if (Todo == null)
            {
                return BadRequest("Missing payload");
            }
            var existingTodo = await _context.Todos.FindAsync(id);
            if (existingTodo == null)
            {
                return NotFound();
            }

            existingTodo.Title = Todo.Title;
            existingTodo.Description = Todo.Description;
            existingTodo.ListId = Todo.ListId;
            existingTodo.ActiveDate = Todo.ActiveDate;
            existingTodo.DueDate = Todo.DueDate;
            existingTodo.Completed = Todo.Completed;

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