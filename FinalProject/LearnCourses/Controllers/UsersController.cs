using BusinessObject.Model;
using DataAccess.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class UsersController : Controller
    {
        public IUserRepository userRepository = new UserRepsitory();
        private readonly ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        //login controller
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
                if(username == "admin@fstore.com" && password == "admin@@")
                {
                    SessionExtensions.SetString(HttpContext.Session, "id", "admin");
					return RedirectToAction("UserManage", "Admin");
				}

                User user = userRepository.GetUserByUserPass(username, password);
				if(user != null)
                {
                    if(user.IsDeleted == 1)
                    {
                        ViewBag.Error = "Your account has been blocked";
                        return View();
                    }
                    SessionExtensions.SetString(HttpContext.Session, "id", user.Id.ToString());
                    SessionExtensions.SetString(HttpContext.Session, "username", username);
					return RedirectToAction("HomePage", "Home");
				}
				else
                {
					ViewBag.Error = "Invalid username or password";
					return View();
				}
        }

        //register controller
        public IActionResult Register()
        {
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
				if(userRepository.GetUserByUserPass(user.Username, user.Password) == null)
                {
                    user.Balance = 0;
					userRepository.AddUser(user);
					return RedirectToAction("Login");
				}
				else
                {
					ModelState.AddModelError("","Username already exists");
                    ViewBag.Error = "Username already exists";
				    return View();
                }
			}
			return View();
		}
    }
}
