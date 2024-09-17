using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SmartHall.Application.Halls.Services;
using SmartHall.Contracts.Common.ApiRoutes;
using SmartHall.Contracts.Halls.CreateHall;

namespace SmartHall.Controllers
{
	public sealed class HallsController : ApiController
	{
		private readonly IHallService _hallService;
		private readonly IValidator<CreateHallRequest> _createHallValidator;

		public HallsController(IHallService hallService, IValidator<CreateHallRequest> createHallValidator)
		{
			_createHallValidator = createHallValidator;
			_hallService = hallService;
		}

		[HttpPost(HallsControllerRoutes.CreateHall)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
		public async Task<IActionResult> CreateHall(CreateHallRequest request, CancellationToken cancellationToken)
		{
			var validationResult = await _createHallValidator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				return Problem(validationResult.Errors.Select(e => Error.Validation(code: e.ErrorCode, description: e.ErrorMessage)).ToList());
			}

			var result = await _hallService.CreateHall(request, cancellationToken);

			return result.Match(Ok, Problem);
		}
	}
}
