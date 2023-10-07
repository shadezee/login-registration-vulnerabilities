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
                    Id = user_id,
                    Username = name
                };

                HttpContext.Session.SetString("UserId", Convert.ToString(user_id));
                HttpContext.Session.SetString("Password", Convert.ToString(password));
                HttpContext.Session.SetString("Username", Convert.ToString(name));

                return View("~/Views/Home/DisplayUser.cshtml", viewModel);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
