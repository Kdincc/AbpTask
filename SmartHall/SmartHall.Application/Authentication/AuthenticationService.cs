using Microsoft.AspNetCore.Identity;
using SmartHall.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Authentication
{
	public sealed class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
        }

		public Task<IdentityUser> FindByEmail(string email, CancellationToken cancellationToken)
		{
			return _userManager.FindByEmailAsync(email);
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
