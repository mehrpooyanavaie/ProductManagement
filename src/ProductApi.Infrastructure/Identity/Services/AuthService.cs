using ProductApi.Application.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Infrastructure.Identity.Models;
using ProductApi.Application.Interfaces.Identity;
using ProductApi.Application.Models.Identity;
using ProductApi.Application.Interfaces.Messaging;
using ProductApi.Application.Models.Messaging;
using ProductApi.Application.VM;

namespace ProductApi.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRabbitMqService _rabbitMqService;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager, IRabbitMqService rabbitMqService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _rabbitMqService = rabbitMqService;
        }


        #region Register
        public async Task<RegistrationResponse> Register(RegisterationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception($"user name '{request.UserName}' already exists.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = false,
                VerificationToken = Guid.NewGuid().ToString(),
                TokenExpiryDate = DateTime.UtcNow.AddMinutes(5)
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "karbar");
                    await SendToConsole(user);
                    return new RegistrationResponse()
                    {
                        UserId = user.Id
                    };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }

            }
            else
            {
                throw new Exception($"Email '{request.Email}' already exists.");
            }
        }
        #endregion
        private async Task SendToConsole(ApplicationUser applicationUser)
        {
            var verificationToken = new VerificationToken
            {
                Email = applicationUser.Email,
                Token = applicationUser.VerificationToken,
                ExpiryDate = applicationUser.TokenExpiryDate.Value
            };
            _rabbitMqService.SendVerificationToken(verificationToken);

        }
        public async Task Verification(VerificationVM verificationVM)
        {
            var user = await _userManager.FindByEmailAsync(verificationVM.Email);
            if (user != null)
            {
                if (user.EmailConfirmed == false)
                {
                    if (user.VerificationToken == verificationVM.Token)
                    {
                        if (user.TokenExpiryDate > DateTime.UtcNow)
                        {
                            user.EmailConfirmed = true;
                            user.VerificationToken = null;
                            user.TokenExpiryDate = null;
                            await _userManager.UpdateAsync(user);
                            Console.WriteLine($"User {user.Email} is now verified.");
                        }
                        else
                        {
                            throw new Exception($"Token for {user.Email} has expired.");
                        }

                    }
                    else
                    {
                        throw new Exception($"Invalid token for {user.Email}");
                    }
                }
                else
                {
                    throw new Exception($"{verificationVM.Email} has already been verified");
                }
            }
            else
            {
                throw new Exception($"{verificationVM.Email} was not found");
            }
        }
        public async Task RegenerateVerificationToken(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                if (user.EmailConfirmed == false)
                {
                    user.VerificationToken = Guid.NewGuid().ToString();
                    user.TokenExpiryDate = DateTime.UtcNow.AddMinutes(5);
                    await _userManager.UpdateAsync(user);
                    await SendToConsole(user);
                }
                else
                {

                    throw new Exception($"{request.Email} has already been verified");

                }
            }
            else
            {
                throw new Exception($"user with {request.Email} not found.");
            }
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"user with {request.Email} not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"credentials for {request.Email} arent valid.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthResponse response = new AuthResponse()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
            };

            return response;

        }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(CustomClaimTypes.Uid,user.Id),
                new Claim(CustomClaimTypes.EmailVerified, user.EmailConfirmed.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
