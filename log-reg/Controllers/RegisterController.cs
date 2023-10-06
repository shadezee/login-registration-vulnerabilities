using log_reg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace log_reg.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Register(Users user_model)
        {
            if (ModelState.IsValid){
                // inserting a user
                var optionsBuilder = new DbContextOptionsBuilder<UsersContext>();
                optionsBuilder.UseSqlServer("Server=localhost;Database=CloudFive;Trusted_Connection=True;TrustServerCertificate=True;");
                
                var options =  optionsBuilder.Options;
                // options.UseSqlServer("Server=localhost;Database=CloudFive;Trusted_Connection=True;TrustServerCertificate=True;");
                using (var context = new UsersContext(options)){
                    context.UsersObjects.Add(user_model);
                    context.SaveChanges();

                    // Check if the new user record was successfully inserted into the database.
                    if (context.SaveChanges() > 0)
                    {
                        // Redirect the user to the new page.
                        return RedirectToAction("Index", "Home");
                    }
                    else{
                        return RedirectToAction("DbStatus", "Home");
                    }
                }

                return RedirectToAction("DbStatus", "Home");
            }
            return View(user_model);
        }
    }
}
