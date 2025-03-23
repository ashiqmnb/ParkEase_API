using MediatR;
using UserService.Application.Common.DTOs.Company;
using UserService.Domain.Repository;

namespace UserService.Application.Companies.Query.GetCompaniesForUser
{
	public class GetCompaniesForUserQueryHandler : IRequestHandler<GetCompaniesForUserQuery, CompanyPageResUserDTO> 
	{
		private readonly ICompanyRepo _companyRepo;

		public GetCompaniesForUserQueryHandler(ICompanyRepo companyRepo)
		{
			_companyRepo = companyRepo;
		}

		public async Task<CompanyPageResUserDTO> Handle(GetCompaniesForUserQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var companies = await _companyRepo.GetAllCompaniesForUser();

				if (!string.IsNullOrEmpty(request.CompanyQueryParams.Type))
				{
					companies = companies
						.Where(c => c.Type.ToString().ToLower() == request.CompanyQueryParams.Type.ToLower())
						.ToList();
				}

				if (!string.IsNullOrEmpty(request.CompanyQueryParams.Search))
				{
					companies = companies
						.Where(c => c.Name.Contains(request.CompanyQueryParams.Search, StringComparison.OrdinalIgnoreCase))
						.ToList();
				}

				int totalPages = (int)Math.Ceiling((double)companies.Count / request.CompanyQueryParams.PageSize);


				companies = companies
					.Skip((request.CompanyQueryParams.PageNumber - 1) * request.CompanyQueryParams.PageSize)
					.Take(request.CompanyQueryParams.PageSize)
					.ToList();

				var companiesRes = companies.Select(c => new CompanyResForUserDTO
				{
					Id = c.Id.ToString(),
					Name = c.Name,
					Profile = c.Profile,
					Type = c.Type.ToString(),
					Place = c.Address.Place,
					District = c.Address.District,
					State = c.Address.State,
					PostalCode = c.Address.PostalCode
				}).ToList();

				var res = new CompanyPageResUserDTO
				{
					CurrentPage = request.CompanyQueryParams.PageNumber,
					TotalPages = totalPages,
					Companies = companiesRes
				};

				return res;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
