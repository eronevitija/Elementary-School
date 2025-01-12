using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ElementarySchool.Models;
using ElementarySchool.Services;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ElementarySchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly StudentService studentService;

        public StudentsController(StudentService stService)
        {
            studentService = stService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            var students = studentService.GetAllStudents();
            return Ok(students);
        }

        // GET: StudentsController
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = studentService.GetStudentByID(id);
            if (student == null)
            
                return NotFound();
            return Ok(student);
        }
        [HttpPost]
        public ActionResult <Student> InsertStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            
                return BadRequest(ModelState);

                studentService.InsertStudent(student);
                return CreatedAtAction(nameof(GetStudentById), new {id = student.StudentID}, student);
            
        }

        [HttpPut]
        public ActionResult Edit(int id, [FromBody] Student student)
        {
            if (id != student.StudentID)
                return BadRequest();

            studentService.EditStudent(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            studentService.DeleteStudent(id);
            return NoContent();
        }


    }
}
    