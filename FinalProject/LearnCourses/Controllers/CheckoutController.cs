using LearnCourses.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class CheckoutController : Controller
    {
        private IVnPayService _vnPayService;

        public CheckoutController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService ?? throw new ArgumentNullException(nameof(vnPayService));
        }
        public IActionResult CheckOut()
        {
            if(ModelState.IsValid)
            {
                var model = new Models.OrderInfo()
                {
                    Amount = 100000,
                    CreatedDate = DateTime.Now,
                    OrderId = 1
                };
                var paymentUrl = _vnPayService.CreatePayment(HttpContext, model);
                return Redirect(paymentUrl);
            }

            return View();
        }
        public IActionResult PaymentFailed()
        {
            return View();
        }
        public IActionResult PaymentSuccess()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult PaymentBack()
        {
            //why it 's not working copilot?
            var response  = _vnPayService.PaymentExecute(Request.Query);

            if(response == null || response.PayStatus != "00")
            {
                TempData["Message"] = "Thanh toán thất bại";
                return RedirectToAction("PaymentFailed");
            }

            //save to database

            TempData["Message"] = "Thanh toán thành công";
            return RedirectToAction("PaymentSuccess");
        }
    }
}
