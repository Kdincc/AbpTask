using Microsoft.AspNetCore.Identity;

namespace SmartHall.Common.Authentication.Models
{
    public record LoginResponse(SignInResult Result);
}
