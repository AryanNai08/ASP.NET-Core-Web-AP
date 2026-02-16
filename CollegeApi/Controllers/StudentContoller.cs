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

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //Get all student details
        public ActionResult<IEnumerable<Student>> GetStudent()
        {
            //ok-200-success
            return Ok(CollegeRepository.Students);

        }



        //Get  single student details by id
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentsByid")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentByName(string name)
        {

                      


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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudentById(int id)
        {
            

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
