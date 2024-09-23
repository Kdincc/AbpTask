using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartHall.Application.Authentication;
using SmartHall.Contracts.Authentication;
using SmartHall.Contracts.Common.ApiRoutes;
using SmartHall.Mappings;

namespace SmartHall.Controllers
{
	public class AuthenticationController : ApiController
	{
		private readonly IValidator<RegisterRequest> _registerRequestValidator;
		private readonly IAuthenticationService _authenticationService;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationController(IAuthenticationService authenticationService, IJwtTokenGenerator jwtTokenGenerator, IValidator<RegisterRequest> registerValidator)
        {
			_registerRequestValidator = registerValidator;
            _authenticationService = authenticationService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost(AuthenticationControllerRoutes.Register)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
		{
			var validationResult = await _registerRequestValidator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				return Problem(validationResult.Errors.ToErrors());
			}

			var result = await _authenticationService.Register(request, cancellationToken);

			if (!result.Result.Succeeded)
			{
				return Problem(result.Result.Errors.ToErrors());
			}

			return Ok();
		}

		[HttpPost(AuthenticationControllerRoutes.Login)]
		[ProducesResponseType<UnauthorizedResult>(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.Login(request, cancellationToken);

			if (!result.Result.Succeeded)
			{
				return Unauthorized("Invalid login or password!")
			}

			var user = await _authenticationService.FindByEmail(request.Email, cancellationToken);

			var token = _jwtTokenGenerator.GenerateToken(user);

			return Ok(new { token });
		}
    }
}
