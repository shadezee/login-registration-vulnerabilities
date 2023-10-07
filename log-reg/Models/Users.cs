using System.ComponentModel.DataAnnotations;

namespace log_reg.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter contact")]
        public int Contact { get; set; }
        // [Required(ErrorMessage = "Please upload an image")]
        // public IFormFile ProfilePic { get; set; }
    }
}
