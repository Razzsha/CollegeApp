using CollegeApp.Models;
using CollegeApp.MyLoggin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
       private readonly ILogger<StudentController> _logger;
        private readonly CollegeDBContext _dbContext;

        public StudentController(ILogger<StudentController> logger, CollegeDBContext dbContext)
        {
         _logger = logger;    
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("All", Name = "GetALLStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {
            _logger.LogInformation("Fetching all students");

            var students = _dbContext.Students.Select(s => new StudentDTO
            {
                id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address,
                DOB = s.DOB
            });

            return Ok(students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest("Invalid student id");
            }
                

            var student = _dbContext.Students
                                      .FirstOrDefault(n => n.Id == id);

            if (student == null)
            {
                _logger.LogError("Student not found with this Id");
                return NotFound($"The student with id {id} not found");
            }
               

            var studentDTO = new StudentDTO()
            {
                id = student.Id,
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
            var student = _dbContext.Students
                                      .FirstOrDefault(n => n.StudentName == name);

            if (student == null)
            {
                _logger.LogError("Student not found with this name");
                return NotFound($"Student with name {name} not found");
            }
               

            var studentDTO = new StudentDTO
            {
                id = student.Id,
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
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
                
            Student student = new Student
            {
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address
            };
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            model.id = student.Id;
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
            {
                _logger.LogWarning("Bad Request");
                return BadRequest("Invalid student data");
            }
                

            var existingStudent = _dbContext.Students.FirstOrDefault(s => s.Id == model.id);

            if (existingStudent == null)
            {
                _logger.LogError("Student not found with this Id");
                return NotFound();
            }
               

            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;
            _dbContext.SaveChanges();

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
            {
                _logger.LogWarning("Bad Request");
                return BadRequest("Invalid student data");
            }
                

            var existingStudent = _dbContext.Students.FirstOrDefault(s => s.Id == id);

            if (existingStudent == null)
            {
                _logger.LogError("Student not found with this Id");
                return NotFound();
            }

            var studentDTO = new StudentDTO
            {
                id = existingStudent.Id,
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
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("Delete/{id:int}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStudent(int id)
        {
            var student = _dbContext.Students
                                      .FirstOrDefault(n => n.Id == id);

            if (student == null)
            {
                _logger.LogError("Student not found with this Id");
                return NotFound($"Student with id {id} not found");
            }
                
            _dbContext.Students.Remove(student);
            return Ok("Student deleted successfully");
        }
    }
}
