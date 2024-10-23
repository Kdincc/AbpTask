using SmartHall.Common.Halls.Models.Dtos;

namespace SmartHall.Service.Dtos.Halls
{
    public sealed class HallDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<HallEquipmentDto> Equipment { get; set; }

        public List<HallReservationDto> Reservations { get; set; }
    }
}
