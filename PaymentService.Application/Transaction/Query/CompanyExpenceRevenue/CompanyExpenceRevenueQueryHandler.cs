using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;
using PaymentService.Domain.Repository;

namespace PaymentService.Application.Transaction.Query.CompanyExpenceRevenue
{
	public class CompanyExpenceRevenueQueryHandler : IRequestHandler<CompanyExpenceRevenueQuery, CompanyExpenceRevenueDTO>
	{
		private readonly IPaymentRepo _paymentRepo;
		private readonly ITransactionRepo _transactionRepo;

		public CompanyExpenceRevenueQueryHandler(IPaymentRepo paymentRepo, ITransactionRepo transactionRepo)
		{
			_paymentRepo = paymentRepo;
			_transactionRepo = transactionRepo;
		}

		public async Task<CompanyExpenceRevenueDTO> Handle(CompanyExpenceRevenueQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var transactionRevenue = await _transactionRepo.GetTransactionByCompanyId(request.CompanyId);
				if (transactionRevenue == null) throw new Exception("No transactions found");

				transactionRevenue = transactionRevenue
					.Where(t => t.Status == Domain.Entity.TransactionStatus.Success)
					.ToList();

				var transactionExpense = await _transactionRepo.GetTransMadeByCompanyId(request.CompanyId);
				if (transactionRevenue == null) throw new Exception("No transactions found");

				transactionExpense = transactionExpense
					.Where(t => t.Status == Domain.Entity.TransactionStatus.Success)
					.ToList();

				int expense = transactionExpense.Sum(t => t.Coin);
				int revenue = transactionRevenue.Sum(t => t.Coin);

				var res = new CompanyExpenceRevenueDTO
				{
					Expence = expense,
					Revenue = revenue,
				};

				return res;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
			
		}
	}
}
