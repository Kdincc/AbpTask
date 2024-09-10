using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Contracts.Halls.GetFreeHall
{
	public record SearchFreeHallRequest(DateOnly Date, TimeSpan StartTime, TimeSpan EndTime, int Capacity);
}
