using SmartHall.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.HallAggregate.ValueObjects
{
	public sealed class HallEquipment : ValueObject
	{
		private HallEquipment(string name, Cost cost)
		{
			Name = name;
			Cost = cost;
		}

		public string Name { get; private set; }
		public Cost Cost { get; private set; }

		public override IEnumerable<object> GetEqualityComponents()
		{
			yield return Name;
			yield return Cost;
		}

		public static HallEquipment Create(string name, Cost cost)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name cannot be null or empty");
			}

			return new HallEquipment(name, cost);
		}

	}
}
