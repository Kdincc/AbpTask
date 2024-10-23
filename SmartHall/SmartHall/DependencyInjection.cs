using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartHall.Common.Repositories;
using SmartHall.DAL.Sql.Repos;
using SmartHall.DAL.Sql;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SmartHall.BLL.Authentication;
using SmartHall.Common.Authentication;
using SmartHall.Common.Halls;
using System.Text;
using SmartHall.BLL.Halls.Managers;
using SmartHall.Service.Mappings;
using SmartHall.DAL.Sql.Mappings;
using SmartHall.BLL.Halls.Validators;

namespace SmartHall.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddService();
            services.AddDAL(configuration);
            services.AddBLL(configuration);

            return services;
        }

        private static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });

                services.AddAutoMapper(a => a.AddProfile<ErrorMapper>(), typeof(ErrorMapper).Assembly);
            });

            return services;
        }

        private static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
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

            services.AddAutoMapper(a => a.AddProfile<HallMappings>(), typeof(HallMappings).Assembly);

            return services;
        }

        private static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHallManager, HallService>();

            ValidatorOptions.Global.LanguageManager.Culture = new("en");
            services.AddValidatorsFromAssembly(typeof(CreateHallRequestValidator).Assembly);

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
