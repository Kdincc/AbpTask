using ErrorOr;
using FluentValidation.Results;

namespace SmartHall.Mappings
{
	public static class ValidationFailureMapping
	{
		public static List<Error> ToErrors(this List<ValidationFailure> failures)
		{
			return failures.Select(e => Error.Validation(code: e.ErrorCode, description: e.ErrorMessage)).ToList();
		}
	}
}
