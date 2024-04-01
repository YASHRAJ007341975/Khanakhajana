using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Khanakhajana.Models
{
    public class Login
    {
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

}


