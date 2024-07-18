using System.ComponentModel.DataAnnotations;
namespace ProductApi.Api.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}