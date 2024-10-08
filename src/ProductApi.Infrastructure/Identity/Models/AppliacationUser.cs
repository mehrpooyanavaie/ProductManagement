using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
namespace ProductApi.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? TokenExpiryDate { get; set; }
    }
}