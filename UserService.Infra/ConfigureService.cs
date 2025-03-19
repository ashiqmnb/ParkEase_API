using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Common.AppSettings;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Repository;
using UserService.Infrastructure.BackgroundServices;
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

			services.AddHostedService<SubscriptionCheckerService>();

			services.AddScoped<IAuthRepo, AuthRepo>();
			services.AddScoped<IAddressRepo, AddressRepo>();
			services.AddScoped<IUserRepo, UserRepo>();
			services.AddScoped<ICompanyRepo, CompanyRepo>();

			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<ICloudinaryService, CloudinaryService>();



			services.AddMassTransit(busConfigurator =>
			{
				busConfigurator.SetDefaultEndpointNameFormatter();

				busConfigurator.AddConsumer<AddCoinEventConsumer>();
				busConfigurator.AddConsumer<AddSubscriptionEventConsumer>();

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
