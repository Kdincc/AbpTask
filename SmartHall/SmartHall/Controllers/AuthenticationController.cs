using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartHall.Common.ApiRoutes;
using SmartHall.Common.Authentication;
using SmartHall.Common.Authentication.Models;
using SmartHall.Controllers;

namespace SmartHall.Service.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly IValidator<RegisterRequest> _registerRequestValidator;
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IJwtTokenGenerator jwtTokenGenerator, IValidator<RegisterRequest> registerValidator, IMapper mapper)
        {
            _mapper = mapper;
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
                return Problem(_mapper.Map<List<Error>>(validationResult.Errors));
            }

            var result = await _authenticationService.Register(request, cancellationToken);

            if (!result.Result.Succeeded)
            {
                return Problem(_mapper.Map<List<Error>>(result.Result.Errors));
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
                return Unauthorized("Invalid login or password!");
            }

            var user = await _authenticationService.FindByEmail(request.Email, cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
