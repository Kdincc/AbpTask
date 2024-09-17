using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment;
using SmartHall.Domain.HallAggregate.Entities.HallEquipment.ValueObjects;
using SmartHall.Domain.HallAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense.Configurations
{
    public sealed class HallEquipmentConfiguration : IEntityTypeConfiguration<Domain.HallAggregate.Entities.HallEquipment.HallEquipment>
	{
		public void Configure(EntityTypeBuilder<Domain.HallAggregate.Entities.HallEquipment.HallEquipment> builder)
		{
			builder.ToTable("HallEquipment");

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

			builder.HasOne<Hall>()
			.WithMany(h => h.AvailableEquipment)
			.HasForeignKey(p => p.HallId);
		}
	}
}
