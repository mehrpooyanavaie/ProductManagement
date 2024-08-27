using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.Models.Identity
{
    public class AuthRequest
    {
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
