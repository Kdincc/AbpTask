using FluentValidation;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Domain.Common.Constanst.Halls;

namespace SmartHall.Application.Halls.Validators.HallEquipment
{
	public sealed class CreateHallEquipmentDtoValidator : AbstractValidator<CreateHallEquipmentDto>
	{
		public CreateHallEquipmentDtoValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.MaximumLength(HallEquipmentConstants.MaxNameLenght);

			RuleFor(x => x.Cost)
				.NotEmpty()
				.GreaterThan(0);
		}
	}
}
