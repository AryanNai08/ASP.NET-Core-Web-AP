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
            return new List<Student>(){
            new Student
            {
                Id = 1,
                Studentname = "Aryan Nai",
                Email = "aryannai841@gmail.com",
                Address = "Ahmedabad,Gujarat"
            },
            new Student
            {
                Id = 2,
                Studentname = "Kartik Ahir",
                Email = "kartik@gmail.com",
                Address = "Surat,Gujarat"
            } };


        }
    }
}
