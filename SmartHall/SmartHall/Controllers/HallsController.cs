using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SmartHall.Application.Halls.Services;
using SmartHall.Contracts.Common.ApiRoutes;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.GetFreeHall;
using SmartHall.Contracts.Halls.RemoveHall;
using SmartHall.Contracts.Halls.ReserveHall;
using SmartHall.Contracts.Halls.SearchFreeHall;
using SmartHall.Contracts.Halls.UpdateHall;
using SmartHall.Mappings;

namespace SmartHall.Controllers
{
	public sealed class HallsController : ApiController
	{
		private readonly IHallService _hallService;
		private readonly IValidator<CreateHallRequest> _createHallValidator;
		private readonly IValidator<RemoveHallRequest> _removeHallValidator;
		private readonly IValidator<ReserveHallRequest> _reserveValidator;
		private readonly IValidator<UpdateHallRequest> _updateHallValidator;
		private readonly IValidator<SearchFreeHallRequest> _searchHallValidator;

		public HallsController(IHallService hallService,
						 IValidator<CreateHallRequest> createHallValidator,
						 IValidator<RemoveHallRequest> removeHallValidator,
						 IValidator<ReserveHallRequest> reserveValidator,
						 IValidator<UpdateHallRequest> updateValidator,
						 IValidator<SearchFreeHallRequest> searchHallValidator)
		{
			_updateHallValidator = updateValidator;
			_reserveValidator = reserveValidator;
			_removeHallValidator = removeHallValidator;
			_createHallValidator = createHallValidator;
			_searchHallValidator = searchHallValidator;
			
			_hallService = hallService;
		}

		[HttpPost(HallsControllerRoutes.CreateHall)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
		[ProducesResponseType<CreateHallResponse>(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateHall(CreateHallRequest request, CancellationToken cancellationToken)
		{
			var validationResult = await _createHallValidator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				return Problem(validationResult.Errors.ToErrors());
			}

			var result = await _hallService.CreateHall(request, cancellationToken);

			return result.Match(Ok, Problem);
		}

		[HttpDelete(HallsControllerRoutes.RemoveHall)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
		[ProducesResponseType<RemoveHallResponse>(StatusCodes.Status200OK)]
		public async Task<IActionResult> RemoveHall(RemoveHallRequest request, CancellationToken cancellationToken)
		{
			var validationResult = await _removeHallValidator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				return Problem(validationResult.Errors.ToErrors());
			}

			var result = await _hallService.RemoveHall(request, cancellationToken);

			return result.Match(Ok, Problem);
		}

		[HttpPatch(HallsControllerRoutes.ReserveHall)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
		[ProducesResponseType<ReserveHallResponse>(StatusCodes.Status200OK)]
		public async Task<IActionResult> ReserveHall(ReserveHallRequest request, CancellationToken cancellationToken)
		{
			var validationResult = await _reserveValidator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				return Problem(validationResult.Errors.ToErrors());
			}

			var result = await _hallService.ReserveHall(request, cancellationToken);

			return result.Match(Ok, Problem);
		}

		[HttpPut(HallsControllerRoutes.UpdateHall)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
		[ProducesResponseType<UpdateHallResponse>(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateHall(UpdateHallRequest request, CancellationToken cancellationToken)
		{
			var validationResult = await _updateHallValidator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				return Problem(validationResult.Errors.ToErrors());
			}

			var result = await _hallService.UpdateHall(request, cancellationToken);

			return result.Match(Ok, Problem);
		}

		[HttpGet(HallsControllerRoutes.SearchFreeHall)]
		[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<SearchFreeHallResponse>(StatusCodes.Status200OK)]
		public async Task<IActionResult> SearchFreeHall(SearchFreeHallRequest request, CancellationToken cancellationToken)
		{
			var validationResult = await _searchHallValidator.ValidateAsync(request, cancellationToken);

			if (!validationResult.IsValid)
			{
				return Problem(validationResult.Errors.ToErrors());
			}

			var result = await _hallService.SearchFreeHall(request, cancellationToken);

			return Ok(result);
		}
	}
}
