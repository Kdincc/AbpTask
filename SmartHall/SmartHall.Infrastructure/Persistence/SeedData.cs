using Microsoft.EntityFrameworkCore;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistence
{
	public static class SeedData
	{
		public static void Seed(DbSet<Hall> halls)
		{
			if (halls.Any())
			{
				return;
			}

			string projectorName = "Projector";
			decimal projectorCost = 500;

			string soundSystemName = "Sound System";
			decimal soundSystemCost = 700;

			string wifiName = "Wi-Fi";
			decimal wifiCost = 300;

			Guid hallAId = Guid.NewGuid();
			List<HallEquipment> hallAEquipment =
			[
				new HallEquipment(Guid.NewGuid(), projectorName, Cost.Create(projectorCost), hallAId),
				new HallEquipment(Guid.NewGuid(), soundSystemName, Cost.Create(soundSystemCost), hallAId),
				new HallEquipment(Guid.NewGuid(), wifiName, Cost.Create(wifiCost), hallAId)
			];
			Hall hallA = new Hall(hallAId, "Hall A", Capacity.Create(50), Cost.Create(2000), hallAEquipment, []);

			Guid hallBId = Guid.NewGuid();
			List<HallEquipment> hallBEquipment =
			[
				new HallEquipment(Guid.NewGuid(), projectorName, Cost.Create(projectorCost), hallBId),
				new HallEquipment(Guid.NewGuid(), soundSystemName, Cost.Create(soundSystemCost), hallBId),
				new HallEquipment(Guid.NewGuid(), wifiName, Cost.Create(wifiCost), hallBId)
			];
			Hall hallB = new Hall(hallBId, "Hall B", Capacity.Create(100), Cost.Create(3500), hallBEquipment, []);

			Guid hallCId = Guid.NewGuid();
			List<HallEquipment> hallCEquipment =
			[
				new HallEquipment(Guid.NewGuid(), projectorName, Cost.Create(projectorCost), hallCId),
				new HallEquipment(Guid.NewGuid(), soundSystemName, Cost.Create(soundSystemCost), hallCId),
				new HallEquipment(Guid.NewGuid(), wifiName, Cost.Create(wifiCost), hallCId)
			];
			Hall hallC = new Hall(hallCId, "Hall C", Capacity.Create(30), Cost.Create(1500), hallCEquipment, []);

			halls.AddRange(hallA, hallB, hallC );
		}
	}
}
