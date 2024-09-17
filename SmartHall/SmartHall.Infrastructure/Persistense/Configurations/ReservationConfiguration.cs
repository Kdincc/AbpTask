using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallAggregate.Entities.Reservation.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense.Configurations
{
	public sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
	{
		public void Configure(EntityTypeBuilder<Reservation> builder)
		{
			builder.ToTable("Reservations");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever()
				.HasConversion(id => id.Value, value => ReservationId.Create(value.ToString()));

			builder.Property(x => x.HallId)
				.HasConversion(id => id.Value, value => HallId.Create(value.ToString()));

			builder.Property(x => x.Period)
				.HasConversion(new ReservationPeriodConverter())
				.HasColumnName("Period")
				.IsRequired();
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
				return $"{period.Start:O},{period.Duration.TotalHours}";
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
