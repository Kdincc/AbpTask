using SmartHall.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects
{
	public sealed class ReservationPeriod : ValueObject
	{
        private ReservationPeriod(DateTimeOffset start, DateTimeOffset end)
        {
            Start = start;
			End = end;
        }

        public DateTimeOffset Start { get; private set; }

		public DateTimeOffset End { get; private set; }

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Start;
			yield return End;
		}

		public bool Contains(DateTimeOffset date)
		{
			return date >= Start && date <= End;
		}

		public static ReservationPeriod Create(DateTimeOffset start, DateTimeOffset end)
		{
			if (start >= end)
			{
				throw new ArgumentException("Start date must be before end date");
			}

			return new ReservationPeriod(start, end);
		}
	}
}
