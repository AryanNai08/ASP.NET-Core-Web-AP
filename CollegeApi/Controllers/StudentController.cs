using CollegeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [Route("{id:int}", Name = "GetStudentByid")]

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
        [ProducesResponseType(StatusCodes.Status201Created)]
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

            model.Id = student.Id;
            //link/location of newly created data
            //status code=201
            return CreatedAtRoute("GetStudentById", new {id=model.Id},model);
            

        }


        //update student api
        [HttpPut]
        [Route("Update")]
        //api/student/update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if(model==null || model.Id <= 0)
            {
                return BadRequest();
            }

            var existingStudent =CollegeRepository.Students.Where(s=>s.Id==model.Id).FirstOrDefault();

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Studentname = model.Studentname;
            existingStudent.Email = model.Studentname;
            existingStudent.Address = model.Address;

            return NoContent();
        }





        //updatepartial student api
        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        //api/student/id/updatepartial
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentpartial(int id,[FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                return BadRequest();
            }

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
            {
                return NotFound();
            }

            var studentDTO = new StudentDTO()
            {
                Id = existingStudent.Id,
                Studentname = existingStudent.Studentname,
                Email = existingStudent.Email,
                Address = existingStudent.Address,
            };

            patchDocument.ApplyTo(studentDTO,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            existingStudent.Studentname = studentDTO.Studentname;
            existingStudent.Email = studentDTO.Studentname;
            existingStudent.Address = studentDTO.Address;

            return NoContent();
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
