using MediatR;
using UserService.Application.Common.DTOs.Company;
using UserService.Domain.Repository;

namespace UserService.Application.Companies.Query.GetRecentListed
{
	public class GetRecentListedQueryHandler : IRequestHandler<GetRecentListedQuery, List<CompanyResForUserDTO>>
	{
		private readonly ICompanyRepo _companyRepo;

		public GetRecentListedQueryHandler(ICompanyRepo companyRepo)
		{
			_companyRepo = companyRepo;
		}

		public async Task<List<CompanyResForUserDTO>> Handle(GetRecentListedQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var companies = await _companyRepo.GetAllCompaniesForUser();

				companies = companies.Take(4).ToList();

				var res = companies.Select(c => new CompanyResForUserDTO
				{
					Id = c.Id.ToString(),
					Name = c.Name,
					Profile = c.Profile,
					Type = c.Type.ToString(),
					Place = c.Address.Place,
					District = c.Address.District,
					State = c.Address.State,
					PostalCode = c.Address.PostalCode,
				}).ToList();

				return res;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
