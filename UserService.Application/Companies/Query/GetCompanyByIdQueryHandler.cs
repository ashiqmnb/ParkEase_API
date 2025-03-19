using MediatR;
using UserService.Application.Common.DTOs.Company;
using UserService.Domain.Repository;

namespace UserService.Application.Companies.Query
{
	public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyByIdResDTO>
	{
		private readonly ICompanyRepo _companyRepo;
		private readonly IAddressRepo _addressRepo;


		public GetCompanyByIdQueryHandler(ICompanyRepo companyRepo, IAddressRepo addressRepo)
		{
			_companyRepo = companyRepo;
			_addressRepo = addressRepo;
		}

		public async Task<CompanyByIdResDTO> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var company = await _companyRepo.GetCompanyById(request.companyId);

				if (company == null) throw new Exception("Company not found");

				var companyRes = new CompanyByIdResDTO
				{
					Name = company.Name,
					Email = company.Email,
					Phone = company.Phone,
					Description = company.Description,
					Profile = company.Profile,
					Images = company.Images,
					Coins = company.Coins,
					SubscriptionStatus = company.SubscriptionStatus.ToString(),
					StartDate = company.SubscriptionStartDate,
					EndDate = company.SubscriptionExpiryDate,
					Type = company.Type.ToString(),
					IsBlocked = company.IsBlocked,
					AddressId = company.AddressId,
				};

				if(company.AddressId != null)
				{
					var address = await _addressRepo.GetAddressById(company.AddressId.ToString());

					companyRes.Place = address.Place;
					companyRes.District = address.District;
					companyRes.State = address.State;
					companyRes.PostalCode = address.PostalCode;
					companyRes.Latitude = address.Latitude;
					companyRes.Longitude = address.Longitude;
				}

				return companyRes;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
