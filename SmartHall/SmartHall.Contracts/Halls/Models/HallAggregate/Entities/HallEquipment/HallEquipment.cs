using SmartHall.Common.BaseModels;
using SmartHall.Common.Shared.ValueObjects;

namespace SmartHall.Common.Halls.Models.HallAggregate.Entities.HallEquipment
{
    /// <summary>
    ///	Entity for hall equipment, part of hall aggregate
    /// </summary>
    public sealed class HallEquipment : Entity<Guid>
    {
        private HallEquipment() : base()
        {
        }

        public HallEquipment(Guid id, string name, Cost cost, Guid hallId) : base(id)
        {
            Name = name;
            Cost = cost;
            HallId = hallId;
        }

        public string Name { get; private set; }

        public Cost Cost { get; private set; }

        public Guid HallId { get; private set; }

        /// <summary>
        /// Method for comparing two hall equipment entities by name and cost
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns>Comparing result</returns>
        public bool IsSameAs(HallEquipment equipment)
        {
            return Name == equipment.Name && Cost == equipment.Cost;
        }
    }
}
