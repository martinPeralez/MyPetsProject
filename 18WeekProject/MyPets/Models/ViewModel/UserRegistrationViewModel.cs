using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "First Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone Number Is Required")]
        public long PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address Is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "E-mail Is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

        [CompareAttribute("Password", ErrorMessage = "The Passwords Must Match.")]
        [Required(ErrorMessage = "Verification Password Required")]
        public string VerificationPassword { get; set; }
    }
}
