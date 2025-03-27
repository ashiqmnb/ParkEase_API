using Contracts.PaymentEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Consumer
{
	public class AddReservationEventConsumer : IConsumer<AddReservationEvent>
	{
		private readonly UserDbContext _userDbContext;
		private readonly IPublishEndpoint _publishEndpoint;

		public AddReservationEventConsumer(UserDbContext userDbContext, IPublishEndpoint publishEndpoint)
		{
			_userDbContext = userDbContext;
			_publishEndpoint = publishEndpoint;
		}

		
		public async Task Consume(ConsumeContext<AddReservationEvent> context)
		{
			using var transaction = await _userDbContext.Database.BeginTransactionAsync();
			try
			{
				var user = await _userDbContext.Users
					.FirstOrDefaultAsync(u => u.Id.ToString() == context.Message.UserId);
				if (user == null) throw new Exception("User not found");

				var company = await _userDbContext.Companies
					.FirstOrDefaultAsync(c => c.Id.ToString() == context.Message.CompanyId);
				if (company == null) throw new Exception("Company not found");

				if (user.Coins < context.Message.Coins) throw new Exception("Coins are not enought");

				user.Coins -= context.Message.Coins;
				company.Coins += context.Message.Coins;

				await _userDbContext.SaveChangesAsync();
				await transaction.CommitAsync();

				await _publishEndpoint.Publish(new UpdateTransactionStatusEvent
				{
					TransactionId = context.Message.TransactionId,
					Status = "Success",
					UpdatedOn = DateTime.UtcNow
				});

				await _publishEndpoint.Publish(new UpdateSlotEvent
				{
					SlotId = context.Message.SlotId,
					UserId = context.Message.UserId,
					UserName = user.Name,
					VehicleNumber = context.Message.VehicleNumber,
					Status = "Reserved",
				});

			}
			catch (Exception ex)
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
