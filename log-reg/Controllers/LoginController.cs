using log_reg.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace log_reg.Controllers
{
    // [Authorize]
    public class LoginController : Controller
    {
        private readonly UsersContext _context;
        public LoginController(UsersContext context)
        {
            _context = context;
        }

        public async Task Bakery(string name)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,  name),
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(20))
            });
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Console.WriteLine("eNTERED lOGIN");
            var user = _context.UsersObjects.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Console.WriteLine("User not null");

                var user_id = user.Id;
                var name = user.Username;
                var viewModel = new UsersViewModel
                {
                    HasProfileImage = user.HasProfileImage,
                    Id = user_id,
                    Username = name
                };

                Console.WriteLine("set View model");
                HttpContext.Session.SetString("UserId", Convert.ToString(user_id));
                HttpContext.Session.SetString("Password", Convert.ToString(password));
                HttpContext.Session.SetString("Username", Convert.ToString(name));

                Console.WriteLine("Run Bakery");
                _ = Bakery(name);
                Console.WriteLine("Ran Bakery");

                return View("~/Views/Home/DisplayUser.cshtml", viewModel);
            }
            TempData["InvalidCredentials"] = "Invalid Credentials.";
            return RedirectToAction("Index", "Home");
        }
    }
}
