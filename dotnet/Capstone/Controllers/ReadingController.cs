using Capstone.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("readings")]
    [ApiController]
    public class ReadingController : ControllerBase
    {
        private readonly IReadingDAO readingDAO;
    }
}
