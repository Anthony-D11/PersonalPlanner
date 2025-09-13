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
    public class TodoListController(PersonalPlannerContext context) : ControllerBase
    {
        private readonly PersonalPlannerContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<TodoList>>> GetTodoLists()
        {
            var TodoLists = await _context.TodoLists.ToListAsync();
            return Ok(TodoLists);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetTodoListById(int id)
        {
            var TodoList = await _context.TodoLists.FindAsync(id);
            if (TodoList == null)
            {
                return NotFound();
            }
            return Ok(TodoList);
        }
        [HttpPost]
        public async Task<ActionResult<TodoList>> AddTodoList(TodoList TodoList)
        {
            if (TodoList == null)
            {
                return BadRequest();
            }
            _context.TodoLists.Add(TodoList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoListById),
                new { id = TodoList.Id },
                TodoList
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoList TodoList)
        {
            if (TodoList == null)
            {
                return BadRequest("Missing payload");
            }
            var existingTodoList = await _context.TodoLists.FindAsync(id);
            if (existingTodoList == null)
            {
                return NotFound();
            }

            existingTodoList.Name = TodoList.Name;
            existingTodoList.Color = TodoList.Color;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var existingTodoList = await _context.TodoLists.FindAsync(id);
            if (existingTodoList == null)
            {
                return NotFound();
            }

            _context.TodoLists.Remove(existingTodoList);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}