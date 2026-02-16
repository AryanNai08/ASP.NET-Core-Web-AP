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
        //api/student/All
        [Route("All", Name = "GetAllStudents")]
        //Get all student details
        public IEnumerable<Student> GetStudent()
        {
            return CollegeRepository.Students;

        }



        //Get  single student details by id
        [HttpGet]
        [Route("{id}", Name = "GetStudentsByid")]
        public Student GetStudentById(int id) 
        {
            return CollegeRepository.Students.Where(x=>x.Id==id).FirstOrDefault();

        }


        //Get  single student details by Name
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        public Student GetStudentByName(string name)
        {

            //return CollegeRepository.Students.Where(x => x.Studentname == name).FirstOrDefault();
            //return CollegeRepository.Students
            //    .Where(x => x.Studentname.ToLower().Contains(name.ToLower())).ToList();

            //above 2 are other ways 

            return CollegeRepository.Students
                .Where(x => x.Studentname.ToLower().Contains(name.ToLower()))
                .FirstOrDefault();

        }

        //Get  single student details by id
        [HttpDelete("{id}", Name = "DeleteStudentById")]
        public bool DeleteStudentById(int id)
        {
            var sid=CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();

            return CollegeRepository.Students.Remove(sid);

        }
    }
}
