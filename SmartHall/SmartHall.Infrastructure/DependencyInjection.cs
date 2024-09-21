using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHall.Application.Common.Persistance;
using SmartHall.Infrastructure.Persistense;
using SmartHall.Infrastructure.Persistense.Repos;

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

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<SmartHallDbContext>()
				.AddDefaultTokenProviders();

			return services;
		}
	}
}
