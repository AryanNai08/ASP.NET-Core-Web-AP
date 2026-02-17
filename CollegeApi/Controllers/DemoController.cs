using CollegeApi.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //1.Strongly coupled/tightly coupled
        //private readonly IMyLogger _myLogger;
        //public DemoController()
        //{
        //    _myLogger = new LogToFile();
        //}

        //2.Loosely coupled
        //private readonly IMyLogger _myLogger;
        private readonly ILogger<DemoController> _logger;
        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogTrace("Trace log");
            _logger.LogDebug("Debug log");
            _logger.LogInformation("Info log");
            _logger.LogWarning("Warning log");
            _logger.LogError("Error log");
            _logger.LogCritical("Critical log");

            return Ok();
        }
    }
}
