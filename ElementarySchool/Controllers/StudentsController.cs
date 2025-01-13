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
    public class StudentsController : Controller
    {

        private readonly StudentService studentService;

        public StudentsController(StudentService stService)
        {
            studentService = stService;
        }
        // GET: StudentsController
        public ActionResult Index()
        {
            return View(studentService.GetAllStudents());
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Student st)
        {
            if (ModelState.IsValid)
            {
                studentService.InsertStudent(st);
                return RedirectToAction("Index");
            }
            return View(st);
        }

        // GET: StudentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(studentService.GetStudentByID(id));
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Student st)
        {
            try
            {
                studentService.EditStudent(st);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(studentService.GetStudentByID(id));
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                studentService.DeleteStudent(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
