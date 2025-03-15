using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Common.AppSettings;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Repository;
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
			services.AddTransient<IEmailService, EmailService>();

			return services;
		}
	}
}
