using CollegeApi.Data;
using CollegeApi.Models;
using CollegeApi.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")] //Defines the base URL route
    [ApiController]//Marks the class as a Web API controller
    public class StudentController : ControllerBase
    {

        private readonly ILogger<StudentController> _Logger;
        private readonly CollegeDBContext _dbContext;
        public StudentController(ILogger<StudentController> logger,CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
            _Logger = logger;
        }

       

        //first end point
        //request method id endpoint
        [HttpGet]
        //api/student/All
        [Route("All", Name = "GetAllStudents")]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //Get all student details
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudent()
        {
            //without linq

            //var students = new List<StudentDTO>();
            //foreach(var item in _dbContext.Students)
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

            _Logger.LogInformation("GetStudents method started ");
            //with linq


            var students =await  _dbContext.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                Studentname = s.Studentname,
                Address = s.Address,
                Email = s.Email,
                DOB = s.DOB.ToShortDateString(),
            }).ToListAsync();

            //ok-200-success
            return Ok(students);

        }



        //Get  single student details by id
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentByid")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> GetStudentByIdAsync(int id)
        {

            if (id <= 0)
            {
                _Logger.LogWarning("Bad request");
                //bad request-400-client side error
                return BadRequest();
            }

            var Student = await _dbContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (Student == null)
            {
                _Logger.LogError("Student not found with given id");
                return NotFound("Student not found");
            }

            var studentDTO = new StudentDTO
            {
                Id = Student.Id,
                Studentname = Student.Studentname,
                Email = Student.Email,
                Address = Student.Address,
                DOB = Student.DOB.ToShortDateString()
            };

            return Ok(studentDTO);



        }


        //Get  single student details by Name
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentByNameAsync(string name)
        {




            if (string.IsNullOrEmpty(name))
            {
                //bad request-400-client side error
                return BadRequest();
            }

            var Student = await _dbContext.Students
                .Where(x => x.Studentname.ToLower().Contains(name.ToLower()))
                .FirstOrDefaultAsync();

            if (Student == null)
            {
                return NotFound("Student not found");
            }
            var studentDTO = new StudentDTO
            {
                Id = Student.Id,
                Studentname = Student.Studentname,
                Email = Student.Email,
                Address = Student.Address,
                DOB = Student.DOB.ToShortDateString()
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
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody]StudentDTO model)
        {
            if (model == null) 
            {
                return BadRequest();
            }

            

            Student student = new Student
            {
                
                Studentname = model.Studentname,
                Email = model.Email,
                Address = model.Address,
                 DOB = Convert.ToDateTime(model.DOB),
            };

          await   _dbContext.Students.AddAsync(student);
           await _dbContext.SaveChangesAsync();

            model.Id = student.Id;
            //link/location of newly created data
            //status code=201
            return CreatedAtRoute("GetStudentByid", new {id=model.Id},model);
            

        }


        //update student api
        [HttpPut]
        [Route("Update")]
        //api/student/update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudentAsync([FromBody] StudentDTO model)
        {
            if(model==null || model.Id <= 0)
            {
                return BadRequest();
            }

            var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == model.Id).FirstOrDefaultAsync();

            if (existingStudent == null)
            {
                return NotFound();
            }

            //existingStudent.Studentname = model.Studentname;
            //existingStudent.Email = model.Email;
            //existingStudent.Address = model.Address;
            //existingStudent.DOB = Convert.ToDateTime(model.DOB);



            //using asnotracking
            var newrecord = new Student()
            {
                Id = existingStudent.Id,
                Studentname = model.Studentname,
                Email = model.Email,
                Address = model.Address,
                DOB = Convert.ToDateTime(model.DOB),
            };

            _dbContext.Students.Update(newrecord);

           await _dbContext.SaveChangesAsync();

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
        public async Task<ActionResult> UpdateStudentpartial(int id,[FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                return BadRequest();
            }

            var existingStudent =await _dbContext.Students.Where(s => s.Id == id).FirstOrDefaultAsync();

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
                DOB= existingStudent.DOB.ToShortDateString()
            };

            patchDocument.ApplyTo(studentDTO,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            existingStudent.Studentname = studentDTO.Studentname;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;
            existingStudent.DOB = Convert.ToDateTime(studentDTO.DOB); ;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }




        //delete  single student details by id
        [HttpDelete("{id}", Name = "DeleteStudentById")]
        //api/student/delete/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudentByIdAsync(int id)
        {
            

            if (id <= 0)
            {
                //bad request-400-client side error
                return BadRequest();
            }

            var Student = await _dbContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (Student == null)
            {
                return NotFound("Student id not found");
            }
            else
            {
                _dbContext.Students.Remove(Student);
              await  _dbContext.SaveChangesAsync();
                return Ok(true);
            }

        }
    }
}
