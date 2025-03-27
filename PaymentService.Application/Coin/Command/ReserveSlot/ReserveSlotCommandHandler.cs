using Contracts.PaymentEvents;
using MassTransit;
using MediatR;
using PaymentService.Domain.Repository;
using System.Transactions;

namespace PaymentService.Application.Coin.Command.ReserveSlot
{
	public class ReserveSlotCommandHandler : IRequestHandler<ReserveSlotCommand, bool>
	{
		private readonly ITransactionRepo _transactionRepo;
		private readonly IPublishEndpoint _publishEndpoint;

		public ReserveSlotCommandHandler(ITransactionRepo transactionRepo, IPublishEndpoint publishEndpoint)
		{
			_transactionRepo = transactionRepo;
			_publishEndpoint = publishEndpoint;
		}

		public async Task<bool> Handle(ReserveSlotCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.UserId == null) throw new Exception("User id id required ");
				if (request.ReserveSlotDto == null) throw new Exception("ReserveSlotDto is required");

				int coins = 0;
				if(request.ReserveSlotDto.Type == "Customer")
				{
					coins = 5;
				}
				else
				{
					coins = 10;
				}


				Guid transactionId = Guid.NewGuid();
				var transaction = new Domain.Entity.Transaction
				{
					Id = transactionId,
					SenderId = Guid.Parse(request.UserId),
					ReceiverId = Guid.Parse(request.ReserveSlotDto.CompanyId),
					Coin = coins,
					Description = "Reserving parking slot",
					CreatedBy = request.UserId,
					CreatedOn = DateTime.UtcNow,
					Status = Domain.Entity.TransactionStatus.Pending,
				};

				await _transactionRepo.AddTransaction(transaction);
				await _transactionRepo.SaveChangesAsyncCustom();

				await _publishEndpoint.Publish(new AddReservationEvent
				{
					TransactionId = transactionId,
					UserId = request.UserId,
					CompanyId = request.ReserveSlotDto.CompanyId,
					Coins = coins,
					SlotId = request.ReserveSlotDto.SlotId,
					VehicleNumber = request.ReserveSlotDto.VehicleNumber
				});

				return true;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
