using SmartHall.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.ValueObjects
{
	public sealed class HallId : ValueObject
	{
		public Guid Value { get; private set; }

		private HallId(Guid value)
		{
			Value = value;
		}

		public override IEnumerable<object> GetEqualityComponents()
		{
			throw new NotImplementedException();
		}

		public static HallId CreateUnique()
		{
			return new HallId(Guid.NewGuid());
		}

		public static HallId Create(Guid value)
		{
			if (value == Guid.Empty)
			{
				throw new ArgumentException("HallId cannot be empty");
			}

			return new HallId(value);
		}
	}
}
