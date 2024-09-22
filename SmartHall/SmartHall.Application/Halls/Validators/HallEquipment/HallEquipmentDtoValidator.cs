using FluentValidation;
using SmartHall.Contracts.Halls.Dtos;
using SmartHall.Domain.Common.Constanst.Halls;

namespace SmartHall.Application.Halls.Validators.HallEquipment
{
	public sealed class HallEquipmentDtoValidator : AbstractValidator<HallEquipmentDto>
	{
		public HallEquipmentDtoValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty();

			RuleFor(x => x.Name)
				.NotEmpty()
				.MaximumLength(HallEquipmentConstants.MaxNameLength);

			RuleFor(x => x.Cost)
				.NotEmpty()
				.GreaterThan(0);
		}
	}
}
