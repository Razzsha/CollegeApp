using CollegeApp.MyLoggin;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {

        private  readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        { 
            _logger =  logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogTrace("Log message from trace method");
            _logger.LogDebug("Log message from Debug method");
            _logger.LogInformation("Log message Information trace method");
            _logger.LogWarning("Log message from Warning method");
            _logger.LogError("Log message from Eroor method");
            _logger.LogCritical("Log message from Critical method");

            return Ok("Log message sent.");
        }
    }
}
