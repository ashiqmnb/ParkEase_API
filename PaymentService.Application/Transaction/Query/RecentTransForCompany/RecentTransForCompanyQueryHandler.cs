using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;
using PaymentService.Domain.Repository;

namespace PaymentService.Application.Transaction.Query.RecentPaymentsForCompany
{
	public class RecentTransForCompanyQueryHandler : IRequestHandler<RecentTransForCompanyQuery, List<CompanyTransResDTO>>
	{
		public readonly ITransactionRepo _transactionRepo;

		public RecentTransForCompanyQueryHandler(ITransactionRepo transactionRepo)
		{
			_transactionRepo = transactionRepo;
		}

		public async Task<List<CompanyTransResDTO>> Handle(RecentTransForCompanyQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var transactions = await _transactionRepo.GetTransactionByCompanyId(request.CompanyId);
				if (transactions == null) throw new Exception("Transactions not found");

				transactions = transactions.Take(4).ToList();

				var res = transactions.Select(t => new CompanyTransResDTO
				{
					SenderId = t.SenderId,
					ReceiverId = t.ReceiverId,
					Coin = t.Coin,
					TransactionId = t.Id.ToString(),
					Date = t.CreatedOn,
					Description = t.Description,
					Status = t.Status.ToString()
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
