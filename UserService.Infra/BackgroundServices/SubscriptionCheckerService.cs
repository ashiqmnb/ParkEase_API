using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UserService.Domain.Entity;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.BackgroundServices
{
	public class SubscriptionCheckerService : BackgroundService
	{
		private readonly ILogger<SubscriptionCheckerService> _logger;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public SubscriptionCheckerService(ILogger<SubscriptionCheckerService> logger, IServiceScopeFactory serviceScopeFactory)
		{
			_logger = logger;
			_serviceScopeFactory = serviceScopeFactory;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				while (!stoppingToken.IsCancellationRequested)
				{
					using (var scope = _serviceScopeFactory.CreateScope())
					{
						var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

						var expiredSubscriptions = await userDbContext.Companies
							.Where(c => c.SubscriptionExpiryDate < DateTime.UtcNow && c.SubscriptionStatus == SubscriptionStatus.Active)
							.ToListAsync(stoppingToken);

						if (expiredSubscriptions.Any())
						{
							foreach (var company in expiredSubscriptions)
							{
								company.SubscriptionStatus = SubscriptionStatus.Inactive;
								company.SubscriptionExpiryDate = null;
								company.SubscriptionStartDate = null;
								company.SubscriptionDurationInDays = null;
							}

							await userDbContext.SaveChangesAsync(stoppingToken);
							_logger.LogInformation($"Deactivated {expiredSubscriptions.Count} expired subscriptions.");
						}
					}

					await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError($"❌ Error in Background Service: {ex.Message}");
			}
		}
	}
}
