using ProductApi.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ProductApi.Application.Interfaces.Identity
{
    public interface IAuthService{
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register (RegisterationRequest request);
    }
}