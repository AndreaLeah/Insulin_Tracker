using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("activity")]
    [ApiController]
    public class ActivityController : Controller
    {
        private readonly IActivityDAO activityDAO;
        public ActivityController(IActivityDAO activityDAO)
        {
            this.activityDAO = activityDAO;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllActivity()
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            List<Activity> activities = activityDAO.GetAllActivity(userId);
            return Ok(activities);
        }

        [Authorize]
        [HttpPost]
        public IActionResult RecordActivityToLog([FromBody] Activity activity)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            activity.UserId = userId;

            bool result = activityDAO.AddActivityToLog(activity);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "An error occurred and profile activity was not added to activity log." });
            }

        }

    }
}
