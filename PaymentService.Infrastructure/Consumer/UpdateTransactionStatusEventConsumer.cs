 using Contracts.PaymentEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entity;
using PaymentService.Infrastructure.Data;

namespace PaymentService.Infrastructure.Consumer
{
	public class UpdateTransactionStatusEventConsumer : IConsumer<UpdateTransactionStatusEvent>
	{
		private readonly PaymentDbContext _paymentDbContext;

		public UpdateTransactionStatusEventConsumer(PaymentDbContext paymentDbContext)
		{
			_paymentDbContext = paymentDbContext;
		}

		public async Task Consume(ConsumeContext<UpdateTransactionStatusEvent> context)
		{
			try
			{
				var transaction = await _paymentDbContext.Transactions
					.FirstOrDefaultAsync(t => t.Id == context.Message.TransactionId);
				if (transaction == null) throw new Exception("Transaction not found");

				if(context.Message.Status == "Success") transaction.Status = TransactionStatus.Success;
				if (context.Message.Status == "Failed") transaction.Status = TransactionStatus.Failed;

				transaction.UpdatedOn = context.Message.UpdatedOn;

				await _paymentDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
