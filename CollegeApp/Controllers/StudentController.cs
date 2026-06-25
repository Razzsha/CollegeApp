using CollegeApp.Models;
using CollegeApp.MyLoggin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMyLogger _myLogger;
        public StudentController(IMyLogger myLogger)
        {
            _myLogger = myLogger;
        }

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

        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
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

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.id <= 0)
                return BadRequest("Invalid student data");

            var existingStudent = CollegeRepo.Students.FirstOrDefault(s => s.id == model.id);

            if (existingStudent == null)
                return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest("Invalid student data");

            var existingStudent = CollegeRepo.Students.FirstOrDefault(s => s.id == id);

            if (existingStudent == null)
                return NotFound();

            var studentDTO = new StudentDTO
            {
                id = existingStudent.id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address
            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;

            return NoContent();
        }

        [HttpDelete("Delete/{id:int}", Name = "DeleteStudentById")]
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
