using Contracts.PaymentEvents;
using MassTransit;
using MediatR;
using PaymentService.Application.Common.AppSettings;
using PaymentService.Domain.Entity;
using PaymentService.Domain.Repository;

namespace PaymentService.Application.Coin.Command.AddSubscription
{
	public class AddSubscriptionCommandHandler : IRequestHandler<AddSubscriptionCommand, bool>
	{
		public readonly ITransactionRepo _transactionRepo;
		private readonly AppSettings _appSettings;
		private readonly IPublishEndpoint _publishEndpoint;

		public AddSubscriptionCommandHandler(AppSettings appSettings, ITransactionRepo transactionRepo, IPublishEndpoint publishEndpoint)
		{
			_appSettings = appSettings;
			_transactionRepo = transactionRepo;
			_publishEndpoint = publishEndpoint;
		}

		public async Task<bool> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
		{
			try
			{
				// coins based on packages
				if (request.Days != 7 && request.Days != 14) throw new Exception("Invalid Package");


				int coins = 0;
				if (request.Days == 7)
				{
					coins = 500;
				}
				else
				{
					coins = 950;
				}



				Guid transactionId = Guid.NewGuid();
				var transaction = new Domain.Entity.Transaction
				{
					Id = transactionId,
					SenderId = Guid.Parse(request.CompanyId),
					ReceiverId = Guid.Parse(_appSettings.AdminId),
					Coin = coins,
					Description = "Purchasing subscription",
					CreatedBy = request.CompanyId,
					CreatedOn = DateTime.UtcNow,
					Status = TransactionStatus.Pending,
				};

				await _transactionRepo.AddTransaction(transaction);
				await _transactionRepo.SaveChangesAsyncCustom();

				await _publishEndpoint.Publish(new AddSubscriptionEvent
				{
					TransactionId = transactionId,
					CompanyId = request.CompanyId,
					Coins = coins,
					Days = request.Days
				});

				return true;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException.Message ?? ex.Message);
			}
		}
	}
}
