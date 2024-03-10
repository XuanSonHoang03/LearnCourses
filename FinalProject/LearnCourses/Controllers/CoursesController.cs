using BusinessObject.Model;
using DataAccess.Repository.CourseRepo;
using DataAccess.Repository.LessonRepo;
using DataAccess.Repository.OrderRepo;
using DataAccess.Repository.RatingRepo;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class CoursesController : Controller
    {
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
    }
}
