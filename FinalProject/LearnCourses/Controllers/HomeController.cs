using DataAccess.Repository.CourseRepo;
using LearnCourses.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessObject.Model;
using DataAccess.Repository.LessonRepo;
using DataAccess.Repository.UserRepo;
using DataAccess.Repository.OrderRepo;
using DataAccess.Repository.EnrollmentRepo;

namespace LearnCourses
{
    public class HomeController : Controller
    {
        LessonRepository lessonRepository = new LessonRepository();
        IEnrollmentRepository enrollmentRepository = new EnrollmentRepository();
        IUserRepository userRepository = new UserRepsitory();
        ICourseRepository courseRepository = new CourseRepository();

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public bool checkSession()
        {
            if (SessionExtensions.GetString(HttpContext.Session, "username") == null)
            {
                return true;
            }
            return false;
        }

        public IActionResult HomePage()
        {
            if(checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            List<Course> listCourses = courseRepository.GetCourses().ToList();
            ViewBag.Courses = listCourses;
            return View();
        }
        public IActionResult Profile()
        {
            if (checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            User user = userRepository.GetUserById(Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id")));
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(User user)
        {
            if (checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            if (user != null)
            {
                int id = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));

                user.Enrollments = userRepository.GetUserById(id).Enrollments;
                user.Password = userRepository.GetUserById(id).Password;
                user.Balance = userRepository.GetUserById(id).Balance;
                user.UpdatedDate = DateTime.Now;

                userRepository.UpdateUser(user);
                TempData["Success"] = "Update successfully";
                return RedirectToAction("Profile", "Home");
            }
            TempData["Error"] = "Update failed";
            return RedirectToAction("Profile", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            if (oldPassword != null && newPassword != null && confirmPassword != null)
            {
                if (newPassword.Equals(confirmPassword))
                {
                    User user = userRepository.GetUserById(Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id")));
                    if (user.Password.Equals(oldPassword))
                    {
                        user.Password = newPassword;
                        userRepository.UpdateUser(user);
                        TempData["Ok"] = "Change password successfully";
                        return RedirectToAction("Profile", "Home");
                    }
                    TempData["Fail"] = "Old password is incorrect";
                    return RedirectToAction("Profile", "Home");
                }
                TempData["Fail"] = "New password and confirm password are not the same";
                return RedirectToAction("Profile", "Home");
            }
            TempData["Fail"] = "Change password failed";
            return RedirectToAction("Profile", "Home");
        }
        public IActionResult Gallery()
        {
            int id = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));
            List<Course> listEnrollment = enrollmentRepository.GetAllCourseUserBuyed(id);
            ViewBag.Courses = listEnrollment;
            return View();
        }
        public IActionResult MyLesson(int idCourse)
        {
            checkSession();
            List<Lesson> lessonlist = lessonRepository.GetLessonByCourseId(idCourse);
            ViewBag.Lessons = lessonlist;
            return View();
        }
    }
}
