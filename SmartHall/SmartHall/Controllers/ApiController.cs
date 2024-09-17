using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartHall.Controllers
{
	[ApiController]
	public abstract class ApiController : ControllerBase
	{
		protected IActionResult Problem(List<Error> errors)
		{
			Error error = errors.First();

			int statusCode = error.Type switch
			{
				ErrorType.Conflict => StatusCodes.Status409Conflict,
				ErrorType.NotFound => StatusCodes.Status404NotFound,
				ErrorType.Validation => StatusCodes.Status401Unauthorized,
				_ => StatusCodes.Status500InternalServerError
			};

			return Problem(statusCode: statusCode, title: error.Description);
		}
	}
}
