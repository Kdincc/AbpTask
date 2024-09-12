using Microsoft.Extensions.DependencyInjection;
using SmartHall.Application.Halls.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IHallService, HallService>();

			return services;
		}
	}
}
