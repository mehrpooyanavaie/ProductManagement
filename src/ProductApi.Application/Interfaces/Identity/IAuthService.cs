using ProductApi.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.VM;
namespace ProductApi.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register (RegisterationRequest request);
        Task Verification(VerificationVM verificationVM);
        Task RegenerateVerificationToken(AuthRequest request) ;
    }
}