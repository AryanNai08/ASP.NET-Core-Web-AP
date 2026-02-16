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
        public ActionResult<IEnumerable<Student>> GetStudent()
        {
            //ok-200-success
            return Ok(CollegeRepository.Students);

        }



        //Get  single student details by id
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentsByid")]
        public ActionResult<Student> GetStudentById(int id) 
        {

            if (id <= 0)
            {
                //bad request-400-client side error
                return BadRequest();
            }

            var Student = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();

            if (Student == null)
            {
                return NotFound("Student not found");
            }
            else
            {
                return Ok(Student);
            }
            

        }


        //Get  single student details by Name
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        public ActionResult<Student> GetStudentByName(string name)
        {

            //return CollegeRepository.Students.Where(x => x.Studentname == name).FirstOrDefault();
            //return CollegeRepository.Students
            //    .Where(x => x.Studentname.ToLower().Contains(name.ToLower())).ToList();

            //above 2 are other ways 

            


            if (string.IsNullOrEmpty(name))
            {
                //bad request-400-client side error
                return BadRequest();
            }

            var Student = CollegeRepository.Students
                .Where(x => x.Studentname.ToLower().Contains(name.ToLower()))
                .FirstOrDefault();

            if (Student == null)
            {
                return NotFound("Student not found");
            }
            else
            {
                return Ok(Student);
            }

        }

        //Get  single student details by id
        [HttpDelete("{id}", Name = "DeleteStudentById")]
        public ActionResult<bool> DeleteStudentById(int id)
        {
            //var sid=CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();

            //return CollegeRepository.Students.Remove(sid);

            if (id <= 0)
            {
                //bad request-400-client side error
                return BadRequest();
            }

            var Student = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();

            if (Student == null)
            {
                return NotFound("Student id not found");
            }
            else
            {
                return Ok(true);
            }

        }
    }
}
