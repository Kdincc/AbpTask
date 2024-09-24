using Microsoft.AspNetCore.Identity;

namespace SmartHall.Application.Authentication
{
	public interface IJwtTokenGenerator
	{
		public string GenerateToken(IdentityUser user);
	}
}
