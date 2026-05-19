using CollegeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetALLStudents")]
        public ActionResult<IEnumerable<Student>> GetStudent()
        {
            return Ok(CollegeRepo.Students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]

        public ActionResult<Student> GetStudentById(int id)
        {
            return Ok(CollegeRepo.Students.Where(n => n.id == id).FirstOrDefault());
        }

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        public Student GetStudentByName(string name)
        {
            return Ok(CollegeRepo.Students.Where(n => n.StudentName == name).FirstOrDefault());
        }

        [HttpDelete("{id:int}", Name = "DeleteStudentById")]
        public bool DeleteStudent(int id)
        {
            var student = CollegeRepo.Students.Where(n => n.id == id).FirstOrDefault();

            if (student == null)
                return false;

            CollegeRepo.Students.Remove(student);
            return true;
        }

    }
}
