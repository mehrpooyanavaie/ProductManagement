using ProductApi.Application.Models.Messaging;
namespace ProductApi.Application.Interfaces.Messaging
{
    public interface IRabbitMqService
    {
        void SendVerificationToken(VerificationToken token);
    }
}