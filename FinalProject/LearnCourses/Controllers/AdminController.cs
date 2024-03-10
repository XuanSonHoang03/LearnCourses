using BusinessObject.Model;
using DataAccess.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class AdminController : Controller
    {
        public IUserRepository userRepository = new UserRepsitory();
        public List<User> users = null;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserManage()
        {
            try
            {
                users = userRepository.GetUsers().ToList();
                ViewBag.Users = users;
            } catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(users);
        }
    }
}
