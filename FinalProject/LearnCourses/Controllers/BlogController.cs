using BusinessObject.Model;
using DataAccess.Repository.DisscussRepo;
using DataAccess.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;
using System.Xml;

namespace LearnCourses.Controllers
{
    public class BlogController : Controller
    {
        public IUserRepository userRepository = new UserRepsitory();
        public IDisscussRepository disscussRepository = new DisscussRepository();
        public IActionResult BlogSingle()
        {

            int UserId = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));
            if (UserId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            User user = userRepository.GetUserById(UserId);
            ViewBag.User = user;
            List<Discussion> discussions = disscussRepository.getDiscussByUserId(UserId);
            foreach (Discussion dis in discussions)
            {
                dis.Content = StripHtml(dis.Content);
            }
            ViewBag.Discussions = discussions;
            return View(user);
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

            int UserId = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));
            Discussion discussion = new Discussion()
            {
                Content = discuss.Content,
                Title = discuss.Title,
                UserId = UserId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            disscussRepository.AddDisscuss(discussion);
            return RedirectToAction("BlogSingle");
        }
        public string StripHtml(string htmlContent)
        {
            // Replace <br> tags with newlines
            htmlContent = htmlContent.Replace("<br>", "\n");

            // Remove all other HTML tags
            string plainText = Regex.Replace(htmlContent, "<.*?>", string.Empty);

            // Decode HTML entities if any
            plainText = System.Net.WebUtility.HtmlDecode(plainText);

            return plainText;
        }
        public IActionResult DeleteBlog(int id)
        {
            disscussRepository.DeleteDisscuss(id);
            return RedirectToAction("BlogSingle");
        }
        public IActionResult BlogPublic()
        {
            int UserId = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));
            if (UserId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            List<Discussion> d = disscussRepository.getNameOfUserDiscuss();
            ViewBag.User = UserId;
            foreach (Discussion dis in d)
            {
                dis.Content = StripHtml(dis.Content);
            }
            ViewBag.Discussions = d;
            return View();
        }
        public IActionResult UpdateBlog(int id)
        {
            if (SessionExtensions.GetString(HttpContext.Session, "id") == null)
            {
                return RedirectToAction("Login", "Users");
            }

            Discussion discuss = disscussRepository.getDisscussById(id);
            if (discuss == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            return View(discuss);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateBlog(Discussion discuss)
        {
            if (discuss == null)
            {
                return RedirectToAction("HomePage", "Home");
            }
            try
            {
                if (disscussRepository.UpdateDisscuss(discuss))
                {
                    return RedirectToAction("BlogSingle");
                }
                else
                {
                    return RedirectToAction("HomePage", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

    }
}
