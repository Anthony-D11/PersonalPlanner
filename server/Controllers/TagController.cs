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
    public class TagController(PersonalPlannerContext context) : ControllerBase
    {
        private readonly PersonalPlannerContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> GetTodos()
        {
            var Tags = await _context.Tags.ToListAsync();
            return Ok(Tags);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTagById(int id)
        {
            var Tag = await _context.Tags.FindAsync(id);
            if (Tag == null)
            {
                return NotFound();
            }
            return Ok(Tag);
        }
        [HttpPost]
        public async Task<ActionResult<Tag>> AddTag(Tag tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTagById),
                new { id = tag.Id },
                tag
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, Tag tag)
        {
            if (tag == null)
            {
                return BadRequest("Missing payload");
            }
            var existingTag = await _context.Tags.FindAsync(id);
            if (existingTag == null)
            {
                return NotFound();
            }

            existingTag.Name = tag.Name;
            existingTag.Color = tag.Color;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var existingTag = await _context.Tags.FindAsync(id);
            if (existingTag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(existingTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}