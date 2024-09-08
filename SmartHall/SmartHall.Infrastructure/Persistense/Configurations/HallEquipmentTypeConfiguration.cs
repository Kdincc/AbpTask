using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHall.Domain.Common.ValueObjects;
using SmartHall.Domain.HallEqupmentAggregate;
using SmartHall.Domain.HallEqupmentAggregate.ValueObjects;
using SmartHall.Domain.HallEqupmentAggregateType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense.Configurations
{
	public sealed class HallEquipmentTypeConfiguration : IEntityTypeConfiguration<HallEquipmentType>
	{
		public void Configure(EntityTypeBuilder<HallEquipmentType> builder)
		{
			builder.ToTable("HallEquipments");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id)
				.ValueGeneratedNever()
				.HasConversion(id => id.Value, value => HallEquipmentTypeId.Create(value.ToString()));

			builder.Property(p => p.Name)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(p => p.Cost)
				.HasConversion(cost => cost.Value, value => Cost.Create(value))
				.IsRequired();
		}
	}
}
