namespace SmartHall.Service.Dtos.Halls
{
    public sealed class HallReservationDto
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public TimeSpan Duration { get; set; }

        public Guid HallId { get; set; }
    }
}