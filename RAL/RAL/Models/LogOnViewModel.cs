using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RAL.Models
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "Please enter your login")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Login length must be between 6 and 16 characters")]
        public string login { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 16 characters")]
        public string password { get; set; }

        public bool rememberme { get; set; }
    }
}