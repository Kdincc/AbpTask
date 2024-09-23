using Microsoft.AspNetCore.Identity;

namespace SmartHall.Contracts.Authentication
{
	public record RegisterResponse(IdentityResult Result);
}
