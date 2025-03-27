using MediatR;
using UserService.Domain.Repository;

namespace UserService.Application.Coin.Query.GetCurrentCoins
{
	public class GetCurrentCoinsCommandHandler : IRequestHandler<GetCurrentCoinsCommand, int>
	{
		private readonly IUserRepo _userRepo;
		private readonly ICompanyRepo _companyRepo;

		public GetCurrentCoinsCommandHandler(IUserRepo userRepo, ICompanyRepo companyRepo)
		{
			_userRepo = userRepo;
			_companyRepo = companyRepo;
		}

		public async Task<int> Handle(GetCurrentCoinsCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.Id == null) throw new Exception("Id is required");

				var user = await _userRepo.GetUserById(request.Id);
				if (user != null) return user.Coins;

				var company = await _companyRepo.GetCompanyById(request.Id);
				if(company != null) return company.Coins;

				var admins = await _userRepo.GetAllAdmins();
				var admin = admins.FirstOrDefault(a => a.Id.ToString() == request.Id);
				if(admin != null) return admin.Coins;

				return 0;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
