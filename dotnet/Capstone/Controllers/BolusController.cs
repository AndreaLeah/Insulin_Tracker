using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("bolus")]
    [ApiController]
    public class BolusController : ControllerBase
    {
        private readonly IBolusDAO bolusDAO;
        private readonly IReadingDAO readingDAO;

        public BolusController(IBolusDAO bolusDAO, IReadingDAO readingDAO)
        {
            this.bolusDAO = bolusDAO;
            this.readingDAO = readingDAO;
        }

        [Authorize]
        [HttpGet("{bolusId}")]
        public IActionResult GetBolus(int bolusId)
        {
            Bolus bolus = bolusDAO.GetBolus(bolusId);
            if (bolus == null)
            {
                return BadRequest(new { message = "Bolus does not exist" });
            }

            Reading reading = readingDAO.GetReading(bolus.ReadingId);
            if (reading == null)
            {
                return BadRequest(new { message = "Reading for bolus does not exist" });
            }

            int userId = int.Parse(this.User.FindFirst("sub").Value);
            if (userId != reading.UserId)
            {
                return BadRequest(new { message = "Reading user id does not match logged in user id" });
            }

            return Ok(bolus);
        }

        [Authorize]
        [HttpGet()]
        public IActionResult GetUserBoluses()
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            List<Bolus> boluses = bolusDAO.GetUserBoluses(userId);
            return Ok(boluses);
        }

        [Authorize]
        [HttpGet("detail/{bolusId}")]
        public IActionResult GetFullBolus(int bolusId)
        {
            FullBolusInfo bolus = bolusDAO.GetFullBolusInfo(bolusId);
            return Ok(bolus);
        }

        [Authorize]
        [HttpPost()]
        public IActionResult AddBolus([FromBody] Bolus bolus)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);

            Reading reading = readingDAO.GetReading(bolus.ReadingId);
            if (reading == null)
            {
                return BadRequest(new { message = "Reading does not exist" });
            }

            if (reading.UserId != userId)
            {
                return BadRequest(new { message = "Wrong user id" });
            }

            bool result = bolusDAO.AddBolus(bolus);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "An error occurred and bolus was not created." });
            }
        }
    }
}
