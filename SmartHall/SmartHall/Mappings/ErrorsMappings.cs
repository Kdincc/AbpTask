using ErrorOr;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace SmartHall.Mappings
{
	public static class ErrorsMappings
	{
		public static List<Error> ToErrors(this List<ValidationFailure> failures)
		{
			return failures.Select(e => Error.Validation(code: e.ErrorCode, description: e.ErrorMessage)).ToList();
		}

		public static List<Error> ToErrors(this IEnumerable<IdentityError> identityErrors)
		{
			return identityErrors.Select(e => Error.Validation(code: e.Code, description: e.Description)).ToList();
		}
	}
}
