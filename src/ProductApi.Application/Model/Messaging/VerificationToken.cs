using System.ComponentModel.DataAnnotations;
namespace ProductApi.Application.Models.Messaging
{
    public class VerificationToken
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
