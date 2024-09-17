using FluentValidation;
using SmartHall.Contracts.Halls.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHall.Domain.Common.Constanst;
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
				.MaximumLength(HallEquipmentConstants.MaxNameLenght);

			RuleFor(x => x.Cost)
				.NotEmpty()
				.GreaterThan(0);
		}
	}
}
