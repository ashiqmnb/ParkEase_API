using AutoMapper;
using MediatR;
using UserService.Application.Common.DTOs.Company;
using UserService.Domain.Repository;

namespace UserService.Application.Companies.Query.GetCompaniesForAdmin
{
	public class GetCompaniesForAdminQueryHandler : IRequestHandler<GetCompaniesForAdminQuery, CompanyPageResAdminDTO>
	{
		private readonly ICompanyRepo _companyRepo;

		public GetCompaniesForAdminQueryHandler(ICompanyRepo companyRepo)
		{
			_companyRepo = companyRepo;
		}

		public async Task<CompanyPageResAdminDTO> Handle(GetCompaniesForAdminQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var companies = await _companyRepo.GetAllCompaniesForAdmin();

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

				if (!string.IsNullOrEmpty(request.CompanyQueryParams.Status))
				{
					companies = companies
						.Where(c => c.SubscriptionStatus.ToString().ToLower() == request.CompanyQueryParams.Status.ToLower())
						.ToList();
				}

				int totalPages = (int)Math.Ceiling((double)companies.Count / request.CompanyQueryParams.PageSize);


				companies = companies
					.Skip((request.CompanyQueryParams.PageNumber - 1) * request.CompanyQueryParams.PageSize)
					.Take(request.CompanyQueryParams.PageSize)
					.ToList();

				var companiesRes = companies.Select(c => new CompanyResForAdminDTO
				{
					Id = c.Id.ToString(),
					Name = c.Name,
					Email = c.Email,
					Type = c.Type.ToString(),
					Status = c.SubscriptionStatus.ToString(),
					AddedDate = c.CreatedOn,
					IsBlocked = c.IsBlocked
				}).ToList();

				var res = new CompanyPageResAdminDTO
				{
					CurrentPage = request.CompanyQueryParams.PageNumber,
					TotalPages = totalPages,
					Companies = companiesRes
				};

				return res;

			}
		    catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ??  ex.Message);
			}
		}
	}
}
