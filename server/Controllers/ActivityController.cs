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
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly PersonalPlannerContext _context;
        public ActivityController(PersonalPlannerContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<Activity>> GetActivities()
        {
            var ActivityList = await _context.activities.ToListAsync();
            return Ok(ActivityList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityById(int Id)
        {
            var activity = await _context.activities.FindAsync(Id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }
        [HttpPost]
        public async Task<ActionResult<Activity>> AddActivity(Activity activity)
        {
            if (activity == null)
            {
                return BadRequest();
            }
            var existingActivity = await _context.activities.FindAsync(activity.id);
            if (existingActivity != null)
            {
                return BadRequest("Resource already existed");
            }
            await _context.activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetActivityById),
                new { id = activity.id },
                activity
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(int Id, Activity activity)
        {
            if (activity == null)
            {
                return BadRequest("Missing payload");
            }
            var existingActivity = await _context.activities.FindAsync(Id);
            if (existingActivity == null)
            {
                return NotFound();
            }

            existingActivity.content = activity.content;
            existingActivity.details = activity.details;
            existingActivity.completed = activity.completed;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int Id)
        {
            var existingActivity = await _context.activities.FindAsync(Id);
            if (existingActivity == null)
            {
                return NotFound();
            }

            _context.activities.Remove(existingActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}