using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
