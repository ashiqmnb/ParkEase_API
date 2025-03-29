using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;

namespace PaymentService.Application.Transaction.Query.CompanyExpenceRevenue
{
	public class CompanyExpenceRevenueQuery : IRequest<CompanyExpenceRevenueDTO>
	{
		public string CompanyId { get; set; }
	}
}
