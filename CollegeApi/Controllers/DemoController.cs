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
        private readonly IMyLogger _myLogger;
        public DemoController(IMyLogger myLogger)
        {
            _myLogger = myLogger;
        }


        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method started");
            return Ok();
        }
       }
}
