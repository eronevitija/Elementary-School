using Microsoft.AspNetCore.Mvc;

namespace ElementarySchool.Controllers
{
    public class HomeControlleri : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
