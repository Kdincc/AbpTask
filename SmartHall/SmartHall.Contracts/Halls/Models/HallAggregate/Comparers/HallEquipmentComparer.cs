using SmartHall.Common.Halls.Models.HallAggregate.Entities.HallEquipment;

namespace SmartHall.Common.Halls.Models.HallAggregate.Comparers
{
    public sealed class HallEquipmentComparer : IEqualityComparer<HallEquipment>
    {
        public bool Equals(HallEquipment x, HallEquipment y) => x.IsSameAs(y);

        public int GetHashCode(HallEquipment obj) => HashCode.Combine(obj.Name, obj.Cost);
    }
}
