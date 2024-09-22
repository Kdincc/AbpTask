
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
				.AddApplication(builder.Configuration) 
				.AddPresentation();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseAuthentication();

			app.MapControllers();

			app.Run();
		}
	}
}
