using System.ComponentModel.DataAnnotations;
namespace ProductApi.Application.VM
{
    public class VerificationVM
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
