namespace SmartHall.Domain.Common.Constants
{
	/// <summary>
	/// Constants describing business hours
	/// </summary>
	public static class BusinessHours
	{
		public static readonly TimeSpan OpenTime = new(6, 0, 0);

		public static readonly TimeSpan CloseTime = new(23, 0, 0);
	}
}
