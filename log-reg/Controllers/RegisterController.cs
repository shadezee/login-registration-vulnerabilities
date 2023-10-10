using log_reg.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
        public static bool CheckIfPasswordValid(string password, string user_name)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                return false;
            }

            if (!Regex.IsMatch(password, "[a-z]"))
            {
                return false;
            }

            if (!Regex.IsMatch(password, "[0-9]"))
            {
                return false;
            }

            if (Regex.IsMatch(password, user_name, RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }
        public IActionResult RegisterUser(Users user_model)
        {
            if (ModelState.IsValid)
            {
                string email = user_model.Email;
                if (!CheckPreExisting(email))
                {
                    if (CheckIfPasswordValid(user_model.Password, user_model.Username))
                    {
                        _context.UsersObjects.Add(user_model);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["PasswordError"] = "The password should be at least 8 characters, contain one uppercase letter, one number and should not contain your name .";
                        return View("~/Views/Home/Register.cshtml");
                    }
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
