using CollegeApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDTO>> GetSudents()
        {
            var students = CollegeRepository.Students.Select(n => new StudentDTO()
            { 
                Id = n.Id,
                Name = n.Name,
                Address = n.Address,
                Email = n.Email
            });

            //OK - 200 - Success
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByID(int id) 
        {
            //BadRequest - 400 - BadRequest - ClientError
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            //NotFound - 404 - NotFound - ClientError
            if (student == null)
                return NotFound($"The student with id {id} not found");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                Email = student.Email
            };

            //OK - 200 - Success
            return Ok(studentDTO);
        }

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentByName(string name)
        {
            var student = CollegeRepository.Students.Where(n => n.Name == name).FirstOrDefault();

            //BadRequest - 400 - BadRequest - ClientError
            if (string.IsNullOrEmpty(name))
                return BadRequest();


            //NotFound - 404 - NotFound - ClientError
            if (student == null)
                return NotFound($"The student with name {name} not found");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                Email = student.Email
            };

            //OK - 200 - Success
            return Ok(student);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {

            if(model == null)
                return BadRequest();

            if (model.AdmissionDate < DateTime.Now)
            {
                ModelState.AddModelError("Admission error", "Must be greater");
                return BadRequest(ModelState);
            }

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;

            Student student = new Student
            {
                Id = newId,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email
            };

            CollegeRepository.Students.Add(student);

            model.Id = student.Id;

            //Status - 201
            return CreatedAtRoute("GetStudentById", new { id = model.Id}, model);
        }



        [HttpDelete("{id:min(1):max(100)}", Name = "DeleteStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            //BadRequest - 400 - BadRequest - ClientError
            if (id <= 0)
                return BadRequest();

            var studentToDelete = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            //NotFound - 404 - NotFound - ClientError
            if (studentToDelete == null)
                return NotFound($"The student with id {id} not found");

            CollegeRepository.Students.Remove(studentToDelete);

            //OK - 200 - Success
            return Ok(true);
        }
    }
}
