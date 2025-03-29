using MediatR;
using UserService.Application.Common.DTOs.Company;
using UserService.Domain.Repository;

namespace UserService.Application.Companies.Query.SubscriptionSummary
{
	public class SubscriptionSummaryQueryHandler : IRequestHandler<SubscriptionSummaryQuery, SubscriptionSummaryDTO>
	{
		private readonly ICompanyRepo _companyRepo;

		public SubscriptionSummaryQueryHandler(ICompanyRepo companyRepo)
		{
			_companyRepo = companyRepo;
		}

		public async Task<SubscriptionSummaryDTO> Handle(SubscriptionSummaryQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var company = await _companyRepo.GetCompanyById(request.CompanyId);
				if (company == null) throw new Exception("Company not found");

				var res = new SubscriptionSummaryDTO
				{
					Status = company.SubscriptionStatus.ToString(),
					SubscriptionDurationInDays = company.SubscriptionDurationInDays,
					SubscriptionExpiryDate = company.SubscriptionExpiryDate,
					SubscriptionStartDate = company.SubscriptionStartDate,
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
