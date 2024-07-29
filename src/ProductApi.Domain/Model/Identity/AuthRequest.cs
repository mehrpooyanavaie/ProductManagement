using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Domain.Models.Identity
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
