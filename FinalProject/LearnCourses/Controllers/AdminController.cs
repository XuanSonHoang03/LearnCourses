using BusinessObject.Model;
using DataAccess.Repository.CourseRepo;
using DataAccess.Repository.LessonRepo;
using DataAccess.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class AdminController : Controller
    {
        public ILessonRepostory lessonRepostory = new LessonRepository();
        public ICourseRepository courseRepository = new CourseRepository();
        public IUserRepository userRepository = new UserRepsitory();
        public List<User> users = null;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserManage()
        {
            if(SessionExtensions.GetString(HttpContext.Session, "id") == "admin")
            {
                try
                {
                    users = userRepository.GetUsers().ToList();
                    ViewBag.Users = users;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
        public IActionResult CourseManage()
        {
            if (ModelState.IsValid)
            {
                if (SessionExtensions.GetString(HttpContext.Session, "id") == "admin")
                {
                    List<Course> listAllCourse = courseRepository.GetCourses().ToList();
                    ViewBag.Courses = listAllCourse;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Users");
                }
            }
            return RedirectToAction("Login", "Users");

        }
        public IActionResult NewCourse()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewCourse(Course course)
        {
                Course newCourse = new Course()
                {
                    Content = course.Content,
                    Name = course.Name,
                    Price = course.Price,
                    ThumbnailImage = course.ThumbnailImage,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Description = course.Description,
                    IsPublished = true,
                    TeacherId = 1
                };
                courseRepository.AddCourse(newCourse);
                return RedirectToAction("CourseManage");
        }
        public IActionResult CourseDetails (int id)
        {
            List<Course> course = courseRepository.GetCourseById(id);
            List<Lesson> lessons = lessonRepostory.GetLessonByCourseId(id).ToList();
            ViewBag.Lessons = lessons;
            ViewBag.Course = course;
            return View();
        }
        [HttpPost]
        public IActionResult ManageLesson(string feature, Lesson lesson)
        {
            if (feature == "Add")
            {
                return RedirectToAction("NewLesson");
            }
            else if (feature == "Update")
            {
                lessonRepostory.UpdateLesson(lesson);
                ViewBag.Success = "Update successfully";
                return RedirectToAction("CourseDetails");
            }
            else if (feature == "Delete")
            {
                lessonRepostory.DeleteLesson(lesson);
                ViewBag.Success = "Delete successfully";
                return RedirectToAction("CourseDetails");
            }
            else
            {
                // Handle invalid feature value here
                return RedirectToAction("Error");
            }
        }

        public IActionResult NewLesson()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }
        public IActionResult BlockUser(int[] id, string feature)
        {
            if(id == null || feature == null)
            {
                return RedirectToAction("Login", "Users");
            }
            if(feature == "delete")
            {
                if (id != null)
                {
                    foreach (int i in id)
                    {
                        User user = userRepository.GetUserById(i);
                        user.IsDeleted = 1;
                        userRepository.UpdateUser(user);
                    }
                    ViewBag.Success = "Block successfully";
                    return RedirectToAction("UserManage");
                }
                else
                {
                    ViewBag.Error = "Block failed";
                    return RedirectToAction("UserManage");
                }
            }
            else if(feature == "open")
            {
                if (id != null)
                {
                    foreach (int i in id)
                    {
                        User user = userRepository.GetUserById(i);
                        user.IsDeleted = 0;
                        userRepository.UpdateUser(user);
                    }
                    ViewBag.Success = "Block successfully";
                    return RedirectToAction("UserManage");
                }
                else
                {
                    ViewBag.Error = "Block failed";
                    return RedirectToAction("UserManage");
                }
            }
            else
            {
                return RedirectToAction("CourseManage");
            }
        }
    }
}
