using ErrorOr;

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

		public static Error SelectedHallEquipmentNotAvailable => Error.Conflict(
			code: "Hall.EquipmentNotAvailable",
			description: "Selected hall equipment is not available");

		public static Error Duplication => Error.Conflict(
			code: "Hall.Duplication",
			description: "Hall with that name, capacity, equipment and base cost already exists");
	}
}
