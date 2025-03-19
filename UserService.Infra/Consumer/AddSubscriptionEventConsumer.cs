using Contracts.PaymentEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entity;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Consumer
{
	public class AddSubscriptionEventConsumer : IConsumer<AddSubscriptionEvent>
	{
		private readonly UserDbContext _userDbContext;
		private readonly IPublishEndpoint _publishEndpoint;

		public AddSubscriptionEventConsumer(UserDbContext userDbContext, IPublishEndpoint publishEndpoint)
		{
			_userDbContext = userDbContext;
			_publishEndpoint = publishEndpoint;
		}
		public async Task Consume(ConsumeContext<AddSubscriptionEvent> context)
		{
			using var transaction = await _userDbContext.Database.BeginTransactionAsync();
			try
			{
				var company = await _userDbContext.Companies
					.FirstOrDefaultAsync(c => c.Id.ToString() == context.Message.CompanyId);
				if (company == null) throw new Exception("Company not found");

				var admin = await _userDbContext.Admins.FirstOrDefaultAsync();
				if (admin == null) throw new Exception("Admin not found");

				if (company.Coins < context.Message.Coins) throw new Exception("Coins are not enought");

				company.Coins -= context.Message.Coins;
				admin.Coins += context.Message.Coins;

				company.SubscriptionStatus = SubscriptionStatus.Active;
				company.SubscriptionStartDate = DateTime.UtcNow;
				company.SubscriptionDurationInDays = 7;
				company.SubscriptionExpiryDate = DateTime.UtcNow.AddDays(7);
				company.UpdatedBy = context.Message.CompanyId;
				company.UpdatedOn = DateTime.UtcNow;

				await _userDbContext.SaveChangesAsync();
				await transaction.CommitAsync();

				await _publishEndpoint.Publish(new UpdateTransactionStatusEvent
				{
					TransactionId = context.Message.TransactionId,
					Status = "Success",
					UpdatedOn = DateTime.UtcNow
				});

			}
			catch(Exception ex)
			{
				await transaction.RollbackAsync();

				await _publishEndpoint.Publish(new UpdateTransactionStatusEvent
				{
					TransactionId = context.Message.TransactionId,
					Status = "Failed",
					UpdatedOn = DateTime.UtcNow,
				});

				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
