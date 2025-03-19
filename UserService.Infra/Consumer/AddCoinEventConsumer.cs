using Contracts.PaymentEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Consumer
{
	public sealed class AddCoinEventConsumer : IConsumer<AddCoinEvent>
	{
		private readonly UserDbContext _userDbContext;

		public AddCoinEventConsumer(UserDbContext userDbContext)
		{
			_userDbContext = userDbContext;
		}

		public async Task Consume(ConsumeContext<AddCoinEvent> context)
		{
			using var transaction = await _userDbContext.Database.BeginTransactionAsync();

			try
			{
				var user = await _userDbContext.Users
					.FirstOrDefaultAsync(u => u.Id.ToString() == context.Message.UserId);

				var company = await _userDbContext.Companies
					.FirstOrDefaultAsync(c => c.Id.ToString() == context.Message.UserId);

				var admin = await _userDbContext.Admins.FirstOrDefaultAsync();

				if (admin == null)
				{
					throw new Exception("Admin not found");
				}

				if (user != null)
				{
					user.Coins += context.Message.Coins;
				}
				else if (company != null)
				{
					company.Coins += context.Message.Coins;
				}
				else
				{
					throw new Exception("User or Company not found");
				}

				// Deduct coins from admin
				if (admin.Coins < context.Message.Coins)
				{
					throw new Exception("Admin does not have enough coins");
				}

				admin.Coins -= context.Message.Coins;

				// Save changes
				await _userDbContext.SaveChangesAsync();
				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
