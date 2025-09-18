using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TagController(PersonalPlannerContext context) : ControllerBase
    {
        private readonly PersonalPlannerContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<TagDTO>>> GetTags()
        {
            var categories = await _context.Tags
            .Select(item => new TagDTO
            {
                Id = item.Id,
                Name = item.Name,
                Color = item.Color,
                TodoIds = item.TodosTags.Where(t => t.TagId == item.Id).Select(t => t.TodoId)
            })
            .ToListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetTagById(int id)
        {
            var tag = await _context.Tags
            .Select(item => new TagDTO
            {
                Id = item.Id,
                Name = item.Name,
                Color = item.Color,
                TodoIds = item.TodosTags.Where(t => t.TagId == item.Id).Select(t => t.TodoId)
            })
            .FirstOrDefaultAsync(c => c.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }
        [HttpPost]
        public async Task<ActionResult<TagDTO>> AddTag(NewTagDTO newTagDTO)
        {
            if (newTagDTO == null)
            {
                return BadRequest();
            }
            Tag newTag = new Tag
            {
                Name = newTagDTO.Name,
                Color = newTagDTO.Color
            };
            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTagById),
                new { id = newTag.Id },
                new TagDTO
                {
                    Id = newTag.Id,
                    Name = newTag.Name,
                    Color = newTag.Color
                }
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, NewTagDTO newTagDTO)
        {
            if (newTagDTO == null)
            {
                return BadRequest("Missing payload");
            }
            var existingTag = await _context.Tags
            .FirstOrDefaultAsync(c => c.Id == id);

            if (existingTag == null)
            {
                return NotFound();
            }

            existingTag.Name = newTagDTO.Name;
            existingTag.Color = newTagDTO.Color;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
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