using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SmartHall.Application.Halls.Services;

namespace SmartHall.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IHallService, HallService>();

			ValidatorOptions.Global.LanguageManager.Culture = new("en");
			services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

			return services;
		}
	}
}
