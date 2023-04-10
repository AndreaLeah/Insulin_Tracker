using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileDAO profileDAO;

        public ProfileController(IProfileDAO profileDAO)
        {
            this.profileDAO = profileDAO;
        }

        [Authorize]
        [HttpGet("{profileId}")]
        public IActionResult GetProfile(int profileId)
        {
            Profile profile = profileDAO.GetProfile(profileId);
            return Ok(profile);
        }

        [Authorize]
        [HttpGet()]
        public IActionResult GetAllUserProfiles()
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            List<Profile> profiles = profileDAO.GetAllUserProfiles(userId);
            return Ok(profiles);
        }

        [Authorize]
        [HttpPut()]
        public IActionResult UpdateProfile([FromBody] Profile profile)
        {
            profileDAO.UpdateProfile(profile);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{profileId}")]
        public IActionResult DeleteProfile(int profileId)
        {
            return Ok();
        }
    }
}
