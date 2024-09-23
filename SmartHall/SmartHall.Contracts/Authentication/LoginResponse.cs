using Microsoft.AspNetCore.Identity;

namespace SmartHall.Contracts.Authentication
{
	public record LoginResponse(SignInResult Result);
}
