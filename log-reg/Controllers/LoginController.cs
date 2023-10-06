using log_reg.Models;
using Microsoft.AspNetCore.Mvc;

namespace log_reg.Controllers
{
    public class LoginController : Controller
    {
        // public IActionResult Index()
        // {
        //     return View();
        // }

        private readonly UsersContext _context;
        public LoginController(UsersContext context)
        {
            _context = context;
        }
        public IActionResult Login(string name, string password)
        {
            var user = _context.UsersObjects.FirstOrDefault(u => u.Username == name);

            if (user == null)
            {
                return RedirectToAction("DisplayUser", "Home");
                // if (user.Password == password)
                // {
                //     return View("DisplayUser", "Home");
                // }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
