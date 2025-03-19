﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingService.Application.Common.AppSettings;
using ParkingService.Infrastructure.Data;

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





			services.AddMassTransit(busConfigurator =>
			{
				busConfigurator.SetDefaultEndpointNameFormatter();


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
