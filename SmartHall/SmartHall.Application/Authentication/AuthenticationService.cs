using Microsoft.AspNetCore.Identity;
using SmartHall.Common.Authentication;
using SmartHall.Common.Authentication.Models;

namespace SmartHall.BLL.Authentication
{
    /// <summary>
    /// Service for authentication operations
    /// </summary>
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityUser> FindByEmail(string email, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

            return new LoginResponse(result);
        }

        public async Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken)
        {

            var user = new IdentityUser { UserName = request.Email, Email = request.Email };

            var result = await _userManager.CreateAsync(user, request.Password);

            return new RegisterResponse(result);
        }
    }
}
