using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.Common.Comparers
{
	public sealed class HallEquipmentComparer : IEqualityComparer<HallEquipment>
	{
		public bool Equals(HallEquipment x, HallEquipment y) => x.IsSameAs(y);

		public int GetHashCode(HallEquipment obj) => HashCode.Combine(obj.Name, obj.Cost);
	}
}
