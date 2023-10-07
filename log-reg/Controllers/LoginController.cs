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
                return RedirectToAction("DisplayUser", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
