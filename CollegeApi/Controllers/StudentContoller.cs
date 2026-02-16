using CollegeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")] //Defines the base URL route
    [ApiController]//Marks the class as a Web API controller
    public class StudentContoller : ControllerBase
    {
        //request method id endpoint
        [HttpGet]

        //first end point
        public IEnumerable<Student> GetStudent()
        {
            return CollegeRepository.Students;

        }
    }
}
