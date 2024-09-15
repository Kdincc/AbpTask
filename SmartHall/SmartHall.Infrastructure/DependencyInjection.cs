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
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<SmartHallDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DbString"),
					b => b.MigrationsAssembly(typeof(SmartHallDbContext).Assembly.FullName)));

			services.AddScoped<IHallRepository, HallRepository>();

			services.AddSingleton(TimeProvider.System);

			return services;
		}
	}
}
