using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;
using PaymentService.Domain.Repository;

namespace PaymentService.Application.Transaction.Query.RecentTransForAdmin
{
	public class RecentTransForAdminQueryHandler : IRequestHandler<RecentTransForAdminQuery, List<CompanyTransResDTO>>
	{
		private readonly ITransactionRepo _transactionRepo;

		public RecentTransForAdminQueryHandler(ITransactionRepo transactionRepo)
		{
			_transactionRepo = transactionRepo;
		}

		public async Task<List<CompanyTransResDTO>> Handle(RecentTransForAdminQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var transactions = await _transactionRepo.GetAllTransactions();

				transactions = transactions.Take(4).ToList();

				var res = transactions.Select(t => new CompanyTransResDTO
				{
					TransactionId = t.Id.ToString(),
					SenderId = t.SenderId,
					ReceiverId = t.ReceiverId,
					Coin = t.Coin,
					Description = t.Description,
					Date = t.CreatedOn,
					Status = t.Status.ToString(),
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
