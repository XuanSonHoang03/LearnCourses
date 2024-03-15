using BusinessObject.Model;
using DataAccess.Repository.CourseRepo;
using DataAccess.Repository.EnrollmentRepo;
using DataAccess.Repository.LessonRepo;
using DataAccess.Repository.OrderRepo;
using DataAccess.Repository.RatingRepo;
using DataAccess.Repository.TransactionRepo;
using DataAccess.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class CoursesController : Controller
    {
        public ITransactionHistoryRepository transactionHistoryRepository = new TransactionHistoryRepository();
        public IUserRepository userRepository = new UserRepsitory();
        public IEnrollmentRepository enrollmentRepository = new EnrollmentRepository();
        public IRatingRepository ratingRepository = new RatingRepository();
        public ICourseRepository courseRepository = new CourseRepository(); 
        public ILessonRepostory lessonRepository = new LessonRepository();
        public IOrderRepository orderRepository = new OrderRepository();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Courses()
        {
            checkSession();
            List<Course> listCourses = courseRepository.GetCourses().ToList();
            ViewBag.Courses = listCourses;
            return View();
        }
        public IActionResult CourseDetails(int id)
        {
            checkSession();
            if (id == 0)
            {
                return RedirectToAction("Courses", "Courses");
            }
            //list comment of course 
            List<Rating> listRating = ratingRepository.GetRattingByCourseIdHaveName(id);
            //get lesson by course id
            List<Lesson> listLessonByCourseID = lessonRepository.GetLessonByCourseId(id);
            //get couse by id
            List<Course> course = courseRepository.GetCourseById(id);
            //viewbag to cshtml
            ViewBag.Comments = listRating;
            ViewBag.Lessons = listLessonByCourseID;
            ViewBag.Course = course;
            return View();
        }
        public IActionResult Cart()
        {
            checkSession();
            int id = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));
            if (id == 0)
            {
                return RedirectToAction("Courses", "Courses");
            }

            List<Course> listCourses = courseRepository.GetCoursesByUserID(id);
            ViewBag.Courses = listCourses;
            return View();
        }
        public IActionResult AddToCart(int id)
        {
            checkSession();
            if (id != null)
            {
                var userIdString = SessionExtensions.GetString(HttpContext.Session, "id");
                if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    TempData["Error"] = "User ID not found in session or invalid";
                    return RedirectToAction("CourseDetails", "Courses");
                }

                try
                {
                    orderRepository.AddOrder(new Order
                    {
                        CourseId = id,
                        UserId = userId,
                        CreatedDate = DateTime.Now
                    });
                    TempData["Success"] = "Add to cart successful";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Failed to add to cart: " + ex.Message;
                    // Log the inner exception for further investigation
                    if (ex.InnerException != null)
                    {
                        // Log the inner exception message
                        // Replace the following line with your logging mechanism
                        // logger.LogError("Inner Exception: " + ex.InnerException.Message);
                    }
                }

                return RedirectToAction("CourseDetails", "Courses");
            }

            TempData["Error"] = "Add to cart failed";
            return RedirectToAction("CourseDetails", "Courses");
        }
        public IActionResult comment(int idCourse, string comment)
        {
            checkSession();
            if (idCourse == 0 && comment == null)
            {
                return RedirectToAction("Courses", "Courses");
            }

            int UserId = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));
            Rating rating = new Rating()
            {
                Comment = comment,
                CourseId = idCourse,
                UserId = UserId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            ratingRepository.AddRating(rating);
            return RedirectToAction("CourseDetails", "Courses", new { id = idCourse });
        }
        public void checkSession()
        {
            if (SessionExtensions.GetString(HttpContext.Session, "username") == null)
            {
                RedirectToAction("Login", "Users");
            }
        }

        public IActionResult CheckOut(decimal priceTotal, string[] selectedItems)
        {
            if (priceTotal <= 0)
            {
                TempData["Message"] = "Value of cart is not valid. Pls check again";
                return RedirectToAction("PaymentFailed", "Checkout");
            }
            int UserId = int.Parse(SessionExtensions.GetString(HttpContext.Session, "id"));
            User user = userRepository.GetUserById(UserId);

            if (user.Balance < priceTotal) 
            {
                TempData["Message"] = "Not enough balance. Pls deposit more to buy it";
                return RedirectToAction("PaymentFailed", "Checkout");
            }
            else
            {
                //add enrollment
                foreach (var id in selectedItems)
                {
                    var enrollment = new Enrollment()
                    {
                        CourseId = int.Parse(id),
                        UserId = UserId,
                        CreatedDate = DateTime.Now,
                        CompletedDate = null,
                        Progress = 0,
                        UpdatedDate = DateTime.Now
                    };
                    enrollmentRepository.AddEnrollment(enrollment);
                }

                //add tracsaction history
                foreach (var id in selectedItems)
                {
                    var transactionHistory = new TransactionsHistory()
                    {
                        UserId = UserId,
                        CourseId = int.Parse(id),
                        Total = priceTotal,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    transactionHistoryRepository.AddTransactionHistory(transactionHistory);
                }

                //delete in order: cart
                foreach (var id in selectedItems)
                {
                    var order = orderRepository.GetOrdersByUserID(UserId).FirstOrDefault(x => x.CourseId == int.Parse(id));
                    orderRepository.DeleteOrder(order);
                }

                //update user balance
                user.Balance -= priceTotal;
                userRepository.UpdateUser(user);
                TempData["Message"] = "Thanh toán thành công";
                return RedirectToAction("PaymentSuccess", "Checkout");
            }
        }
    }
}
