using Microsoft.AspNetCore.Identity;

namespace SmartHall.Common.Authentication
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(IdentityUser user);
    }
}
