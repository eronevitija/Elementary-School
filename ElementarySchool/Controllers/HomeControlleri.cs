using Microsoft.AspNetCore.Mvc;
using ElementarySchool.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ElementarySchool.Services;

namespace ElementarySchool.Controllers
{
    // Cursor AI: I am getting a 404 error when calling '/api/home'. Can you help me identify why it's not found?


    public class HomeControlleri : Controller
    {
        private readonly StudentService studentService;
        public HomeControlleri(StudentService stService) 
        {
            studentService = stService;
        }
        
        [HttpPost]
        public ActionResult Index(Student st)
        {
            return View(st);
        }



       
    }
}
