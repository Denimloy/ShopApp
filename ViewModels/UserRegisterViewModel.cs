using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public class UserRegisterViewModel
    {
        [Display(Name = "Your name")]
        [Required(ErrorMessage = "Enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your email.")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,4}$", ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your Password.")]
        [MinLength(6, ErrorMessage = "Passwords must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Re-enter password")]
        [Required(ErrorMessage = "Type your password again.")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
