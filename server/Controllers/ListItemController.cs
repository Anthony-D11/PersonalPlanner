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
    public class ListItemController(PersonalPlannerContext context) : ControllerBase
    {
        private readonly PersonalPlannerContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<ListItem>>> GetTodos()
        {
            var ListItems = await _context.ListItems.ToListAsync();
            return Ok(ListItems);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ListItem>> GetListItemById(int id)
        {
            var ListItem = await _context.ListItems.FindAsync(id);
            if (ListItem == null)
            {
                return NotFound();
            }
            return Ok(ListItem);
        }
        [HttpPost]
        public async Task<ActionResult<ListItem>> AddListItem(ListItem listItem)
        {
            if (listItem == null)
            {
                return BadRequest();
            }
            _context.ListItems.Add(listItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetListItemById),
                new { id = listItem.Id },
                listItem
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, ListItem listItem)
        {
            if (listItem == null)
            {
                return BadRequest("Missing payload");
            }
            var existingListItem = await _context.ListItems.FindAsync(id);
            if (existingListItem == null)
            {
                return NotFound();
            }

            existingListItem.Name = listItem.Name;
            existingListItem.Color = listItem.Color;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var existingListItem = await _context.ListItems.FindAsync(id);
            if (existingListItem == null)
            {
                return NotFound();
            }

            _context.ListItems.Remove(existingListItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}