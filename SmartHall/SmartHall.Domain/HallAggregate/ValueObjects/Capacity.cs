using SmartHall.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.ValueObjects
{
	public sealed class Capacity : ValueObject
	{
		public int Value { get; private set; }

		private Capacity(int value)
		{
			Value = value;
		}

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static Capacity Create(int value)
		{
			if (value < 0)
			{
				throw new ArgumentException("Capacity cannot be negative");
			}

			return new Capacity(value);
		}
	}
}
