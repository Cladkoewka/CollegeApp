using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IMyLogger _myLogger;

        public DemoController()
        {
            _myLogger = new LogToFile();
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method started");
            return Ok();
        }
    }
}
