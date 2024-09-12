using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Contracts.Halls.GetFreeHall
{
	public record SearchFreeHallRequest(DateTime DateTime, TimeSpan Duratation, int Capacity);
}
