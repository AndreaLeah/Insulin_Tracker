using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

            // Activity object used to log an error to the activity log
            Activity errorActivity = new Activity();
            errorActivity.UserId = userId;
            errorActivity.ActivityName = "ERROR WHILE LOGGING";

            bool result = activityDAO.AddActivityToLog(activity);
            if (result)
            {
                return Ok();
            }
            else
            {
                //attempt to log errorActivity to the activity log and send BadRequest to client
                activityDAO.AddActivityToLog(errorActivity);
                return BadRequest(new { message = "An error occurred and profile activity was not added to activity log." });
            }

        }

    }
}
