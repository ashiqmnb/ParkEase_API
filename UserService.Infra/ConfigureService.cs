using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Common.AppSettings;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Repository;
using UserService.Infrastructure.Consumer;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Repository;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure
{
    public static class ConfigureService
	{
		public static IServiceCollection AddInfrastructureServices
			(this IServiceCollection services, IConfiguration configuration, AppSettings appSettings)
		{
			services.AddDbContext<UserDbContext>(optins =>
			{
				optins.UseNpgsql(appSettings.DbConnectionString);
			});

			services.AddTransient<IAuthRepo, AuthRepo>();
			services.AddTransient<IAddressRepo, AddressRepo>();
			services.AddTransient<IUserRepo, UserRepo>();
			services.AddTransient<ICompanyRepo, CompanyRepo>();

			services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<ICloudinaryService, CloudinaryService>();

			services.AddMassTransit(busConfigurator =>
			{
				busConfigurator.SetDefaultEndpointNameFormatter();

				busConfigurator.AddConsumer<AddCoinEventConsumer>();

				busConfigurator.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host("localhost", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});

					cfg.ConfigureEndpoints(context);
				});
			});

			return services;
		}
	}
}
