using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UserService.Application
{
	public static class ConfigureService
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(ctg =>
			{
				ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});

			return services;
		}
	}
}
