using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.Common.Constanst
{
	public static class BusinessHours
	{
		public static readonly TimeSpan OpenTime = new(6, 0, 0);

		public static readonly TimeSpan CloseTime = new(23, 0, 0);
	}
}
