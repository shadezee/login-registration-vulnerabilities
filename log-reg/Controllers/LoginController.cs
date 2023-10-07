using Azure.Identity;
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

        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            // var user = _context.UsersObjects.FirstOrDefault(u => u.Username == name && u.Password == password);
            var loginQuery = _context.UsersObjects.Where(u => u.Username == name && u.Password == password);

            List<string> usernames = loginQuery.Select(u => u.Username).ToList();

            var user = loginQuery.FirstOrDefault();


            if (user == null)
            {
                return BadRequest(usernames); ;
                if (user.Password == password)
                {
                    // return View("DisplayUser", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
