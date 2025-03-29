using MediatR;
using PaymentService.Domain.Repository;

namespace PaymentService.Application.Transaction.Query.TotalRevenue
{
	public class TotalRevenueQueryHandler : IRequestHandler<TotalRevenueQuery, int>
	{
		private readonly IPaymentRepo _paymentRepo;

		public TotalRevenueQueryHandler(IPaymentRepo paymentRepo)
		{
			_paymentRepo = paymentRepo;
		}

		public async Task<int> Handle(TotalRevenueQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var payments = await _paymentRepo.GetAllPayments();
				var revenue = payments.Sum(p => p.Amount);
				return revenue;	
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
