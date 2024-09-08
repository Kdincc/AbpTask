using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallEqupmentAggregate;
using SmartHall.Domain.HallEqupmentAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense.Configurations
{
	public sealed class HallEquipmentConfiguration : IEntityTypeConfiguration<HallEquipment>
	{
		public void Configure(EntityTypeBuilder<HallEquipment> builder)
		{
			builder.ToTable("HallEquipments");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id)
				.ValueGeneratedNever()
				.HasConversion(id => id.Value, value => HallEquipmentTypeId.Create(value.ToString()));

			builder.Property(p => p.Name)
				.HasMaxLength(100);

			builder.Property(p => p.Cost)
				.HasConversion(cost => cost.Value, value => Cost.Create(value));
		}
	}
}
