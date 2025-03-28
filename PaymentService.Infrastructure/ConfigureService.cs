﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Common.AppSettings;
using PaymentService.Domain.Repository;
using PaymentService.Infrastructure.Consumer;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Repository;

namespace PaymentService.Infrastructure
{
	public static class ConfigureService
	{
		public static IServiceCollection AddInfrastructureServices
			(this IServiceCollection services, IConfiguration configuration, AppSettings appSettings)
		{
			services.AddDbContext<PaymentDbContext>(optins =>
			{
				optins.UseNpgsql(appSettings.DbConnectionString);
			});


			services.AddScoped<IPaymentRepo, PaymentRepo>();
			services.AddScoped<ITransactionRepo, TransactionRepo>();


			services.AddMassTransit(busConfigurator =>
			{
				busConfigurator.SetDefaultEndpointNameFormatter();

				busConfigurator.AddConsumer<UpdateTransactionStatusEventConsumer>();

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
