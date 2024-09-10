using SmartHall.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects
{
	public sealed class ReservationId : ValueObject
	{
        private ReservationId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static ReservationId CreateUnique() => new(Guid.NewGuid());

		public static ReservationId Create(string value) => new(Guid.Parse(value));
	}
}
