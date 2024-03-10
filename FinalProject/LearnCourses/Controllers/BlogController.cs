using BusinessObject.Model;
using DataAccess.Repository.DisscussRepo;
using Microsoft.AspNetCore.Mvc;

namespace LearnCourses.Controllers
{
    public class BlogController : Controller
    {
        public IDisscussRepository disscussRepository = new DisscussRepository();
        public IActionResult BlogSingle()
        {
            List<Discussion> discussions = disscussRepository.GetDisscusses();
            ViewBag.Discussions = discussions;
            return View();
        }
        public IActionResult NewBlog()
        {
            if (SessionExtensions.GetString(HttpContext.Session, "id") == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewBlog(Discussion discuss)
        {
            if (ModelState.IsValid)
            {
                int UserId = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));
                Discussion discussion = new Discussion()
                {
                    Content = discuss.Content,
                    Title = discuss.Title,
                    UserId = UserId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                if (disscussRepository.AddDisscuss(discussion))
                {
                    ViewBag.Success = "Add new blog success";
                }
                else
                {
                    ViewBag.Error = "Add new blog fail";
                }
            }
            else
            {
                ViewBag.Error = "Invalid data submitted";
            }

            return View(discuss);
        }

    }
}
