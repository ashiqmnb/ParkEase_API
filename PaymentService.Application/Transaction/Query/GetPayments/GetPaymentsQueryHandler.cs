using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;
using PaymentService.Domain.Repository;

namespace PaymentService.Application.Transaction.Query.GetPayments
{
	public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, PaymentPageResDTO>
	{
		private readonly IPaymentRepo _paymentRepo;

		public GetPaymentsQueryHandler(IPaymentRepo paymentRepo)
		{
			_paymentRepo = paymentRepo;
		}

		public async Task<PaymentPageResDTO> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var payments = await _paymentRepo.GetAllPayments();

				int totalPages = (int)Math.Ceiling((double)payments.Count / request.PageSize);

				payments = payments
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToList();

				var paymentRes = payments.Select(p => new PaymentResDTO
				{
					TransactionId = p.TransactionId,
					Amount = p.Amount,
					Coin = p.Coin,
					CustomerId = p.CustomerId.ToString(),
					Status = p.Status.ToString(),
					Date = p.CreatedOn
				}).ToList();

				var res = new PaymentPageResDTO
				{
					CurrentPage = request.PageNumber,
					TotalPages = totalPages,
					Payments = paymentRes
				};

				return res;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException.Message ?? ex.Message);
			}
		}
	}
}
