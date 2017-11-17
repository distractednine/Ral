using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RAL.Models
{
    public class UserProfileViewModel
    {
        public uint id { get; set; }

        [Required(ErrorMessage = "Please enter your login")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Login length must be between 6 and 16 characters")]
        public string login { get; set; }

        [Required(ErrorMessage = "Please enter your login")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 16 characters")]
        public string password { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Invalid email")]
        public string email { get; set; }

        public DateTime regDate { get; set; }
    }
}