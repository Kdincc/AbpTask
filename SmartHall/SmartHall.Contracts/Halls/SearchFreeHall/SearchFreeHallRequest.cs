namespace SmartHall.Contracts.Halls.GetFreeHall
{
	public record SearchFreeHallRequest(DateTime DateTime, double Hours, int Capacity);
}
