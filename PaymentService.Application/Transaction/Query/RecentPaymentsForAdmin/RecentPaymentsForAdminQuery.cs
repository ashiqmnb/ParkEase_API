using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;

namespace PaymentService.Application.Transaction.Query.RecentPaymentsForAdmin
{
	public class RecentPaymentsForAdminQuery : IRequest<List<PaymentResDTO>>
	{
	}
}
