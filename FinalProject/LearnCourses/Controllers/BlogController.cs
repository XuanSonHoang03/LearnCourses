using BusinessObject.Model;
using DataAccess.Repository.DisscussRepo;
using DataAccess.Repository.UserDiscussRepo;
using DataAccess.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.RegularExpressions;
using System.Xml;

namespace LearnCourses.Controllers
{
    public class BlogController : Controller
    {
        public IUserDiscussRepository userDiscussRepository = new UserDiscussRepository();
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
        
        public IActionResult ViewBlog(int DiscussId)
        {
            if(SessionExtensions.GetString(HttpContext.Session, "id") == null)
            {
                return RedirectToAction("Login", "Users");
            }
            List<UserDissucss> listComment = userDiscussRepository.getAllCommentByDiscussId(DiscussId);
            Discussion discuss = disscussRepository.getDisscussById(DiscussId);

            if (discuss == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            discuss.Content = StripHtml(discuss.Content);

            ViewBag.Comment = listComment;

            return View(discuss);
        }
        public IActionResult commentBlog()
        {
            if (SessionExtensions.GetString(HttpContext.Session, "id") == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View("ViewBlog", "View");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult commentBlog(string content, int discussId)
        {
            int userId;
            if (!int.TryParse(SessionExtensions.GetString(HttpContext.Session, "id"), out userId))
            {
                return RedirectToAction("Login", "Users");
            }

            User user = userRepository.GetUserById(userId);

            if (user == null)
            {
                return RedirectToAction("Login", "Users");
            }

            UserDissucss userDiscuss = new UserDissucss()
            {
                Content = content,
                UserId = user.Id,
                DisscussId = discussId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            userDiscussRepository.AddUserDiscuss(userDiscuss);

            return RedirectToAction("ViewBlog", new { discussId = discussId });
        }

    }
}
