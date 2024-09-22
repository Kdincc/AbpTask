using Microsoft.AspNetCore.Mvc;
using SmartHall.Application.Authentication;
using SmartHall.Contracts.Authentication;
using SmartHall.Contracts.Common.ApiRoutes;

namespace SmartHall.Controllers
{
	public class AuthenticationController : ApiController
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationController(IAuthenticationService authenticationService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authenticationService = authenticationService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost(AuthenticationControllerRoutes.Register)]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.Register(request, cancellationToken);

			if (!result.Result.Succeeded)
			{
				return BadRequest(result.Result.Errors);
			}

			return Ok();
		}

		[HttpPost(AuthenticationControllerRoutes.Login)]
		public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.Login(request, cancellationToken);

			if (!result.Result.Succeeded)
			{
				return Unauthorized("Invalid login or password!");
			}

			var user = await _authenticationService.FindByEmail(request.Email, cancellationToken);

			var token = _jwtTokenGenerator.GenerateToken(user);

			return Ok(new { token });
		}
    }
}
