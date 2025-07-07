using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        static private List<Activity> ActivityList = new List<Activity>
        {
            new Activity
            {
                Id = 3,
                Content = "Go shopping",
                Details = "",
                Completed = false
            },
            new Activity
            {
                Id = 2,
                Content = "Go camping",
                Details = "Go with my family",
                Completed = false
            }
        };
        [HttpGet]
        public ActionResult<Activity> GetActivities()
        {
            return Ok(ActivityList);
        }
        [HttpGet("{id}")]
        public ActionResult<Activity> GetActivityById(int Id)
        {
            var result = ActivityList.FirstOrDefault(x => x.Id == Id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public ActionResult<Activity> AddActivity(Activity activity)
        {
            if (activity == null)
            {
                return BadRequest();
            }
            var existingActivity = ActivityList.FirstOrDefault(x => x.Id == activity.Id);
            if (existingActivity != null)
            {
                return BadRequest("Resource already existed");
            }
            ActivityList.Add(activity);

            return CreatedAtAction(
                nameof(GetActivityById),
                new { id = activity.Id },
                activity
            );
        }
        [HttpPut("{id}")]
        public IActionResult UpdateActivity(int Id, Activity activity)
        {
            if (activity == null)
            {
                return BadRequest();
            }
            var existingActivity = ActivityList.FirstOrDefault(x => x.Id == Id);
            if (existingActivity == null)
            {
                return NotFound();
            }
            if (Id != activity.Id)
            {
                return BadRequest();
            }

            existingActivity.Id = activity.Id;
            existingActivity.Content = activity.Content;
            existingActivity.Details = activity.Details;
            existingActivity.Completed = activity.Completed;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActivity(int Id)
        {
            var existingActivity = ActivityList.FirstOrDefault(x => x.Id == Id);
            if (existingActivity == null)
            {
                return NotFound();
            }

            ActivityList.Remove(existingActivity);

            return NoContent();
        }
    }
}