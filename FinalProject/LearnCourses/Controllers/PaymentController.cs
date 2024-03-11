using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
