using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileDAO profileDAO;

        public ProfileController(IProfileDAO profileDAO)
        {
            this.profileDAO = profileDAO;
        }

        [HttpGet("{profileId}")]
        public IActionResult GetProfile(int profileId)
        {
            Profile profile = profileDAO.GetProfile(profileId);
            return Ok(profile);
        }
    }
}
