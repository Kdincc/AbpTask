﻿using FluentValidation;
using SmartHall.Contracts.Halls.UpdateHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHall.Domain.Common.Constanst.Halls;
using SmartHall.Contracts.Halls.CreateHall;

namespace SmartHall.Application.Halls.Validators
{
	public sealed class UpdateHallRequestValidator : AbstractValidator<UpdateHallRequest>
	{
		private readonly IValidator<CreateHallEquipmentDto> _equipmentValidator;

		public UpdateHallRequestValidator(IValidator<CreateHallEquipmentDto> equipmentValidator)
		{
			_equipmentValidator = equipmentValidator;

			RuleFor(c => c.HallId)
				.NotEmpty();

			RuleFor(c => c.Name)
				.NotEmpty()
				.MaximumLength(HallConstants.MaxNameLenght);

			RuleFor(c => c.Capacity)
				.GreaterThan(0);

			RuleFor(c => c.BaseCost)
				.GreaterThan(0);

			RuleFor(c => c.HallEquipment)
				.Must(ValidateHallEquipment)
				.WithMessage("One or more equipments not valid");
		}

		private bool ValidateHallEquipment(List<CreateHallEquipmentDto> equipmentDtos)
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
	}
}
