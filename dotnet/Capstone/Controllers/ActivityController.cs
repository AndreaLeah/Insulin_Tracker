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
    }
}
