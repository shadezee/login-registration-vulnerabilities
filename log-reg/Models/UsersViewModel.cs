﻿using System.ComponentModel.DataAnnotations;

namespace log_reg.Models
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please enter contact")]
        public int Contact { get; set; }
        public int HasProfileImage { get; set; }
        public int NoteId;
    }
}
