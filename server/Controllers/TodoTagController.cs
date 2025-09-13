using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoTagController(PersonalPlannerContext context) : ControllerBase
    {
        private readonly PersonalPlannerContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<TodoTag>>> GetTodoTags()
        {
            var TodoTags = await _context.TodoTags.ToListAsync();
            return Ok(TodoTags);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTag>> GetTodoTagById(int id)
        {
            var TodoTag = await _context.TodoTags.FindAsync(id);
            if (TodoTag == null)
            {
                return NotFound();
            }
            return Ok(TodoTag);
        }
        [HttpPost]
        public async Task<ActionResult<TodoTag>> AddTodoTag(TodoTag todoTag)
        {
            if (todoTag == null)
            {
                return BadRequest();
            }
            _context.TodoTags.Add(todoTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoTagById),
                new { id = todoTag.Id },
                todoTag
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoTag todoTag)
        {
            if (todoTag == null)
            {
                return BadRequest("Missing payload");
            }
            var existingTodoTag = await _context.TodoTags.FindAsync(id);
            if (existingTodoTag == null)
            {
                return NotFound();
            }

            existingTodoTag.Name = todoTag.Name;
            existingTodoTag.Color = todoTag.Color;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var existingTodoTag = await _context.TodoTags.FindAsync(id);
            if (existingTodoTag == null)
            {
                return NotFound();
            }

            _context.TodoTags.Remove(existingTodoTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}