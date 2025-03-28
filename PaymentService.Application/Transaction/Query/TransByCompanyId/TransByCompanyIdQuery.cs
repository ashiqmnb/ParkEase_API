using MediatR;
using PaymentService.Application.Common.DTOs.Transaction;

namespace PaymentService.Application.Transaction.Query.TransByCompanyId
{
	public class TransByCompanyIdQuery : IRequest<CompanyPageTransResDTO>
	{
		public string CompanyId { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
