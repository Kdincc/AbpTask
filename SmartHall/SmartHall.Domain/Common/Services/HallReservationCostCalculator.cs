using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.Common.Services
{
	public sealed class HallReservationCostCalculator
	{
		public Cost Calculate(Hall hall, Reservation reservation)
		{
			Cost totalCost = Cost.Create(0);

			// Разделяем бронирование на промежутки времени и рассчитываем стоимость для каждого
			for (var current = reservation.Period.Start; current < reservation.Period.End; current = current.AddHours(1))
			{
				Cost hourlyRate = hall.BaseCost;

				// Определяем тариф в зависимости от времени
				if (current.TimeOfDay >= TimeSpan.FromHours(9) && current.TimeOfDay < TimeSpan.FromHours(18))
				{
					// Стандартные часы
					hourlyRate = hall.BaseCost;
				}
				else if (current.TimeOfDay >= TimeSpan.FromHours(18) && current.TimeOfDay < TimeSpan.FromHours(23))
				{
					// Вечерние часы - скидка 20%
					hourlyRate = Cost.Create(hall.BaseCost.Value * 0.8m);
				}
				else if (current.TimeOfDay >= TimeSpan.FromHours(6) && current.TimeOfDay < TimeSpan.FromHours(9))
				{
					// Ранкові години - скидка 10%
					hourlyRate = Cost.Create(hall.BaseCost.Value * 0.9m);
				}
				else if (current.TimeOfDay >= TimeSpan.FromHours(12) && current.TimeOfDay < TimeSpan.FromHours(14))
				{
					// Пиковые часы - наценка 15%
					hourlyRate = Cost.Create(hall.BaseCost.Value * 1.15m);
				}

				totalCost = Cost.Create(totalCost.Value + hourlyRate.Value);
			}

			return totalCost;
		}
	}
}
