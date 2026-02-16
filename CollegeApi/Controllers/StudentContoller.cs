using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentContoller : ControllerBase
    {

        [HttpGet]
        public string GetStudent()
        {
            return "Aryan Nai";
        }
    }
}
