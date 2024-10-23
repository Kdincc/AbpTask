using SmartHall.BLL.Halls.HallAggregate.Entities.Reservation;
using SmartHall.BLL.Halls.HallAggregate.ValueObjects;
using SmartHall.Common.BaseModels;
using SmartHall.Common.Halls.Models.HallAggregate.Comparers;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.HallEquipment;
using SmartHall.Common.Shared.ValueObjects;

namespace SmartHall.Common.Halls.Models.HallAggregate
{
    public sealed class Hall : AggregateRoot<Guid>
    {
        private readonly List<Reservation> _reservations;
        private List<HallEquipment> _availableEquipment;

        private Hall() : base()
        {
            _reservations = [];
            _availableEquipment = [];
        }

        public Hall(Guid id, string name, Capacity capacity, Cost baseCost, List<HallEquipment> availableEquipment, List<Reservation> reservations) : base(id)
        {
            Name = name;
            Capacity = capacity;
            BaseCost = baseCost;
            _availableEquipment = availableEquipment;
            _reservations = reservations;
        }

        public string Name { get; private set; }

        public Capacity Capacity { get; private set; }

        public Cost BaseCost { get; private set; }

        public IReadOnlyCollection<HallEquipment> AvailableEquipment => _availableEquipment.AsReadOnly();

        public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();

        public void Update(string name, Capacity capacity, Cost baseCost, List<HallEquipment> hallEquipment)
        {
            Name = name;
            Capacity = capacity;
            BaseCost = baseCost;
            _availableEquipment = hallEquipment;
        }

        /// <summary>
        /// Method for reserving hall, returns total cost of reservation
        /// </summary>
        /// <param name="reservation">Reservation to add</param>
        /// <param name="selectedEquipment">selected equipment for this reservation</param>
        /// <param name="reservationStrategy">strategy for calculating total reservation cost</param>
        /// <returns>Total cost of reservation</returns>
        /// <exception cref="ArgumentException">Throwing if period of reservation overlaps with existing for this hall</exception>
        public Cost Reserve(Reservation reservation, List<HallEquipment> selectedEquipment, IHallReservationStrategy reservationStrategy)
        {
            if (IsReservationOverlaps(reservation))
            {
                throw new ArgumentException("Reservation period overlaps with another reservation");
            }

            _reservations.Add(reservation);

            return reservationStrategy.CalculateCost(this, reservation, selectedEquipment);
        }

        public bool IsSameAs(Hall hall)
        {
            return Name == hall.Name && Capacity == hall.Capacity && BaseCost == hall.BaseCost && HasSameEquipment(hall.AvailableEquipment);
        }

        public bool HasSameEquipment(IEnumerable<HallEquipment> equipment)
        {
            var comparer = new HallEquipmentComparer();

            bool areEqual = equipment.SequenceEqual(_availableEquipment, comparer);

            return areEqual;
        }

        public bool IsReservationOverlaps(Reservation reservation)
        {
            if (_reservations.Any(r => r.Period.Overlaps(reservation.Period)))
            {
                return true;
            }

            return false;
        }

        public bool HasNotAvailableEquipment(List<HallEquipment> equipment)
        {
            return equipment.Any(e => !_availableEquipment.Contains(e));
        }
    }
}
