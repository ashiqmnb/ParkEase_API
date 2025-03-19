using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Common.AppSettings;
using PaymentService.Domain.Repository;
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


			services.AddTransient<IPaymentRepo, PaymentRepo>();


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
