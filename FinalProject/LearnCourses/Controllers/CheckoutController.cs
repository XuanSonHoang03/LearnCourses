
using BusinessObject.Model;
using DataAccess.Repository.EnrollmentRepo;
using DataAccess.Repository.OrderRepo;
using DataAccess.Repository.TransactionRepo;
using DataAccess.Repository.UserRepo;
using LearnCourses.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class CheckoutController : Controller
    {
        private IVnPayService _vnPayService;

        public IOrderRepository OrderRepository = new OrderRepository();
        public IUserRepository userRepository = new UserRepsitory();
        public ITransactionHistoryRepository transactionHistoryRepository = new TransactionHistoryRepository();
        public IEnrollmentRepository enrollmentRepository = new EnrollmentRepository();

        public CheckoutController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService ?? throw new ArgumentNullException(nameof(vnPayService));
        }
        /*public IActionResult CheckOut(decimal price)
        {
            if (ModelState.IsValid)
            {
                var order = new Order()
                {
                    UserId = 1,
                    CourseId = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var model = new TransactionsHistory()
                {
                    UserId = 1,
                    CourseId = 1,
                    Total = price,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                var paymentUrl = _vnPayService.CreatePayment(HttpContext, model);
                return Redirect(paymentUrl);
            }

            return View();
        }*/
        public IActionResult CheckOut(decimal price, int[] ids)
        {
            if(price <= 0)
            {
                ViewData["Error"] = "Value of cart is not valid. Pls check again";
				return RedirectToAction("Cart", "Courses");
			}
            int UserId = int.Parse(SessionExtensions.GetString(HttpContext.Session, "id"));
            User user = userRepository.GetUserById(UserId);

            if(user.Balance < price)
            {
				ViewData["Error"] = "Not enough balance. Pls deposit more to buy it";
				return RedirectToAction("Cart", "Courses");
			} else
            {
                //add enrollment
                foreach (var id in ids)
                {
                    var enrollment = new Enrollment()
                    {
                        CourseId = id,
                        UserId = UserId,
                        CreatedDate = DateTime.Now,
                        CompletedDate = null,
                        Progress = 0,
                        UpdatedDate = DateTime.Now
                    };
                    enrollmentRepository.AddEnrollment(enrollment);
                }

                //add tracsaction history
                foreach(var id in ids)
                {
                    var transactionHistory = new TransactionsHistory()
                    {
                        UserId = UserId,
                        CourseId = id,
                        Total = price,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    transactionHistoryRepository.AddTransactionHistory(transactionHistory);
                }

                //delete in order: cart
                foreach(var id in ids)
                {
                    var order = OrderRepository.GetOrdersByUserID(UserId).FirstOrDefault(x => x.CourseId == id);
                    OrderRepository.DeleteOrder(order);
                }

                //update user balance
                user.Balance -= price;
                userRepository.UpdateUser(user);
                TempData["Message"] = "Thanh toán thành công";
                return RedirectToAction("PaymentSuccess");
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
