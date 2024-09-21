
using Microsoft.AspNetCore.Identity;
using SmartHall.Application;
using SmartHall.Infrastructure;

namespace SmartHall
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services
				.AddInfrastructure(builder.Configuration)
				.AddApplication() 
				.AddPresentation();

			builder.Services.AddAuthorization();
			builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
