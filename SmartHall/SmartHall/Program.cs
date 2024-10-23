using SmartHall.BLL;
using SmartHall.DAL.Sql;
using SmartHall.Service;

namespace SmartHall
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.RegisterServices(builder.Configuration);

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
