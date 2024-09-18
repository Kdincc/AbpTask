using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment.ValueObjects;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense.Configurations
{
	public sealed class HallConfiguration : IEntityTypeConfiguration<Hall>
	{
		public void Configure(EntityTypeBuilder<Hall> builder)
		{
			builder.ToTable("Halls");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id)
				.ValueGeneratedNever()
				.HasConversion(id => id.Value, value => HallId.Create(value.ToString()));

			builder.Property(p => p.Name)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(p => p.Capacity)
				.HasConversion(capacity => capacity.Value, value => Capacity.Create(value))
				.IsRequired();

			builder.Property(p => p.BaseCost)
				.HasConversion(cost => cost.Value, value => Cost.Create(value));

			builder.Metadata.FindNavigation(nameof(Hall.AvailableEquipment)).SetPropertyAccessMode(PropertyAccessMode.Field);
			builder.Metadata.FindNavigation(nameof(Hall.Reservations)).SetPropertyAccessMode(PropertyAccessMode.Field);

			ConfigureEquipment(builder);
			ConfigureReservation(builder);
		}

		private void ConfigureReservation(EntityTypeBuilder<Hall> builder)
		{
			builder.OwnsMany(p => p.Reservations, builder =>
			{
				builder.ToTable("Reservations");

				builder.WithOwner().HasForeignKey("HallId");

				builder.HasKey(p => p.Id);

				builder.Property(p => p.Id)
					.ValueGeneratedNever()
					.HasConversion(id => id.Value, value => ReservationId.Create(value.ToString()));

				builder.Property(x => x.Period)
				.HasConversion(new ReservationPeriodConverter())
				.HasColumnName("Period")
				.IsRequired();
			});

			builder.Navigation(p => p.Reservations).Metadata.SetField("_reservations");
			builder.Navigation(p => p.Reservations).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private void ConfigureEquipment(EntityTypeBuilder<Hall> builder)
		{
			builder.OwnsMany(p => p.AvailableEquipment, builder =>
			{
				builder.ToTable("HallEquipment");

				builder.WithOwner().HasForeignKey("HallId");

				builder.HasKey(p => p.Id);

				builder.Property(p => p.Id)
					.ValueGeneratedNever()
					.HasConversion(id => id.Value, value => HallEquipmentId.Create(value.ToString()));

				builder.Property(p => p.Name)
					.HasMaxLength(100)
					.IsRequired();

				builder.Property(p => p.Cost)
					.HasConversion(cost => cost.Value, value => Cost.Create(value))
					.IsRequired();

				builder.Property(p => p.HallId)
					.HasConversion(id => id.Value, value => HallId.Create(value.ToString()));
			});

			builder.Navigation(p => p.AvailableEquipment).Metadata.SetField("_availableEquipment");
			builder.Navigation(p => p.AvailableEquipment).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		public class ReservationPeriodConverter : ValueConverter<ReservationPeriod, string>
		{
			public ReservationPeriodConverter()
				: base(
					period => ConvertToString(period),
					str => ConvertFromString(str))
			{
			}

			private static string ConvertToString(ReservationPeriod period)
			{
				return $"{period.Start},{period.Duration.TotalHours}";
			}

			private static ReservationPeriod ConvertFromString(string str)
			{
				var parts = str.Split(',');
				var start = DateTime.Parse(parts[0]);
				var duration = TimeSpan.FromHours(double.Parse(parts[1]));
				return ReservationPeriod.Create(start, duration);
			}
		}

	}
}
