using MediatR;
using UserService.Application.Common.DTOs.Company;

namespace UserService.Application.Companies.Query.GetCompaniesForAdmin
{
	public class GetCompaniesForAdminQuery : IRequest<CompanyPageResAdminDTO>
	{
		public CompanyQueryParamsForAdmin CompanyQueryParams { get; set; }

        public GetCompaniesForAdminQuery(CompanyQueryParamsForAdmin companyQueryParams)
        {
            this.CompanyQueryParams = companyQueryParams;
        }
    }
}
