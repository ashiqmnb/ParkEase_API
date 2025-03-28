using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;

namespace PaymentService.Application.Transaction.Query.GetPayments
{
	public class GetPaymentsQuery : IRequest<PaymentPageResDTO>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
