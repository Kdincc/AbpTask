using FluentValidation;
using SmartHall.Common.Halls.Models.Dtos;
using SmartHall.Common.Halls.Models.ReserveHall;
using SmartHall.Common.Shared.Constants;

namespace SmartHall.BLL.Halls.Validators
{
    public sealed class ReserveHallRequestValidator : AbstractValidator<ReserveHallRequest>
	{
		private readonly IValidator<HallEquipmentDto> _equipmentValidator;

		public ReserveHallRequestValidator(TimeProvider timeProvider, IValidator<HallEquipmentDto> equipmentValidator)
		{
			_equipmentValidator = equipmentValidator;

			RuleFor(request => request)
				.Must(BeWithinOneDay)
				.WithMessage("Reservation must be within one day");

			RuleFor(c => c.HallId)
				.NotEmpty();

			RuleFor(c => c.ReservationDateTime)
				.NotEmpty()
				.Must(c => c >= timeProvider.GetUtcNow())
				.WithMessage("Reservation date time must be in the future");

			RuleFor(request => request)
				.Must(ValidateReservationTime)
				.WithMessage("Reservation time invalid. Reservation must be in range 6:00 - 23:00 and reservation must be in one calendar day");

			RuleFor(c => c.Hours)
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(c => c.SelectedEquipment)
				.Must(ValidateHallEquipment)
				.WithMessage("One or more equipments not valid");
		}

		private bool ValidateReservationTime(ReserveHallRequest reserveHall)
		{
			DateTime startDate = reserveHall.ReservationDateTime;
			DateTime endDate = reserveHall.ReservationDateTime.Add(TimeSpan.FromHours(reserveHall.Hours));

			if (startDate.TimeOfDay < BusinessHours.OpenTime || endDate.TimeOfDay > BusinessHours.CloseTime)
			{
				return false;
			}

			if (startDate.Date != endDate.Date)
			{
				return false;
			}

			return true;
		}

		private bool ValidateHallEquipment(List<HallEquipmentDto> equipmentDtos)
		{
			foreach (var equipment in equipmentDtos)
			{
				var validationResult = _equipmentValidator.Validate(equipment);

				if (!validationResult.IsValid)
				{
					return false;
				}
			}

			return true;
		}

		private bool BeWithinOneDay(ReserveHallRequest request)
		{
			DateTime start = request.ReservationDateTime;
			DateTime end = start.Add(TimeSpan.FromHours(request.Hours));

			return start.Date == end.Date;
		}
	}
}
