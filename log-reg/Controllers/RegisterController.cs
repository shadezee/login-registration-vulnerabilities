using log_reg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace log_reg.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UsersContext _context;

        public RegisterController(UsersContext context)
        {
            _context = context;
        }
        public IActionResult RegisterUser(Users user_model)
        {
            if (ModelState.IsValid)
            {
                _context.UsersObjects.Add(user_model);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user_model);
        }
    }
}
