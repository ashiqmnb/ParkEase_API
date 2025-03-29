using MediatR;
using UserService.Application.Common.DTOs.Company;

namespace UserService.Application.Companies.Query.GetRecentListed
{
	public class GetRecentListedQuery : IRequest<List<CompanyResForUserDTO>>
	{
	}
}
