using System.Net;
using Azure.Identity;
using log_reg.Models;
using Microsoft.AspNetCore.Mvc;

namespace log_reg.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsersContext _context;
        public LoginController(UsersContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.UsersObjects.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {

                var user_id = user.Id;
                var name = user.Username;
                var viewModel = new UsersViewModel
                {
                    HasProfileImage = user.HasProfileImage,
                    Id = user_id,
                    Username = name
                };

                HttpContext.Session.SetString("UserId", Convert.ToString(user_id));
                HttpContext.Session.SetString("Password", Convert.ToString(password));
                HttpContext.Session.SetString("Username", Convert.ToString(name));

                var net_cookie = new Cookie("UserCookie", Convert.ToString(user_id));
                net_cookie.Expires = DateTime.UtcNow.AddDays(20);

                Response.Cookies.Append(net_cookie.Name, net_cookie.Value);

                return View("~/Views/Home/DisplayUser.cshtml", viewModel);
            }
            TempData["InvalidCredentials"] = "Invalid Credentials.";
            return RedirectToAction("Index", "Home");
        }
    }
}
