using Azure.Identity;
using log_reg.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace log_reg.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Get the connection string from the configuration file.
            var connectionString = "Server=localhost;Database=CloudFive;Trusted_Connection=True;TrustServerCertificate=True;";

            // Create a new SqlConnection object.
            using (var connection = new SqlConnection(connectionString))
            {
                // Open the connection.
                connection.Open();

                // Create a new SqlCommand object.
                var command = new SqlCommand($"SELECT * FROM UsersObjects WHERE Username = '{username}' AND Password = '{password}'", connection);

                // Execute the command and get the results.
                var reader = command.ExecuteReader();

                // If there is a row in the results, then the login is successful.
                if (reader.Read())
                {
                    // Get the user data from the results.
                    var userData = new { Username = reader["Username"], Id = reader["Id"], Password = reader["Password"] };

                    // Pass the user data to the view.
                    return View("~/Views/Home/DisplayUser.cshtml", userData);
                }
                else
                {
                    // The login failed.
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}