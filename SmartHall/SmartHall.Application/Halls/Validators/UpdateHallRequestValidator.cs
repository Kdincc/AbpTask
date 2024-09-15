using FluentValidation;
using SmartHall.Contracts.Halls.UpdateHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHall.Domain.Common.Constanst.Halls;

namespace SmartHall.Application.Halls.Validators
{
	public sealed class UpdateHallRequestValidator : AbstractValidator<UpdateHallRequest>
	{
		public UpdateHallRequestValidator()
		{
			RuleFor(c => c.HallId)
				.NotEmpty();

			RuleFor(c => c.Name)
				.NotEmpty()
				.MaximumLength(HallConstants.MaxNameLenght);

			RuleFor(c => c.Capacity)
				.GreaterThan(0);

			RuleFor(c => c.BaseCost)
				.GreaterThan(0);
		}
	}
}
