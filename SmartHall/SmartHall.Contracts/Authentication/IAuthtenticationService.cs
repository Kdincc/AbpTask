using Microsoft.AspNetCore.Identity;
using SmartHall.Common.Authentication.Models;

namespace SmartHall.Common.Authentication
{
    public interface IAuthenticationService
    {
        public Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken);

        public Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken);

        public Task<IdentityUser> FindByEmail(string email, CancellationToken cancellationToken);
    }
}
