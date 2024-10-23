using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHall.Common.ApiRoutes;
using SmartHall.Common.Halls;
using SmartHall.Common.Halls.Models.CreateHall;
using SmartHall.Common.Halls.Models.RemoveHall;
using SmartHall.Common.Halls.Models.ReserveHall;
using SmartHall.Common.Halls.Models.SearchFreeHall;
using SmartHall.Common.Halls.Models.UpdateHall;
using SmartHall.Controllers;

namespace SmartHall.Service.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public sealed class HallsController : ApiController
    {
        private readonly IHallManager _hallService;
        private readonly IValidator<CreateHallRequest> _createHallValidator;
        private readonly IValidator<RemoveHallRequest> _removeHallValidator;
        private readonly IValidator<ReserveHallRequest> _reserveValidator;
        private readonly IValidator<UpdateHallRequest> _updateHallValidator;
        private readonly IValidator<SearchFreeHallRequest> _searchHallValidator;
        private readonly IMapper _mapper;

        public HallsController(IHallManager hallService,
                         IValidator<CreateHallRequest> createHallValidator,
                         IValidator<RemoveHallRequest> removeHallValidator,
                         IValidator<ReserveHallRequest> reserveValidator,
                         IValidator<UpdateHallRequest> updateValidator,
                         IValidator<SearchFreeHallRequest> searchHallValidator,
                         IMapper mapper)
        {
            _updateHallValidator = updateValidator;
            _reserveValidator = reserveValidator;
            _removeHallValidator = removeHallValidator;
            _createHallValidator = createHallValidator;
            _searchHallValidator = searchHallValidator;

            _mapper = mapper;

            _hallService = hallService;
        }

        [HttpPost(HallsControllerRoutes.CreateHall)]
        [ProducesResponseType<UnauthorizedResult>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<CreateHallResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateHall(CreateHallRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _createHallValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Problem(_mapper.Map<List<Error>>(validationResult.Errors));
            }

            var result = await _hallService.CreateHall(request, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpDelete(HallsControllerRoutes.RemoveHall)]
        [ProducesResponseType<UnauthorizedResult>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<RemoveHallResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveHall(RemoveHallRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _removeHallValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Problem(_mapper.Map<List<Error>>(validationResult.Errors));
            }

            var result = await _hallService.RemoveHall(request, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpPatch(HallsControllerRoutes.ReserveHall)]
        [ProducesResponseType<UnauthorizedResult>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ReserveHallResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReserveHall(ReserveHallRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _reserveValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Problem(_mapper.Map<List<Error>>(validationResult.Errors));
            }

            var result = await _hallService.ReserveHall(request, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpPut(HallsControllerRoutes.UpdateHall)]
        [ProducesResponseType<UnauthorizedResult>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<UpdateHallResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateHall(UpdateHallRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _updateHallValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Problem(_mapper.Map<List<Error>>(validationResult.Errors));
            }

            var result = await _hallService.UpdateHall(request, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpGet(HallsControllerRoutes.SearchFreeHall)]
        [ProducesResponseType<UnauthorizedResult>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<SearchFreeHallResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchFreeHall(SearchFreeHallRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _searchHallValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Problem(_mapper.Map<List<Error>>(validationResult.Errors));
            }

            var result = await _hallService.SearchFreeHall(request, cancellationToken);

            return Ok(result);
        }
    }
}
