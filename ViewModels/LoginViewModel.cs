using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your Password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
