using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("readings")]
    [ApiController]
    public class ReadingController : ControllerBase
    {
        private readonly IReadingDAO readingDAO;
        private readonly IProfileDAO profileDAO;

        public ReadingController(IReadingDAO readingDAO, IProfileDAO profileDAO)
        {
            this.readingDAO = readingDAO;
            this.profileDAO = profileDAO;
        }

        [Authorize]
        [HttpGet("{readingId}")]
        public IActionResult GetReading(int readingId)
        {
            Reading reading = readingDAO.GetReading(readingId);
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            if (userId == reading.UserId)
            {
                return Ok(reading);
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize]
        [HttpGet()]
        public IActionResult GetUserReadings()
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            List<Reading> readings = readingDAO.GetUserReadings(userId);
            return Ok(readings);
        }

        [Authorize]
        [HttpGet("profile/{profileId}")]
        public IActionResult GetReadingsByProfile(int profileId)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Profile profile = profileDAO.GetProfile(profileId);

            if (userId != profile.UserId)
            {
                return BadRequest(new { message = "Profile does not belong to logged in user" });
            }

            List<Reading> readings = readingDAO.GetReadingsByProfile(profileId);

            return Ok(readings);
        }

        [Authorize]
        [HttpPost()]
        public IActionResult AddReading([FromBody] Reading reading)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Profile profile = profileDAO.GetProfile(reading.ProfileId);

            if (userId != reading.UserId)
            {
                return BadRequest(new { message = "User id in reading does not match logged in user id" });
            }
            if (userId != profile.UserId)
            {
                return BadRequest(new { message = "Profile id in reading does not match logged in user id" });
            }

            bool result = readingDAO.AddReading(reading);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "An error occurred and reading was not created." });
            }
        }
    }
}
