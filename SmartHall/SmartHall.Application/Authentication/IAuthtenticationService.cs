using Microsoft.AspNetCore.Identity;
using SmartHall.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Authentication
{
	public interface IAuthenticationService
	{
		public Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken);

		public Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken);
	}
}
