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
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService teacherService;

        public TeacherController(TeacherService tService)
        {
           teacherService = tService;
        }
        // GET: TeacherController
        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> GetTeachers() 
        {
            var teachers = teacherService.GetAllTeachers();
            return Ok(teachers);
        }

        // GET: TeacherController/Details/5
        [HttpGet("{id}")]
        public ActionResult GetTeacherById(int id)
        {
            var teacher = teacherService.GetTeacherByID(id);
            if (teacher == null)

                return NotFound();
            return Ok(teacher);
        }

        // GET: TeacherController/Create
        [HttpPost]
        public ActionResult<Teacher> InsertTeacher([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            teacherService.InsertTeacher(teacher);
            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.TeacherID }, teacher);

        }


        // GET: TeacherController/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.TeacherID)
                return BadRequest();

            teacherService.EditTeacher(teacher);
            return NoContent();
        }



        // POST: TeacherController/Delete/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            teacherService.DeleteTeacher(id);
            return NoContent();
        }
    }
}
