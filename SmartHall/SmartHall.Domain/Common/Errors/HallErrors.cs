using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.Common.Errors
{
	public static class HallErrors
	{
		public static Error HallNotFound => Error.NotFound(
			code: "Hall.NotFound",
			description: "Hall with that Id not found");

		public static Error HallAlreadyReserved => Error.Conflict(
			code: "Hall.AlreadyReserved",
			description: "Hall is already reserved on this time");
	}
}
