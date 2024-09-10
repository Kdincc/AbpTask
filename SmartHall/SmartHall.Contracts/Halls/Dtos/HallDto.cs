﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Contracts.Halls.Dtos
{
	public record HallDto(Guid Id, string Name, List<HallEquipmentDto> HallEquipment);
}