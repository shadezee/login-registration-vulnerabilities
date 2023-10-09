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
        public bool CheckPreExisting(string email)
        {
            bool existing = false;
            var user = _context.UsersObjects.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                existing = true;
            }
            return existing;
        }
        public IActionResult RegisterUser(Users user_model)
        {
            if (ModelState.IsValid)
            {
                string email = user_model.Email;
                if (!CheckPreExisting(email))
                {
                    _context.UsersObjects.Add(user_model);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["PreExistingUserError"] = "An account has been already registered to this email.";
                    return View("~/Views/Home/Register.cshtml");
                }
            }
            return View("~/Views/Home/Register.cshtml");
        }
    }
}
