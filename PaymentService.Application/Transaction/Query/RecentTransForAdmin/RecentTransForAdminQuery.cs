using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;

namespace PaymentService.Application.Transaction.Query.RecentTransForAdmin
{
	public class RecentTransForAdminQuery : IRequest<List<CompanyTransResDTO>>
	{
	}
}
