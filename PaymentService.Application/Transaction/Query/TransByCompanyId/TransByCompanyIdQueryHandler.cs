using MassTransit.Initializers;
using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;
using PaymentService.Domain.Repository;

namespace PaymentService.Application.Transaction.Query.TransByCompanyId
{
	public class TransByCompanyIdQueryHandler : IRequestHandler<TransByCompanyIdQuery, CompanyPageTransResDTO>
	{
		private readonly ITransactionRepo _transactionRepo;

		public TransByCompanyIdQueryHandler(ITransactionRepo transactionRepo)
		{
			_transactionRepo = transactionRepo;
		}

		public async Task<CompanyPageTransResDTO> Handle(TransByCompanyIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var transactions = await _transactionRepo.GetTransactionByCompanyId(request.CompanyId);

				int totalPages = (int)Math.Ceiling((double)transactions.Count / request.PageSize);

				transactions = transactions
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToList();

				var resTrans = transactions.Select(t => new CompanyTransResDTO
				{
					TransactionId = t.Id.ToString(),
					SenderId = t.SenderId,
					ReceiverId = t.ReceiverId,
					Coin = t.Coin,
					Description = t.Description,
					Date = t.CreatedOn,
					Status = t.Status.ToString()
				}).ToList() ?? new List<CompanyTransResDTO>();

				var res = new CompanyPageTransResDTO
				{
					CurrentPage = request.PageNumber,
					TotalPages = totalPages,
					Transactions = resTrans
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
