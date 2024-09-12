using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
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

			builder.HasMany(p => p.AvailableEquipment)
				.WithOne()
				.HasForeignKey(h => h.HallId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(p => p.Reservations)
				.WithOne()
				.HasForeignKey(r => r.HallId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
