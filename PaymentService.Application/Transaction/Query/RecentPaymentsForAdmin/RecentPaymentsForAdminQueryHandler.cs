using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;
using PaymentService.Domain.Repository;

namespace PaymentService.Application.Transaction.Query.RecentPaymentsForAdmin
{
	public class RecentPaymentsForAdminQueryHandler : IRequestHandler<RecentPaymentsForAdminQuery, List<PaymentResDTO>>
	{
		private readonly IPaymentRepo _paymentRepo;

		public RecentPaymentsForAdminQueryHandler(IPaymentRepo paymentRepo)
		{
			_paymentRepo = paymentRepo;
		}

		public async Task<List<PaymentResDTO>> Handle(RecentPaymentsForAdminQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var payments = await _paymentRepo.GetAllPayments();
				payments = payments.Take(4).ToList();

				var res = payments.Select(p => new PaymentResDTO
				{
					TransactionId = p.TransactionId,
					CustomerId = p.CustomerId.ToString(),
					Amount = p.Amount,
					Coin = p.Coin,
					Date = p.CreatedOn,
					Status = p.Status.ToString()
				}).ToList();

				return res;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
