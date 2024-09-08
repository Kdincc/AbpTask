using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHall.Application.Common.Persistance;
using SmartHall.Infrastructure.Persistense;
using SmartHall.Infrastructure.Persistense.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure
{
	public static class DependencyInjection
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<SmartHallDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly(typeof(SmartHallDbContext).Assembly.FullName)));

			services.AddScoped<IHallRepository, HallRepository>();
			services.AddScoped<IHallEquipmentTypeRepository, HallEquipmentTypeRepository>();
			services.AddScoped<IReservationRepository, ReservationsRepository>();
		}
	}
}
