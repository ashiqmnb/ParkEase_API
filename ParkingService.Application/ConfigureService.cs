using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ParkingService.Application
{
	public static class ConfigureService
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			services.AddMediatR(ctg =>
			{
				ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});

			return services;
		}
	}
}
