using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingService.Application.Common.AppSettings;
using ParkingService.Domain.Repository;
using ParkingService.Infrastructure.Consumer;
using ParkingService.Infrastructure.Data;
using ParkingService.Infrastructure.Repository;

namespace ParkingService.Infrastructure
{
	public static class ConfigureService
	{
		public static IServiceCollection AddInfrastructureServices
			(this IServiceCollection services, IConfiguration configuration, AppSettings appSettings)
		{
			services.AddDbContext<ParkingDbContext>(optins =>
			{
				optins.UseNpgsql(appSettings.DbConnectionString);
			});


			services.AddScoped<ISlotRepo, SlotRepo>();
			services.AddScoped<IHistoryRepo, HistoryRepo>();




			services.AddMassTransit(busConfigurator =>
			{
				busConfigurator.SetDefaultEndpointNameFormatter();

				busConfigurator.AddConsumer<UpdateSlotEventConsumer>();


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
