using MediatR;
using UserService.Application.Common.DTOs.Company;

namespace UserService.Application.Companies.Query.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<CompanyByIdResDTO>
    {
        public string companyId { get; set; }
    }
}
