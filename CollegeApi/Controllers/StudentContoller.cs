using CollegeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")] //Defines the base URL route
    [ApiController]//Marks the class as a Web API controller
    public class StudentContoller : ControllerBase
    {


        //first end point
        //request method id endpoint
        [HttpGet]
        //Get all student details
        public IEnumerable<Student> GetStudent()
        {
            return CollegeRepository.Students;

        }



        //Get all single student details by id
        [HttpGet("{id:int}")]
        public Student GetStudentID(int id)
        {
            return CollegeRepository.Students.Where(x=>x.Id==id).FirstOrDefault();

        }
    }
}
