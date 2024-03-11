using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult CheckOut(int price, int[] courseId)
        {
            return View();
        }
        [Authorize]
        public IActionResult PaymentBack()
        {
            return View();
        }
    }
}
