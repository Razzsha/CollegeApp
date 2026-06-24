using CollegeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetALLStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {

            var students = CollegeRepo.Students.Select(s => new StudentDTO()
            {
                id = s.id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address
            });
            return Ok(CollegeRepo.Students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid student id");

            var student = CollegeRepo.Students
                                      .FirstOrDefault(n => n.id == id);

            if (student == null)
                return NotFound($"The student with id {id} not found");

            var studentDTO = new StudentDTO()
            {
                id = student.id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };

            return Ok(studentDTO);
        }

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            var student = CollegeRepo.Students
                                      .FirstOrDefault(n => n.StudentName == name);

            if (student == null)
                return NotFound($"Student with name {name} not found");

            var studentDTO = new StudentDTO
            {
                id = student.id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };

            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if (model == null)
                return BadRequest();
            int newId = CollegeRepo.Students.LastOrDefault().id + 1;
            Student student = new Student
            {
                id = newId,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address
            };
            CollegeRepo.Students.Add(student);
            model.id = student.id;
            return CreatedAtRoute("GetStudentById", new { id = model.id }, model);
        }       

        [HttpDelete("{id:int}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStudent(int id)
        {
            var student = CollegeRepo.Students
                                      .FirstOrDefault(n => n.id == id);

            if (student == null)
                return NotFound($"Student with id {id} not found");

            CollegeRepo.Students.Remove(student);
            return Ok("Student deleted successfully");
        }
    }
}