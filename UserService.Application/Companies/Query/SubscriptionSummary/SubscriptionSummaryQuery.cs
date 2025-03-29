using MediatR;
using UserService.Application.Common.DTOs.Company;

namespace UserService.Application.Companies.Query.SubscriptionSummary
{
	public class SubscriptionSummaryQuery : IRequest<SubscriptionSummaryDTO>
	{
		public string CompanyId {  get; set; }
	}
}
