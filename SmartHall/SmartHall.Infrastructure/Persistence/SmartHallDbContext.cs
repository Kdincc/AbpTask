using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHall.Domain.HallAggregate;

namespace SmartHall.Infrastructure.Persistence
{
	public sealed class SmartHallDbContext : IdentityDbContext<IdentityUser>
	{
		public SmartHallDbContext(DbContextOptions<SmartHallDbContext> options) : base(options)
		{
		}

		public DbSet<Hall> Halls { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartHallDbContext).Assembly);
		}
	}
}
