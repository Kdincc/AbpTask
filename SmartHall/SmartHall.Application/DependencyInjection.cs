using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SmartHall.Application.Halls.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using SmartHall.Application.Authentication;

namespace SmartHall.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IHallService, HallService>();

			ValidatorOptions.Global.LanguageManager.Culture = new("en");
			services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

			services.AddApplicationAuthentication(configuration);

			services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();

			return services;
		}

		private static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("Jwt");

			services.AddAuthorization();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtSettings["Issuer"],
						ValidAudience = jwtSettings["Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
					};
				});


			return services;
		}
	}
}
