using MediatR;
using UserService.Application.Common.DTOs.Company;

namespace UserService.Application.Companies.Query.GetCompaniesForUser
{
	public class GetCompaniesForUserQuery : IRequest<CompanyPageResUserDTO>
	{
		public CompanyQueryParamsForUser CompanyQueryParams {  get; set; }

        public GetCompaniesForUserQuery(CompanyQueryParamsForUser companyQueryParams)
		{
            this.CompanyQueryParams = companyQueryParams;
        }
    }
}
