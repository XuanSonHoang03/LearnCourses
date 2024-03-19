using BusinessObject.Model;
using DataAccess.Repository.CourseRepo;
using DataAccess.Repository.LessonRepo;
using DataAccess.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Text.RegularExpressions;

namespace LearnCourses.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ILessonRepostory lessonRepostory = new LessonRepository();
        public ICourseRepository courseRepository = new CourseRepository();
        public IUserRepository userRepository = new UserRepsitory();
        public List<User> users = null;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }
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
        public IActionResult NewCourse(Course course, IFormFile img)
        {
            //upload image
            course.ThumbnailImage = UpLoadImage(img);
            //end upload image

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

        private string UpLoadImage(IFormFile img)
        {
            string filename = "";
            string filePath = "";
            if (img != null)
            {
                filename = Path.GetFileName(img.FileName);
                string directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "home", "assets", "img");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                filePath = Path.Combine(directoryPath, filename);

                using (FileStream fileStream = System.IO.File.Create(filePath))
                {
                    img.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            return "../home/assets/img/" + filename;
        }



        public IActionResult CourseDetails (int id)
        {
            List<Course> course = courseRepository.GetCourseById(id);
            List<Lesson> lessons = lessonRepostory.GetLessonByCourseId(id).ToList();
            ViewBag.Lessons = lessons;
            ViewBag.Course = course;
            ViewBag.CourseId = id;
            return View();
        }
        [HttpPost]
        public IActionResult ManageLesson(string feature, int[] lessonId, int courseId)
        {
            if (feature == "Add")
            {
                return RedirectToAction("NewLesson", new {courseId = courseId});
            }
            else if (feature == "Update")
            {
                //lessonRepostory.UpdateLesson(lesson);
                ViewBag.Success = "Update successfully";
                return RedirectToAction("CourseDetails");
            }
            else if (feature == "Delete")
            {
                foreach(int i in lessonId)
                {
                    Lesson lesson = lessonRepostory.GetLessonById(i).FirstOrDefault();
                    lessonRepostory.DeleteLesson(lesson);
                }
                ViewBag.Success = "Delete successfully";
                return RedirectToAction("CourseDetails");
            }
            else
            {
                // Handle invalid feature value here
                return RedirectToAction("Error");
            }
        }
        public IActionResult NewLesson(int courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewLesson(Lesson lesson, int idCourse)
        {
            if (idCourse == null)
            {
                ViewBag.Error = "Add failed";
                return View(lesson);
            }
            lesson.CreatedDate = DateTime.Now;
            lesson.UpdatedDate = DateTime.Now;
            lesson.CourseId = idCourse;
            lesson.Order = 1;

            lessonRepostory.AddLesson(lesson);
            ViewBag.Success = "Add successfully";
            return RedirectToAction("CourseDetails", new {id = idCourse});
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
