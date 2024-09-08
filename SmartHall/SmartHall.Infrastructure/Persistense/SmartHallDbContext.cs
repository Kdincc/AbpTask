using Microsoft.EntityFrameworkCore;
using SmartHall.Domain.HallAggregate;
using SmartHall.Domain.HallAggregate.Entities.Reservation;
using SmartHall.Domain.HallEqupmentAggregateType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure.Persistense
{
    public sealed class SmartHallDbContext : DbContext
    {
        public SmartHallDbContext(DbContextOptions<SmartHallDbContext> options) : base(options)
        {
        }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<HallEquipmentType> HallEquipmentTypes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartHallDbContext).Assembly);
		}
    }
}
