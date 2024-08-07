using CollegeApp.Models;
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
        public ActionResult<IEnumerable<Student>> GetSudents()
        {
            //OK - 200 - Success
            return Ok(CollegeRepository.Students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentByID(int id) 
        {
            //BadRequest - 400 - BadRequest - ClientError
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            //NotFound - 404 - NotFound - ClientError
            if (student == null)
                return NotFound($"The student with id {id} not found");

            //OK - 200 - Success
            return Ok(student);
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

            //OK - 200 - Success
            return Ok(student);
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
