namespace SmartHall.Contracts.Halls.GetFreeHall
{
	public record SearchFreeHallRequest(DateTime DateTime, int Hours, int Capacity);
}
