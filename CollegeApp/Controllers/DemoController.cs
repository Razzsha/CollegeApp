using CollegeApp.MyLoggin;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //1. Strongly coupled/tightly coupled
        //2. Loosely coupled

        private  readonly IMyLogger _myLogger;

        public DemoController(IMyLogger myLogger)
        {
            _myLogger =  myLogger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("This is a log message from DemoController.");
            
            return Ok("Log message sent.");
        }
    }
}
