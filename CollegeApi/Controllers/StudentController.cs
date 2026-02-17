using CollegeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")] //Defines the base URL route
    [ApiController]//Marks the class as a Web API controller
    public class StudentController : ControllerBase
    {


        //first end point
        //request method id endpoint
        [HttpGet]
        //api/student/All
        [Route("All", Name = "GetAllStudents")]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //Get all student details
        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {
            //without linq

            //var students = new List<StudentDTO>();
            //foreach(var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        Studentname=item.Studentname,
            //        Address=item.Address,
            //        Email=item.Email
            //    };
            //    students.Add(obj);
            //}


            //with linq

            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                Studentname = s.Studentname,
                Address = s.Address,
                Email = s.Email
            });

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
        public ActionResult<StudentDTO> GetStudentById(int id)
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

            var studentDTO = new StudentDTO
            {
                Id = Student.Id,
                Studentname = Student.Studentname,
                Email = Student.Email,
                Address = Student.Address
            };

            return Ok(studentDTO);



        }


        //Get  single student details by Name
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
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
            var studentDTO = new StudentDTO
            {
                Id = Student.Id,
                Studentname = Student.Studentname,
                Email = Student.Email,
                Address = Student.Address
            };
            return Ok(studentDTO);


        }

        //create student record
        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if (model == null) 
            {
                return BadRequest();
            }

            int newid=CollegeRepository.Students.LastOrDefault().Id+1;

            Student student = new Student
            {
                Id = newid,
                Studentname = model.Studentname,
                Email = model.Email,
                Address = model.Address
            };

            CollegeRepository.Students.Add(student);

            model.Id = newid;

            return Ok(model);

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
