using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public class UserChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current Password Is Required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password Is Required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Verification Password Is Required")]
        [Compare("NewPassword", ErrorMessage = "The Passwords Must Match")]
        public string VerifyNewPassword { get; set; }
    }
}
